﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public int cost;
    public float damage;
    public float fireDelay;

    public Animator animator;

    [HideInInspector]
    public float currentDelay;

    [HideInInspector]
    public int layerMask;


    // Start is called before the first frame update
    public void Start()
    {
        currentDelay = fireDelay;
        layerMask = LayerMask.GetMask("Zombie");
    }

    // Update is called once per frame
    public void Update()
    {
        currentDelay -= Time.deltaTime;
        if (currentDelay <= 0f)
        {
            Fire();
            currentDelay = fireDelay / (1 + (UpgradeManager._instance.fireRateUpgradeLevel * UpgradeManager._instance.fireRateUpgrade));
        }
    }

    public abstract void Fire();
}
