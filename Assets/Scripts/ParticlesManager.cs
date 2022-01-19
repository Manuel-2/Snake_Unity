using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    public static ParticlesManager sharedInstance;

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

    public void SpawnParticleEffect(string itemTag, Vector3 position)
    {
        var particles = ObjectPooler.sharedInstance.GetItem(itemTag, position, Quaternion.identity);
        var sis = particles.GetComponent<ParticleSystem>();
        sis.Play();

        StartCoroutine(RemoveEffect(particles, sis.main.duration + sis.main.startLifetime.constant));
    }

    IEnumerator RemoveEffect(GameObject particles, float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        ObjectPooler.sharedInstance.ReturnItem(particles);
    }
}
