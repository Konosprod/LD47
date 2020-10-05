using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager _instance;
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this);
    }


    [Header("UI")]
    public GameObject upgradePanel;

    public Text damageUpgradeButtonText;
    public Text fireRateUpgradeButtonText;
    public Text gatlingCriticalUpgradeButtonText;
    public Text mortarMultishotUpgradeButtonText;
    public Text flamethrowerRangeUpgradeButtonText;
    public Text barbedWireWeakeningUpgradeButtonText;

    public Button damageUpgradeButton;
    public Button fireRateUpgradeButton;
    public Button gatlingCriticalUpgradeButton;
    public Button mortarMultishotUpgradeButton;
    public Button flamethrowerRangeUpgradeButton;
    public Button barbedWireWeakeningUpgradeButton;

    // Tower damage
    [HideInInspector]
    public int damageUpgradeLevel = 0;
    private int damageUpgradeCost = 100;
    [HideInInspector]
    public float damageUpgrade = 0.25f;

    // Tower fire rate
    [HideInInspector]
    public int fireRateUpgradeLevel = 0;
    private int fireRateUpgradeCost = 150;
    [HideInInspector]
    public float fireRateUpgrade = 0.20f;

    // Gatling critical
    [HideInInspector]
    public int gatlingCriticalUpgradeLevel = 0;
    private int gatlingCriticalUpgradeCost = 500;
    [HideInInspector]
    public float gatlingCriticalUpgrade = 0.20f;
    private const int gatlingCriticalUpgradeMaxLevel = 5;

    // Mortar multishot
    [HideInInspector]
    public int mortarMultishotUpgradeLevel = 0;
    private int mortarMultishotUpgradeCost = 1000;
    [HideInInspector]
    public float mortarMultishotUpgrade = 0.10f;
    private const int mortarMultishotUpgradeMaxLevel = 10;


    // Flamethrower range
    [HideInInspector]
    public int flamethrowerRangeUpgradeLevel = 0;
    private int flamethrowerRangeUpgradeCost = 1000;
    [HideInInspector]
    public float flamethrowerRangeUpgrade = 0.20f;


    // Barbed wire weakening
    [HideInInspector]
    public int barbedWireWeakeningUpgradeLevel = 0;
    private int barbedWireWeakeningUpgradeCost = 750;
    [HideInInspector]
    public float barbedWireWeakeningUpgrade = 0.15f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateAllTexts();
    }


    public void BuyDamageUpgrade()
    {
        if (GameManager._instance.CanAfford(Mathf.FloorToInt(Mathf.Pow(2, damageUpgradeLevel) * damageUpgradeCost)))
        {
            GameManager._instance.Spend(Mathf.FloorToInt(Mathf.Pow(2, damageUpgradeLevel) * damageUpgradeCost));
            damageUpgradeLevel++;
            UpdateDamageUpgradeButtonText();

            TowerManager._instance.UpdateTowersButtonText();
        }
    }

    public void BuyFireRateUpgrade()
    {
        if (GameManager._instance.CanAfford(Mathf.FloorToInt(Mathf.Pow(2, fireRateUpgradeLevel) * fireRateUpgradeCost)))
        {
            GameManager._instance.Spend(Mathf.FloorToInt(Mathf.Pow(2, fireRateUpgradeLevel) * fireRateUpgradeCost));
            fireRateUpgradeLevel++;
            UpdateFireRateUpgradeButtonText();

            TowerManager._instance.UpdateTowersAnimatorSpeed();
            TowerManager._instance.UpdateTowersButtonText();
        }
    }

    public void BuyGatlingCriticalUpgrade()
    {
        if(GameManager._instance.CanAfford(Mathf.FloorToInt(Mathf.Pow(2, gatlingCriticalUpgradeLevel) * gatlingCriticalUpgradeCost)))
        {
            GameManager._instance.Spend(Mathf.FloorToInt(Mathf.Pow(2, gatlingCriticalUpgradeLevel) * gatlingCriticalUpgradeCost));
            gatlingCriticalUpgradeLevel++;
            UpdateGatlingCriticalUpgradeButtonText();
        }
    }

    public void BuyMortarMultishotUpgrade()
    {
        if(GameManager._instance.CanAfford(Mathf.FloorToInt(Mathf.Pow(2, mortarMultishotUpgradeLevel) * mortarMultishotUpgradeCost)))
        {
            GameManager._instance.Spend(Mathf.FloorToInt(Mathf.Pow(2, mortarMultishotUpgradeLevel) * mortarMultishotUpgradeCost));
            mortarMultishotUpgradeLevel++;
            UpdateMortarMultishotUpgradeButtonText();
        }
    }

    public void BuyFlamethrowerRangeUpgrade()
    {
        if(GameManager._instance.CanAfford(Mathf.FloorToInt(Mathf.Pow(2, flamethrowerRangeUpgradeLevel) * flamethrowerRangeUpgradeCost)))
        {
            GameManager._instance.Spend(Mathf.FloorToInt(Mathf.Pow(2, flamethrowerRangeUpgradeLevel) * flamethrowerRangeUpgradeCost));
            flamethrowerRangeUpgradeLevel++;
            UpdateFlamethrowerRangeUpgradeButtonText();
        }
    }

    public void BuyBarbedWireWeakeningUpgrade()
    {
        if(GameManager._instance.CanAfford(Mathf.FloorToInt(Mathf.Pow(2, barbedWireWeakeningUpgradeLevel) * barbedWireWeakeningUpgradeCost)))
        {
            GameManager._instance.Spend(Mathf.FloorToInt(Mathf.Pow(2, barbedWireWeakeningUpgradeLevel) * barbedWireWeakeningUpgradeCost));
            barbedWireWeakeningUpgradeLevel++;
            UpdateBarbedWireWeakeningUpgradeButtonText();
        }
    }

    public void UpdateAllTexts()
    {
        UpdateDamageUpgradeButtonText();
        UpdateFireRateUpgradeButtonText();
        UpdateGatlingCriticalUpgradeButtonText();
        UpdateMortarMultishotUpgradeButtonText();
        UpdateFlamethrowerRangeUpgradeButtonText();
        UpdateBarbedWireWeakeningUpgradeButtonText();
    }

    private void UpdateDamageUpgradeButtonText()
    {
        damageUpgradeButtonText.text = $"Increase tower damage by {(damageUpgradeLevel + 1) * (damageUpgrade * 100f)}%\nCost : {Mathf.FloorToInt(Mathf.Pow(2f, damageUpgradeLevel) * damageUpgradeCost)}";
        damageUpgradeButton.interactable = GameManager._instance.CanAfford(Mathf.FloorToInt(Mathf.Pow(2, damageUpgradeLevel) * damageUpgradeCost));
    }
    private void UpdateFireRateUpgradeButtonText()
    {
        fireRateUpgradeButtonText.text = $"Increase tower fire rate by {(fireRateUpgradeLevel + 1) * (fireRateUpgrade * 100f)}%\nCost : {Mathf.FloorToInt(Mathf.Pow(2f, fireRateUpgradeLevel) * fireRateUpgradeCost)}";
        fireRateUpgradeButton.interactable = GameManager._instance.CanAfford(Mathf.FloorToInt(Mathf.Pow(2, fireRateUpgradeLevel) * fireRateUpgradeCost));
    }
    private void UpdateGatlingCriticalUpgradeButtonText()
    {
        gatlingCriticalUpgradeButtonText.text = $"Gatling gun has a {(gatlingCriticalUpgradeLevel + (gatlingCriticalUpgradeLevel == gatlingCriticalUpgradeMaxLevel ? 0 : 1)) * gatlingCriticalUpgrade * 100f}% chance to deal a critical strike\nCost : {Mathf.FloorToInt(Mathf.Pow(2, gatlingCriticalUpgradeLevel) * gatlingCriticalUpgradeCost)}";
        gatlingCriticalUpgradeButton.interactable = gatlingCriticalUpgradeLevel < gatlingCriticalUpgradeMaxLevel && GameManager._instance.CanAfford(Mathf.FloorToInt(Mathf.Pow(2, gatlingCriticalUpgradeLevel) * gatlingCriticalUpgradeCost));
    }
    private void UpdateMortarMultishotUpgradeButtonText()
    {
        mortarMultishotUpgradeButtonText.text = $"Mortar has a {(mortarMultishotUpgradeLevel + (mortarMultishotUpgradeLevel == mortarMultishotUpgradeMaxLevel ? 0 : 1)) * mortarMultishotUpgrade * 100f}% chance to fire an extra shell\nCost : {Mathf.FloorToInt(Mathf.Pow(2, mortarMultishotUpgradeLevel) * mortarMultishotUpgradeCost)}";
        mortarMultishotUpgradeButton.interactable = mortarMultishotUpgradeLevel < mortarMultishotUpgradeMaxLevel && GameManager._instance.CanAfford(Mathf.FloorToInt(Mathf.Pow(2, mortarMultishotUpgradeLevel) * mortarMultishotUpgradeCost));
    }
    private void UpdateFlamethrowerRangeUpgradeButtonText()
    {
        flamethrowerRangeUpgradeButtonText.text = $"Increase flamethrower range by {(flamethrowerRangeUpgradeLevel + 1) * (flamethrowerRangeUpgrade * 100f)}%\nCost : {Mathf.Pow(2f, flamethrowerRangeUpgradeLevel) * flamethrowerRangeUpgradeCost}";
        flamethrowerRangeUpgradeButton.interactable = GameManager._instance.CanAfford(Mathf.FloorToInt(Mathf.Pow(2, flamethrowerRangeUpgradeLevel) * flamethrowerRangeUpgradeCost));
    }
    private void UpdateBarbedWireWeakeningUpgradeButtonText()
    {
        barbedWireWeakeningUpgradeButtonText.text = $"Ennemies affected by barbed wire take {(barbedWireWeakeningUpgradeLevel + 1) * (barbedWireWeakeningUpgrade * 100f)}% increased damage\nCost : {Mathf.Pow(2f, barbedWireWeakeningUpgradeLevel) * barbedWireWeakeningUpgradeCost}";
        barbedWireWeakeningUpgradeButton.interactable = GameManager._instance.CanAfford(Mathf.FloorToInt(Mathf.Pow(2, barbedWireWeakeningUpgradeLevel) * barbedWireWeakeningUpgradeCost));
    }
}
