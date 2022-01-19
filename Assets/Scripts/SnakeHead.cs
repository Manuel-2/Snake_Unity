using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    [SerializeField] string appleTag, bodyPartTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(appleTag))
        {
            SnakeController.sharedInstance.AddBodyPart();
            Destroy(collision.gameObject);
        }else if (collision.gameObject.CompareTag(bodyPartTag) && SnakeController.sharedInstance.invincible == false)
        {
            SnakeController.sharedInstance.GameOver();
        }
    }
}
