using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int playerIndex;
    public Material healthMaterial;
    public Material staminaMaterial;
    public GameObject playerCoinPrefab;
    public  GameObject HealthBar;
    public GameObject ShotCounter;
    public GameplayManager gm;
    private StatsManager stats;
    public float currentHealth;
    //public float maxHealth = 5f;
    public float damageCount;
    private GameStats gameStats;
    public bool isHit;
    public bool isHealed;
    //public float maxStaminaCount;
    public float currStaminaCount;
    public float reloadTimer;
    //private float resetReloadTimer = 0f;
    public float reloadCooldown = 1f;
    public GameEvent OnPlayerDeath;

    public int playerID;
    // Start is called before the first frame update
    void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameplayManager>();
        GameObject playerCoin = Instantiate(playerCoinPrefab, gm.playerCoinParent) as GameObject;
        HealthBar = playerCoin.transform.GetChild(0).gameObject;
        ShotCounter = playerCoin.transform.GetChild(1).gameObject;

        healthMaterial = new Material(playerCoin.transform.GetChild(0).gameObject.GetComponent<Image>().material);
        playerCoin.transform.GetChild(0).gameObject.GetComponent<Image>().material = healthMaterial;

        staminaMaterial = new Material(playerCoin.transform.GetChild(1).gameObject.GetComponent<Image>().material);
        playerCoin.transform.GetChild(1).gameObject.GetComponent<Image>().material = staminaMaterial;
    }
    void Start()
    {
        stats = GetComponent<StatsManager>();
        
        gameStats = GetComponent<GameStats>();
        
        healthMaterial.SetFloat("_RemoovedSegment", stats.maxHealth);
        healthMaterial.SetFloat("_SegmentCount", stats.maxHealth);
        staminaMaterial.SetFloat("_RemoovedSegment", stats.maxStaminaCount);
        staminaMaterial.SetFloat("_SegmentCount", stats.maxStaminaCount);
        currStaminaCount = stats.maxStaminaCount;
        currentHealth = stats.maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (currStaminaCount < stats.maxStaminaCount)
        {
            reloadTimer += Time.deltaTime;
            if (reloadTimer > reloadCooldown){
                
                currStaminaCount += Time.deltaTime;
                //Debug.Log(reloadTimer);
                /*if (reloadTimer > reloadCooldown)
                {
                    currStaminaCount++;
                    //Debug.Log(currStaminaCount);
                    reloadTimer = resetReloadTimer;
                }*/

            }
            //reloadTimer = resetReloadTimer;
        }
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Death();
            
        }
        staminaMaterial.SetFloat("_RemoovedSegment", stats.maxStaminaCount - currStaminaCount);
        healthMaterial.SetFloat("_RemoovedSegment", stats.maxHealth - currentHealth);
       

    }
    public void UseStamina(float staminaCost){
        currStaminaCount--;
        gameStats.StaminaUsed((int)staminaCost);
    }
    
    public void TakeDamage(float damage)
    {
        /*float newHealth = currentHealth;
        newHealth += bulletDamage / -10f;*/
        currentHealth += damage / -10f;
        gameStats.DamageTaken((int)damage);
        //Debug.Log("Hurt" + currentHealth);
        //StartCoroutine(ChangeHealth(newHealth));
    }
    void GainHealth(float healAmount)
    {
        currentHealth -= healAmount / 10f;
    }
    IEnumerator ChangeHealth(float newHealth){
        while(newHealth != currentHealth){
            currentHealth += .1f;
            yield return new WaitForSeconds(.1f);
        }
        
    }
    public void Respawn(){
        currentHealth = stats.maxHealth;
        currStaminaCount = stats.maxStaminaCount;
        staminaMaterial.SetFloat("_SegmentCount", stats.maxStaminaCount);
        healthMaterial.SetFloat("_SegmentCount", stats.maxHealth);
    }
    void Death()
    {
        OnPlayerDeath.Raise(this, playerIndex);
        gm.placements.Add(this.gameObject);
    }
}
