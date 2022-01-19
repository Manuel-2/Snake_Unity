using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    [SerializeField] SnakeController snakeController;
    [SerializeField] string appleTag, bodyPartTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(appleTag))
        {
            snakeController.AddBodyPart();
            Destroy(collision.gameObject);
        }else if (collision.gameObject.CompareTag(bodyPartTag))
        {
            snakeController.GameOver();
        }
    }
}
