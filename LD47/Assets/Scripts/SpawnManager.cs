using System;
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
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(0.5f, new Wave.Spawn(2, 1)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(1f, new Wave.Spawn(10, 0)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(1f, new Wave.Spawn(1, 2)));

        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(4f, new Wave.Spawn(10, 1)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(5f, new Wave.Spawn(15, 1)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(5f, new Wave.Spawn(20, 0)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(6f, new Wave.Spawn(3, 2)));

        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(7f, new Wave.Spawn(30, 0)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(7f, new Wave.Spawn(30, 1)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(10f, new Wave.Spawn(3, 2)));

        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(10.5f, new Wave.Spawn(30, 1)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(10.5f, new Wave.Spawn(30, 0)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(11f, new Wave.Spawn(5, 2)));

        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(14f, new Wave.Spawn(50, 1)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(14.5f, new Wave.Spawn(50, 0)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(15f, new Wave.Spawn(75, 1)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(15f, new Wave.Spawn(10, 2)));

        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(18f, new Wave.Spawn(50, 1)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(19f, new Wave.Spawn(100, 0)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(20f, new Wave.Spawn(50, 1)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(21f, new Wave.Spawn(100, 0)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(22f, new Wave.Spawn(50, 1)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(23f, new Wave.Spawn(150, 0)));

        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(25f, new Wave.Spawn(20, 2)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(26f, new Wave.Spawn(80, 1)));

        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(31f, new Wave.Spawn(20, 2)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(31f, new Wave.Spawn(50, 1)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(31f, new Wave.Spawn(100, 0)));


        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(34f, new Wave.Spawn(60, 1)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(35f, new Wave.Spawn(120, 0)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(36f, new Wave.Spawn(60, 1)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(37f, new Wave.Spawn(120, 0)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(38f, new Wave.Spawn(60, 1)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(39f, new Wave.Spawn(175, 0)));

        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(41f, new Wave.Spawn(30, 2)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(42f, new Wave.Spawn(100, 1)));

        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(46f, new Wave.Spawn(50, 2)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(46f, new Wave.Spawn(150, 1)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(46f, new Wave.Spawn(250, 0)));

        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(50f, new Wave.Spawn(250, 0)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(52f, new Wave.Spawn(250, 0)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(54f, new Wave.Spawn(250, 1)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(56f, new Wave.Spawn(250, 0)));
        wave.spawnList.Add(new Tuple<float, Wave.Spawn>(58f, new Wave.Spawn(250, 1)));
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
     * 0    Zombie-girl   100       2        10 
     * 1    Fast-girl      25       5        5
     * 2    Big-boy       1500      1.5      100        Speed is special due to animation
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
            newMob.transform.localScale = new Vector3(UnityEngine.Random.Range(0.8f, 1.2f), UnityEngine.Random.Range(0.8f, 1.2f));
            Monster monster = newMob.GetComponent<Monster>();
            monster.speed *= UnityEngine.Random.Range(0.9f, 1.1f);

            // Scaling stats with time
            float scaling = 1f + Mathf.Sqrt(currentTimer);
            monster.speed *= Mathf.Sqrt(scaling);
            monster.maxHP *= scaling;
            if(currentTimer > 15f)
            {
                monster.maxHP += 50f * scaling;
            }
            if(currentTimer > 30f)
            {
                monster.maxHP += 50f * scaling;
                monster.maxHP *= scaling;
            }
            if(currentTimer > 45f)
            {
                monster.maxHP += 500f * scaling;
            }

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
