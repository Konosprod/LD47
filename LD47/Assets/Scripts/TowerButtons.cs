using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject panelStats;

    public void OnPointerEnter(PointerEventData eventData)
    {
        panelStats.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        panelStats.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
