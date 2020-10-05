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
    public Button damageUpgradeButton;
    public Button fireRateUpgradeButton;

    [HideInInspector]
    public int damageUpgradeLevel = 0;
    private int damageUpgradeCost = 100;
    [HideInInspector]
    public float damageUpgrade = 0.25f;
    [HideInInspector]
    public int fireRateUpgradeLevel = 0;
    private int fireRateUpgradeCost = 150;
    [HideInInspector]
    public float fireRateUpgrade = 0.20f;

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


    public void UpdateAllTexts()
    {
        UpdateDamageUpgradeButtonText();
        UpdateFireRateUpgradeButtonText();
    }

    private void UpdateDamageUpgradeButtonText()
    {
        damageUpgradeButtonText.text = $"Increase tower damage by {(damageUpgradeLevel + 1) * (damageUpgrade * 100f)}%\nCost : {Mathf.Pow(2f, damageUpgradeLevel) * damageUpgradeCost}";
        damageUpgradeButton.interactable = GameManager._instance.CanAfford(Mathf.FloorToInt(Mathf.Pow(2, damageUpgradeLevel) * damageUpgradeCost));
    }
    private void UpdateFireRateUpgradeButtonText()
    {
        fireRateUpgradeButtonText.text = $"Increase tower fire rate by {(fireRateUpgradeLevel + 1) * (fireRateUpgrade * 100f)}%\nCost : {Mathf.Pow(2f, fireRateUpgradeLevel) * fireRateUpgradeCost}";
        fireRateUpgradeButton.interactable = GameManager._instance.CanAfford(Mathf.FloorToInt(Mathf.Pow(2, fireRateUpgradeLevel) * fireRateUpgradeCost));
    }

}
