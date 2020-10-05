using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatling : Tower
{
    public ParticleSystem gunshellParticleSystem;
    public ParticleSystem muzzleflashParticleSystem;
    public ParticleSystem shotParticleSystem;
    public ParticleSystem criticalShotParticleSystem;

    public AudioClip fire;

    public override void Fire()
    {
        AudioManager.instance.PlayGatlingSound(fire);
        bool crit = UnityEngine.Random.Range(0f, 1f) < UpgradeManager._instance.gatlingCriticalUpgradeLevel * UpgradeManager._instance.gatlingCriticalUpgrade;
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.right, Mathf.Infinity, layerMask);
        if (rayHit.collider != null)
        {
            // Deal damage to the monster
            float damageTotal = damage * (1 + (UpgradeManager._instance.damageUpgradeLevel * UpgradeManager._instance.damageUpgrade));
            if (crit)
                damageTotal *= 2f;
            

            Debug.Log($"Level of crit : {UpgradeManager._instance.gatlingCriticalUpgradeLevel} / Crit rate : {UpgradeManager._instance.gatlingCriticalUpgradeLevel * UpgradeManager._instance.gatlingCriticalUpgrade} / HasCrit : {crit}");

            rayHit.collider.gameObject.GetComponent<Monster>().TakeDamage(damageTotal);
        }

        // Visual effects
        gunshellParticleSystem.Emit(1);
        muzzleflashParticleSystem.Emit(30);
        if (!crit)
            shotParticleSystem.Emit(1);
        else
            criticalShotParticleSystem.Emit(1);
    }
}
