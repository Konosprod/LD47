using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour
{
    public static TowerManager _instance;
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this);
    }


    [Header("Tower prefabs")]
    public Tower[] towerPrefabs = new Tower[5];
    public Transform towersParent;
    public GameObject tooPoor;

    [Header("UI")]
    public Text gatlingButtonText;  // towerPrefabs[0]
    public Text mortarButtonText;  // towerPrefabs[1]
    public Text flamethrowerButtonText;  // towerPrefabs[2]
    public Text barbedWireButtonText;  // towerPrefabs[3]

    // Preview of the tower that you are trying to build
    private GameObject previewTower;
    private int selectedTowerType = -1;

    private List<Tower> towers = new List<Tower>();

    // Start is called before the first frame update
    void Start()
    {
        UpdateTowersButtonText();
    }


    // Update is called once per frame
    void Update()
    {
        if (selectedTowerType != -1)
        {
            // Left-click to buy tower
            if (Input.GetMouseButtonDown(0))
            {
                // Check if the mouse is over a UI element
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    // Buy the tower if the player can afford it
                    if (GameManager._instance.CanAfford(towerPrefabs[selectedTowerType].cost))
                    {
                        BuySelectedTower();
                    }
                }
            }

            // Move the preview to the correct position
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            previewTower.transform.position = new Vector3(mousePos.x, previewTower.transform.position.y, -1f);
            tooPoor.SetActive(!GameManager._instance.CanAfford(towerPrefabs[selectedTowerType].cost));
            tooPoor.transform.position = new Vector3(mousePos.x, previewTower.transform.position.y, -2f);


            // Right-click with selected tower to remove the selection
            if (Input.GetMouseButtonDown(1))
            {
                RemoveSelectedTower();
            }
        }
    }


    private void BuySelectedTower()
    {
        GameObject tower = Instantiate(towerPrefabs[selectedTowerType].gameObject, towersParent);
        tower.transform.position = new Vector3(previewTower.transform.position.x, previewTower.transform.position.y, 0f);
        Tower t = tower.GetComponent<Tower>();
        if(t.animator != null) 
            t.animator.speed = 1 + (UpgradeManager._instance.fireRateUpgradeLevel * UpgradeManager._instance.fireRateUpgrade);
        towers.Add(t);
        GameManager._instance.Spend(towerPrefabs[selectedTowerType].cost);
    }


    private void RemoveSelectedTower()
    {
        selectedTowerType = -1;
        tooPoor.SetActive(false);
        Destroy(previewTower);
    }


    public void SelectTowerType(int type)
    {
        selectedTowerType = type;
        GameObject preview = Instantiate(towerPrefabs[type].gameObject);

        // Disable animations and shooting
        preview.GetComponent<Tower>().enabled = false;

        Collider2D collider = preview.GetComponent<Collider2D>();
        if (collider != null)
            collider.enabled = false;

        Animator animator = preview.GetComponentInChildren<Animator>();
        if (animator != null)
            animator.enabled = false;


        // Remove previous preview tower
        Destroy(previewTower);

        previewTower = preview;
    }

    public void UpdateTowersAnimatorSpeed()
    {
        foreach(Tower tower in towers)
        {
            if(tower.animator != null)
            {
                tower.animator.speed = 1 + (UpgradeManager._instance.fireRateUpgradeLevel * UpgradeManager._instance.fireRateUpgrade);
            }
        }
    }

    public void UpdateTowersButtonText()
    {
        SetGatlingButtonText();
        SetMortarButtonText();
        SetFlamethrowerButtonText();
        SetBarbedWireButtonText();
    }

    private void SetGatlingButtonText()
    {
        gatlingButtonText.text = $"Gatling gun\nCost : {towerPrefabs[0].cost} \nDamage : {towerPrefabs[0].damage * (1 + (UpgradeManager._instance.damageUpgradeLevel * UpgradeManager._instance.damageUpgrade))} \nFire rate : {(1 + (UpgradeManager._instance.fireRateUpgradeLevel * UpgradeManager._instance.fireRateUpgrade)) / towerPrefabs[0].fireDelay}/s";
    }

    private void SetMortarButtonText()
    {
        mortarButtonText.text = $"Mortar\nCost : {towerPrefabs[1].cost} \nDamage : {towerPrefabs[1].damage * (1 + (UpgradeManager._instance.damageUpgradeLevel * UpgradeManager._instance.damageUpgrade))} \nFire rate : {(1 + (UpgradeManager._instance.fireRateUpgradeLevel * UpgradeManager._instance.fireRateUpgrade)) / towerPrefabs[1].fireDelay}/s";
    }

    private void SetFlamethrowerButtonText()
    {
        flamethrowerButtonText.text = $"Flamethrower\nCost : {towerPrefabs[2].cost} \nDPS : {towerPrefabs[2].damage * (1 + (UpgradeManager._instance.damageUpgradeLevel * UpgradeManager._instance.damageUpgrade)) * (1 + (UpgradeManager._instance.fireRateUpgradeLevel * UpgradeManager._instance.fireRateUpgrade))}/s";
    }

    private void SetBarbedWireButtonText()
    {
        barbedWireButtonText.text = $"Barbed wire\nCost : {towerPrefabs[3].cost} \nDPS : {towerPrefabs[3].damage * (1 + (UpgradeManager._instance.damageUpgradeLevel * UpgradeManager._instance.damageUpgrade)) * (1 + (UpgradeManager._instance.fireRateUpgradeLevel * UpgradeManager._instance.fireRateUpgrade))}/s\nSlows ennemies by 60%";
    }
}
