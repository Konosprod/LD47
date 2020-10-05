using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float maxHP;
    public float speed;
    public int goldReward;

    private float currentHP;

    [HideInInspector]
    public int isSlowed = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void TakeDamage(float damage)
    {
        currentHP -= damage * (isSlowed > 0 ? 1f + (UpgradeManager._instance.barbedWireWeakeningUpgradeLevel * UpgradeManager._instance.barbedWireWeakeningUpgrade) : 1f);
        if (currentHP <= 0f)
        {
            //Debug.Log("RIP : " + gameObject.name);
            currentHP = 0f;
            GameManager._instance.EarnGold(goldReward);
            SpawnManager._instance.MonsterDeath(gameObject);
            Destroy(gameObject);
        }
    }


    /*void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"{other.name} has collided with me");
    }*/
}
