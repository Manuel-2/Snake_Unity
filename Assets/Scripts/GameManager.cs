using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;

    [SerializeField] GameObject snakePrefab;
    [SerializeField] Transform snakeSpawn;

    private void Awake()
    {
        if(sharedInstance == null)
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
        // TODO: show the menus, save the data and that
    }

    public void StartNewMatch()
    {
        //TODO: spawn a new snake, hide the main menu interface and use ingame ui, spawn apples
        Instantiate(snakePrefab, snakeSpawn.position, Quaternion.identity);
    }
}
