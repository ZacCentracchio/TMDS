using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    
    private StatsManager stats;
    public float bulletDamage;
    public List<string> destroyableObjects = new List<string>();

    public void SetBulletDamage(float newBulletDamage){
        bulletDamage = newBulletDamage;
    }
    private void OnTriggerEnter(Collider collision)
    {
        
        if(destroyableObjects.Contains(collision.tag) ){
            
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(bulletDamage);
            Debug.Log("Range Hit " + bulletDamage);
            //onBulletHit.Raise(this, stats.bulletDamage);
            Destroy(this.gameObject); 
        }    
        
    }
}
