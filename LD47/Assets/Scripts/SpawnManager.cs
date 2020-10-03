using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject monstersParentObject;

    private List<GameObject> monstersList = new List<GameObject>();

    private GameManager gameManager;

    public GameObject[] monsterPrefabs = new GameObject[3];

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager._instance;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Reset()
    {
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
}
