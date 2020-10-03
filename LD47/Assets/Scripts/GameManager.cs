using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    [Header("Game logic")]
    public SpawnManager spawnManager;

    [Header("UI")]
    public Text goldText;
    public Text timerText;

    private int totalGold = 50;
    private int currentGold;

    private float initialTimer = 60f;
    private float currentTimer;

    public bool waveInProgress = true;

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
        currentGold = totalGold;
        UpdateGoldText();
        currentTimer = initialTimer;
        UpdateTimerText();
    }

    // Update is called once per frame
    void Update()
    {
        if (waveInProgress)
        {
            currentTimer -= Time.deltaTime;
            if (currentTimer <= 0f)
            {
                // Win

            }
            else
                UpdateTimerText();
        }
    }


    public void LoseLoop()
    {
        spawnManager.Reset();

        // Reset
        currentGold = totalGold;
        UpdateGoldText();
        currentTimer = initialTimer;
    }


    public void EarnGold(int goldReward)
    {
        totalGold += goldReward;
        currentGold += goldReward;
        UpdateGoldText();
    }

    public void UpdateGoldText()
    {
        goldText.text = $"Gold : {currentGold}";
    }

    public void UpdateTimerText()
    {
        float seconds = Mathf.FloorToInt(currentTimer % 60);
        float milliseconds = (currentTimer % 1) * 1000;
        timerText.text = string.Format("{0:00}:{1:000}", seconds, milliseconds);
    }
}
