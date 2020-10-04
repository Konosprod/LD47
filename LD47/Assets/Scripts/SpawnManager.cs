﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private class Wave
    {
        public struct Spawn
        {
            public int number;
            public int prefabID;

            public Spawn(int n, int pid)
            {
                number = n;
                prefabID = pid;
            }
        }

        public List<Tuple<float, Spawn>> spawnList = new List<Tuple<float, Spawn>>();
    }

    private Wave wave = new Wave();
    private void BuildWave()
    {
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(0f, new Wave.Spawn(5, 0)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(0.5f, new Wave.Spawn(20, 1)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(1f, new Wave.Spawn(10, 0)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(1f, new Wave.Spawn(1, 2)));
    }
    private int waveIndex = 0;

    public static SpawnManager _instance;

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this);

        BuildWave();
    }


    public Transform monstersParent;

    private List<GameObject> monstersList = new List<GameObject>();

    private GameManager gameManager;

    public GameObject[] monsterPrefabs = new GameObject[3];
    /* Monster balance
     * ID |   Sprite    | MaxHP | Speed | Gold reward | Note
     * 0    Zombie-girl    50       3        10 
     * 1    Fast-girl      25       8        15
     * 2    Big-boy       1500      2.2      250        Speed is special due to animation
     */

    private float currentTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager._instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.waveInProgress)
        {
            currentTimer += Time.deltaTime;
            if (waveIndex < wave.spawnList.Count)
            {
                Tuple<float, Wave.Spawn> tuple = wave.spawnList[waveIndex];
                if (currentTimer >= tuple.Item1)
                {
                    Spawn(monsterPrefabs[tuple.Item2.prefabID], tuple.Item2.number);
                    waveIndex++;
                }
            }
        }
    }

    private void Spawn(GameObject prefab, int number)
    {
        for (int i = 0; i < number; i++)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x + UnityEngine.Random.Range(-0.5f, 0.5f), prefab.transform.position.y + transform.position.y);
            GameObject newMob = GameObject.Instantiate(prefab, spawnPosition, prefab.transform.rotation, monstersParent);
            Monster monster = newMob.GetComponent<Monster>();
            monster.speed *= UnityEngine.Random.Range(0.9f, 1.1f);
            ZombieBehaviour zombie = newMob.GetComponent<ZombieBehaviour>();
            zombie.monsterInfo = monster;

            if (zombie.hasSpriteVariation)
            {
                int rand = UnityEngine.Random.Range(0, zombie.sprites.Length);
                zombie.spriteRenderer.sprite = zombie.sprites[rand];
                zombie.animator.runtimeAnimatorController = zombie.animatorControllers[rand];
            }

            monstersList.Add(newMob);
        }
    }


    public void Reset()
    {
        waveIndex = 0;
        currentTimer = 0f;
        ClearMonsters();
    }

    private void ClearMonsters()
    {
        for (int i = monstersList.Count - 1; i >= 0; i--)
        {
            Destroy(monstersList[i]);
        }

        monstersList.Clear();
    }


    public void MonsterDeath(GameObject monster)
    {
        monstersList.Remove(monster);
    }
}
