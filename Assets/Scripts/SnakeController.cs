using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public static SnakeController sharedInstance;

    [SerializeField] Transform snakeHead;
    [SerializeField] float stepDistance;
    [Tooltip("indicates how many steps does in one second")]
    [SerializeField] float stepFrequency;
    [SerializeField] GameObject snakeBodyPart;
    [SerializeField] GameObject conectorPrefab;
    [SerializeField] int snakeInitialSize;
    [SerializeField] float snakeSize;
    [SerializeField] float time2DestroySnake;
    [SerializeField] string spawnParticleEffectTag;
    [SerializeField] string explotionParticleEffectTag;

    List<Transform> snakeBody = new List<Transform>();
    List<Transform> snakeConectors = new List<Transform>();
    Vector3 movementDirection;
    Vector3 lastMovementDirection;
    bool isAlive;
    [HideInInspector]
    public bool invincible;

    public enum Directions
    {
        up,
        right,
        down,
        left
    }

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        SetUpSnake();
        StartMoving();
    }

    private void SetUpSnake()
    {
        isAlive = true;
        invincible = true;
        snakeBody.Add(snakeHead);
        for (int i = 1; i < snakeInitialSize; i++)
        {
            AddBodyPart();
        }
        // Set Speed
        stepFrequency = CalculateStepFrequency();
        // particles
        ParticlesManager.sharedInstance.SpawnParticleEffect(spawnParticleEffectTag, snakeHead.position);
    }

    public float CalculateStepFrequency()
    {
        int level = PlayerPrefs.GetInt(MenuManager.sharedInstance.playerGameSpeedConfigkey, MenuManager.sharedInstance.defaultGameSpeed);
        float frequency = 0.5f -(level - 1f) / 10f;
        return frequency;
    }

    private void CanDie()
    {
        invincible = false;
    }

    private void Step()
    {
        if (isAlive == false)
            return;

        if (stepDistance == 0)
        {
            Debug.LogWarning("Step distance not set, the snake it not going to move");
            return;
        }
        Vector3 lastPos = snakeHead.position;
        snakeHead.position += movementDirection.normalized * stepDistance;
        lastMovementDirection = movementDirection;

        for (int i = 1; i < snakeBody.Count; i++)
        {
            Vector3 temp = snakeBody[i].position;
            snakeBody[i].position = lastPos;
            lastPos = temp;

            Vector3 tailDirection = (snakeBody[i - 1].position - snakeBody[i].position).normalized;
            snakeConectors[i - 1].position = snakeBody[i].position + tailDirection * (snakeSize / 2);
        }
    }

    public void EatApple()
    {
        AddBodyPart();
        GameManager.sharedInstance.AddPoints();
        GameManager.sharedInstance.GenerateNewRoundOfApples(1);
    }

    private void AddBodyPart()
    {
        Transform part = Instantiate(snakeBodyPart, snakeBody[snakeBody.Count - 1].position, Quaternion.identity).transform;
        part.SetParent(this.transform);
        snakeBody.Add(part);

        Transform connector = Instantiate(conectorPrefab, snakeBody[snakeBody.Count - 1].position, Quaternion.identity).transform;
        connector.SetParent(this.transform);
        snakeConectors.Add(connector);
    }

    public void StartMoving()
    {
        setDirection(Directions.up);
        lastMovementDirection = Vector3.up;
        InvokeRepeating("Step", stepFrequency, stepFrequency);
        Invoke("CanDie", (snakeInitialSize * stepFrequency));
    }

    public void GameOver()
    {
        isAlive = false;
        GameManager.sharedInstance.GameOver();
        // call explotion effect
        StartCoroutine("DestroySnake");
    }

    public void setDirection(Directions input)
    {
        switch (input)
        {
            case Directions.up:
                if (lastMovementDirection != Vector3.down)
                    movementDirection = Vector3.up;
                break;
            case Directions.right:
                if (lastMovementDirection != Vector3.left)
                    movementDirection = Vector3.right;
                break;
            case Directions.down:
                if (lastMovementDirection != Vector3.up)
                    movementDirection = Vector3.down;
                break;
            case Directions.left:
                if (lastMovementDirection != Vector3.right)
                    movementDirection = Vector3.left;
                break;
            default:
                Debug.LogWarning("input not match any case");
                break;
        }
    }

    IEnumerator DestroySnake()
    {
        var delayBetweenExplotions = time2DestroySnake / snakeBody.Count;
        // travel all snake parts
        for (int i = 0; i < snakeBody.Count; i++)
        {
            ParticlesManager.sharedInstance.SpawnParticleEffect(explotionParticleEffectTag, snakeBody[i].position);
            Destroy(snakeBody[i].gameObject);
            if (i != 0)
                Destroy(snakeConectors[i - 1].gameObject);
            yield return new WaitForSeconds(delayBetweenExplotions);
        }
        WindowsManager.sharedInstance.ShowGameOverWindow();
        Destroy(this.gameObject);
    }
}
