﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : Tower
{
    public Sprite flamethrowerOff;
    public Sprite flamethrowerOn;

    public SpriteRenderer spriteRenderer;

    public ParticleSystem flameParticleSystem;
    public BoxCollider2D flameCollider;

    public float initialSizeX;
    public float initialOffsetX;
    public float initialStartSpeed;

    private void Awake()
    {
        initialSizeX = flameCollider.size.x;
        initialOffsetX = flameCollider.offset.x;
        initialStartSpeed = flameParticleSystem.main.startSpeedMultiplier;
    }

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

        if (monstersAffected.Count == 0)
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
