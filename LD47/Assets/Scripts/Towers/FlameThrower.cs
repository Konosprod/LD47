using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : Tower
{
    public Sprite flamethrowerOff;
    public Sprite flamethrowerOn;

    public SpriteRenderer spriteRenderer;

    public ParticleSystem flameParticleSystem;

    private List<Monster> monstersAffected = new List<Monster>();

    public override void Fire()
    {
        for (int i = monstersAffected.Count - 1; i >= 0; i--)
        {
            monstersAffected[i].TakeDamage(damage * Time.deltaTime);
        }

        if(monstersAffected.Count == 0)
        {
            spriteRenderer.sprite = flamethrowerOff;
            ParticleSystem.EmissionModule emission = flameParticleSystem.emission;
            emission.enabled = false;
        }
        else
        {
            spriteRenderer.sprite = flamethrowerOn;
            ParticleSystem.EmissionModule emission = flameParticleSystem.emission;
            emission.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        monstersAffected.Add(collision.gameObject.GetComponent<Monster>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        monstersAffected.Remove(collision.gameObject.GetComponent<Monster>());
    }
}
