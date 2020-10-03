using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float maxHP;
    public float speed;
    public int goldReward;

    private float currentHP;

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
        currentHP -= damage;
        if(currentHP <= 0f)
        {
            //Debug.Log("RIP : " + gameObject.name);
            currentHP = 0f;
            GameManager._instance.EarnGold(goldReward);
            SpawnManager._instance.MonsterDeath(gameObject);
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"{other.name} has collided with me");
    }
}
