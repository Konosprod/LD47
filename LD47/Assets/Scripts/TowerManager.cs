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

    private int[] towersBought = new int[4];

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
                    if (GameManager._instance.CanAfford(Mathf.FloorToInt(towerPrefabs[selectedTowerType].cost * Mathf.Pow(1.5f, towersBought[selectedTowerType]))))
                    {
                        BuySelectedTower();
                    }
                }
            }

            // Move the preview to the correct position
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            previewTower.transform.position = new Vector3(mousePos.x, previewTower.transform.position.y, -1f);
            tooPoor.SetActive(!GameManager._instance.CanAfford(Mathf.FloorToInt(towerPrefabs[selectedTowerType].cost * Mathf.Pow(1.5f, towersBought[selectedTowerType]))));
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
        if (t.animator != null)
            t.animator.speed = 1 + (UpgradeManager._instance.fireRateUpgradeLevel * UpgradeManager._instance.fireRateUpgrade);

        if (t.GetType().Name == "FlameThrower")
        {
            FlameThrower ft = t.GetComponent<FlameThrower>();
            ft.flameCollider.size = new Vector2(ft.initialSizeX * (1f + (UpgradeManager._instance.flamethrowerRangeUpgradeLevel * UpgradeManager._instance.flamethrowerRangeUpgrade)), ft.flameCollider.size.y);
            ft.flameCollider.offset = new Vector2(ft.initialOffsetX * (1f + (UpgradeManager._instance.flamethrowerRangeUpgradeLevel * UpgradeManager._instance.flamethrowerRangeUpgrade * 0.75f)), ft.flameCollider.offset.y); // 0.75f otherwise the collider moves away from the flamethrower sprite
            var fpsm = ft.flameParticleSystem.main;
            fpsm.startSpeedMultiplier = ft.initialStartSpeed * (1f + (UpgradeManager._instance.flamethrowerRangeUpgradeLevel * UpgradeManager._instance.flamethrowerRangeUpgrade));
        }

        towers.Add(t);

        GameManager._instance.Spend(Mathf.FloorToInt(towerPrefabs[selectedTowerType].cost * Mathf.Pow(1.5f, towersBought[selectedTowerType])));
        towersBought[selectedTowerType]++;
        UpdateTowersButtonText();
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
        foreach (Tower tower in towers)
        {
            if (tower.animator != null)
            {
                tower.animator.speed = 1 + (UpgradeManager._instance.fireRateUpgradeLevel * UpgradeManager._instance.fireRateUpgrade);
            }
        }
    }

    public void UpdateFlamethrowerRange()
    {
        foreach (Tower tower in towers)
        {
            if (tower.GetType().Name == "FlameThrower")
            {
                FlameThrower ft = tower.GetComponent<FlameThrower>();
                ft.flameCollider.size = new Vector2(ft.initialSizeX * (1f + (UpgradeManager._instance.flamethrowerRangeUpgradeLevel * UpgradeManager._instance.flamethrowerRangeUpgrade)), ft.flameCollider.size.y);
                ft.flameCollider.offset = new Vector2(ft.initialOffsetX * (1f + (UpgradeManager._instance.flamethrowerRangeUpgradeLevel * UpgradeManager._instance.flamethrowerRangeUpgrade * 0.75f)), ft.flameCollider.offset.y); // 0.75f otherwise the collider moves away from the flamethrower sprite
                var fpsm = ft.flameParticleSystem.main;
                fpsm.startSpeedMultiplier = ft.initialStartSpeed * (1f + (UpgradeManager._instance.flamethrowerRangeUpgradeLevel * UpgradeManager._instance.flamethrowerRangeUpgrade));
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
        gatlingButtonText.text = $"Gatling gun\n\nCost \n{Mathf.FloorToInt(towerPrefabs[0].cost * Mathf.Pow(1.5f, towersBought[0]))} \nDamage \n{towerPrefabs[0].damage * (1 + (UpgradeManager._instance.damageUpgradeLevel * UpgradeManager._instance.damageUpgrade))} \nFire rate \n {(1 + (UpgradeManager._instance.fireRateUpgradeLevel * UpgradeManager._instance.fireRateUpgrade)) / towerPrefabs[0].fireDelay}/s";
    }

    private void SetMortarButtonText()
    {
        mortarButtonText.text = $"Mortar\n\nCost \n {Mathf.FloorToInt(towerPrefabs[1].cost * Mathf.Pow(1.5f, towersBought[1]))} \nDamage \n {towerPrefabs[1].damage * (1 + (UpgradeManager._instance.damageUpgradeLevel * UpgradeManager._instance.damageUpgrade))} \nFire rate \n {(1 + (UpgradeManager._instance.fireRateUpgradeLevel * UpgradeManager._instance.fireRateUpgrade)) / towerPrefabs[1].fireDelay}/s";
    }

    private void SetFlamethrowerButtonText()
    {
        flamethrowerButtonText.text = $"Flamethrower\n\nCost \n {Mathf.FloorToInt(towerPrefabs[2].cost * Mathf.Pow(1.5f, towersBought[2]))} \nDPS \n {towerPrefabs[2].damage * (1 + (UpgradeManager._instance.damageUpgradeLevel * UpgradeManager._instance.damageUpgrade)) * (1 + (UpgradeManager._instance.fireRateUpgradeLevel * UpgradeManager._instance.fireRateUpgrade))}/s";
    }

    private void SetBarbedWireButtonText()
    {
        barbedWireButtonText.text = $"Barbed wire\n\nCost \n {Mathf.FloorToInt(towerPrefabs[3].cost * Mathf.Pow(1.5f, towersBought[3]))} \nDPS \n {towerPrefabs[3].damage * (1 + (UpgradeManager._instance.damageUpgradeLevel * UpgradeManager._instance.damageUpgrade)) * (1 + (UpgradeManager._instance.fireRateUpgradeLevel * UpgradeManager._instance.fireRateUpgrade))}/s\nSlows ennemies by <color=#ADD8E6>60%</color>";
    }
}
