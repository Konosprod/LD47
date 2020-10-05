using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : Tower
{
    public float explosionRadius;
    public GameObject mortarShellPrefab;

    public AudioClip fire;

    public override void Fire()
    {
        AudioManager.instance.PlayMortar(fire);
        GameObject shell = Instantiate(mortarShellPrefab, transform);
        MortarShell ms = shell.GetComponent<MortarShell>();
        ms.damage = damage * (1 + (UpgradeManager._instance.damageUpgradeLevel * UpgradeManager._instance.damageUpgrade));
        ms.explosionRadius = explosionRadius;

        ms.rigidbody2D.AddForce(new Vector2(0.42f, 0.9f).normalized * UnityEngine.Random.Range(250f, 400f));

        if(UnityEngine.Random.Range(0f, 1f) < UpgradeManager._instance.mortarMultishotUpgradeLevel * UpgradeManager._instance.mortarMultishotUpgrade)
        {
            GameObject shell2 = Instantiate(mortarShellPrefab, transform);
            ms = shell2.GetComponent<MortarShell>();
            ms.damage = damage * (1 + (UpgradeManager._instance.damageUpgradeLevel * UpgradeManager._instance.damageUpgrade));
            ms.explosionRadius = explosionRadius;

            ms.rigidbody2D.AddForce(new Vector2(0.42f, 0.9f).normalized * UnityEngine.Random.Range(250f, 400f));
        }
    }
}
