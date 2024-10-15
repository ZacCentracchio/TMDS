using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class BlastardExplosion : MonoBehaviour
{
    public GameObject explosionPrefab;
    public DemoShake cameraShake;
    public float knockbackForce = 50f;  
    public float knockbackDuration = 1f;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject explosionInstance = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        
        cameraShake.Shake(1f, 2.5f);
        Destroy(gameObject);
        explosionInstance.AddComponent<ExplosionDestroyer>();
        AudioManager.instance.PlayOneShot(FMODEvents.instance.BlastardExplode, this.transform.position);
    }

}
