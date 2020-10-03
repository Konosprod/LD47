using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatling : Tower
{
    public ParticleSystem gunshellParticleSystem;

    public override void Fire()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.right, Mathf.Infinity, layerMask);
        if(rayHit.collider != null)
        {
            rayHit.collider.gameObject.GetComponent<Monster>().TakeDamage(damage);
            gunshellParticleSystem.Emit(1);
        }
    }
}
