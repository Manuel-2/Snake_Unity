using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;

    [Header("Game Elements")]
    [SerializeField] GameObject snakePrefab;
    [SerializeField] Transform snakeSpawn;
    [SerializeField] string snakeContainerName;
    [SerializeField] string appleTag;
    [SerializeField] int pointPerApple;
    [SerializeField] string playerHighScoreKey;
    [SerializeField] Vector2 minGameArea, maxGameArea;
    [SerializeField] string appleDestroyExplotionEffectName;
    [Header("UI Components")]
    [SerializeField] string scorePrefix;
    [SerializeField] TextMeshProUGUI inGameScoreTextComponent;
    [SerializeField] TextMeshProUGUI endGameScoreTextComponent;
    [SerializeField] string highScorePrefix;
    [SerializeField] TextMeshProUGUI inGameHighScoreTextComponent;
    [SerializeField] TextMeshProUGUI endameHighScoreTextComponent;
    [Header("Menu animations")]
    [SerializeField] Animator menuAnimator;
    [SerializeField] string gameOverAnimationTrigger;

    [HideInInspector]
    public int score;

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

    public void GameOver()
    {
        menuAnimator.SetTrigger(gameOverAnimationTrigger);
        int highScore = PlayerPrefs.GetInt(playerHighScoreKey, 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt(playerHighScoreKey, score);
        }
        CleanPlayArea();

        //Update GameOver UI
        endGameScoreTextComponent.text = $"{scorePrefix} {score}";
        endameHighScoreTextComponent.text = $"{highScorePrefix} {highScore}";
    }

    public void AddPoints()
    {
        score += pointPerApple;

        //Update UI
        inGameScoreTextComponent.text = $"{scorePrefix} {score}";
    }

    public void StartNewMatch()
    {
        Instantiate(snakePrefab, snakeSpawn.position, Quaternion.identity);
        score = 0;

        //TODO: get this value from the playerConfiguration
        int amountOfApples = 5;
        GenerateNewRoundOfApples(amountOfApples);

        //update UI
        inGameScoreTextComponent.text = $"{scorePrefix} {score}";
        inGameHighScoreTextComponent.text = $"{highScorePrefix} {PlayerPrefs.GetInt(playerHighScoreKey, 0)}";
    }

    public void GenerateNewRoundOfApples(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if(maxGameArea.magnitude <= 0)
            {
                Debug.LogWarning("El area de juego es 0, tienes que configurar el vector de maxGameArea en el GameManager");
            }

            Vector3 ApplePosition = GenerateRandomPosition(this.minGameArea, this.maxGameArea);
            GameObject Apple = ObjectPooler.sharedInstance.GetItem(appleTag, ApplePosition, Quaternion.identity);

            GameObject snakeContainer = GameObject.Find(snakeContainerName);
            int numberOfParts = snakeContainer.transform.childCount;
            List<Transform> snakeParts = new List<Transform>();

            for (int j = 0; j < numberOfParts; j++)
            {
                snakeParts.Add(snakeContainer.transform.GetChild(j));

                if(snakeParts[j].position.x == ApplePosition.x && snakeParts[j].position.y == ApplePosition.y)
                {
                    ObjectPooler.sharedInstance.ReturnItem(Apple);
                    GenerateNewRoundOfApples(1);
                }
            }
        }
    }

    public Vector3 GenerateRandomPosition(Vector2 min, Vector2 max)
    {
        int posX = Random.Range((int)min.x, (int)max.x + 1);
        int posY = Random.Range((int)min.y, (int)max.y + 1);

        return new Vector3(posX, posY, 0);
    }

    public void CleanPlayArea()
    {
        var Apples = GameObject.FindGameObjectsWithTag(appleTag);
        foreach (GameObject Apple in Apples)
        {
            ParticlesManager.sharedInstance.SpawnParticleEffect(appleDestroyExplotionEffectName, Apple.transform.position);
            ObjectPooler.sharedInstance.ReturnItem(Apple);
        }
    }
}
