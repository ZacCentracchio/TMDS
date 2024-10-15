using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDestroyer : MonoBehaviour
{
    public float destroyDelay = 0.9f;
    void Start()
    {
        StartCoroutine(DestroyExplosion());
    }
    private IEnumerator DestroyExplosion()
    {
        yield return new WaitForSeconds(destroyDelay);
        ParticleSystem explosionParticles = GetComponent<ParticleSystem>();
        if (explosionParticles != null)
        {
            explosionParticles.Stop();
        }
        Destroy(gameObject);
        Debug.Log("Destroyed explosion instance");
    }
}
