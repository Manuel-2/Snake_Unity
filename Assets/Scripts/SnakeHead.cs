using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    [SerializeField] string appleTag, bodyPartTag;
    [SerializeField] AudioClip EatAppleSound, hitSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(appleTag))
        {
            EatApple(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag(bodyPartTag) && SnakeController.sharedInstance.invincible == false)
        {
            SnakeController.sharedInstance.GameOver();
            AudioManager.sharedInstance.PlaySound(hitSound);
            PostProcesingManager.sharedInstance.PlayHitEffect();
        }
    }

    private void EatApple(GameObject Apple)
    {
        SnakeController.sharedInstance.EatApple();
        ParticlesManager.sharedInstance.SpawnParticleEffect("AppleExplotion", Apple.transform.position);
        ObjectPooler.sharedInstance.ReturnItem(Apple);
        AudioManager.sharedInstance.PlayAppleSound(EatAppleSound);
    }
}
