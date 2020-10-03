using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public SpawnManager spawnManager;

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LoseLoop()
    {
        spawnManager.Reset();
    }
}
