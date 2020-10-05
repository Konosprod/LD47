using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatling : Tower
{
    public ParticleSystem gunshellParticleSystem;
    public ParticleSystem muzzleflashParticleSystem;
    public ParticleSystem shotParticleSystem;

    public override void Fire()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.right, Mathf.Infinity, layerMask);
        if (rayHit.collider != null)
        {
            // Deal damage to the monster
            rayHit.collider.gameObject.GetComponent<Monster>().TakeDamage(damage * (1 + (UpgradeManager._instance.damageUpgradeLevel * UpgradeManager._instance.damageUpgrade)));
        }

        // Visual effects
        gunshellParticleSystem.Emit(1);
        muzzleflashParticleSystem.Emit(30);
        shotParticleSystem.Emit(1);
    }
}
