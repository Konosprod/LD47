using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour
{
    [Header("Tower prefabs")]
    public Tower[] towerPrefabs = new Tower[5];
    public Transform towersParent;

    [Header("UI")]
    public Text gatlingButtonText;  // towers[0]

    // Preview of the tower that you are trying to build
    private GameObject previewTower;
    private int selectedTowerType = -1;

    private List<GameObject> towers = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        SetGatlingButtonText();
    }


    // Update is called once per frame
    void Update()
    {
        if (selectedTowerType != -1)
        {
            // Right-click with selected tower to remove the selection
            if (Input.GetMouseButtonDown(1))
            {
                RemoveSelectedTower();
            }


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
        }
    }


    private void BuySelectedTower()
    {
        GameObject tower = Instantiate(towerPrefabs[selectedTowerType].gameObject, towersParent);
        tower.transform.position = new Vector3(previewTower.transform.position.x, previewTower.transform.position.y, 0f);
        towers.Add(tower);
        GameManager._instance.Spend(towerPrefabs[selectedTowerType].cost);
    }


    private void RemoveSelectedTower()
    {
        selectedTowerType = -1;
        Destroy(previewTower);
    }


    public void SelectTowerType(int type)
    {
        selectedTowerType = type;
        GameObject preview = Instantiate(towerPrefabs[type].gameObject);

        // Disable animations and shooting
        preview.GetComponent<Tower>().enabled = false;
        preview.GetComponentInChildren<Animator>().enabled = false;

        // Remove previous preview tower
        Destroy(previewTower);

        previewTower = preview;
    }

    private void SetGatlingButtonText()
    {
        gatlingButtonText.text = $"Gatling gun\nCost : {towerPrefabs[0].cost} \nDamage : {towerPrefabs[0].damage} \nFire rate : {(1 / towerPrefabs[0].fireDelay)}/s";
    }
}
