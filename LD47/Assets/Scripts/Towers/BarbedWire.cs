using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbedWire : Tower
{
    private List<Monster> monstersAffected = new List<Monster>();

    public override void Fire()
    {
        for (int i = monstersAffected.Count - 1; i >= 0; i--)
        {
            if (monstersAffected[i] != null)
                monstersAffected[i].TakeDamage(damage * (1 + (UpgradeManager._instance.damageUpgradeLevel * UpgradeManager._instance.damageUpgrade)) * Time.deltaTime * (1 + (UpgradeManager._instance.fireRateUpgradeLevel * UpgradeManager._instance.fireRateUpgrade)));
            else
                monstersAffected.RemoveAt(i);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Monster monster = collision.gameObject.GetComponent<Monster>();
        monstersAffected.Add(monster);
        monster.isSlowed++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Monster monster = collision.gameObject.GetComponent<Monster>();
        monstersAffected.Remove(monster);
        monster.isSlowed--;
    }
}
