using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StationController : MonoBehaviour
{
    public static StationController instance;

    public StationMovement station;
    public List<StationMovement> stations = new List<StationMovement>();
    public GameObject panel;
    public int stationLevel = 0;
    public int maxLevel = 3;

    private void Awake()
    {
        instance = this;
    }

    public void AddStation(Transform tf)
    {
        tf.TryGetComponent<StationMovement>(out StationMovement st);
        station = st;
        if(station != null)
        {
            station.isSelected(true);
            playerAttackController.instance.station = station;  
            panel.SetActive(true);
        }
    }

    public void RemoveStation()
    {
        if (station != null)
        {
            station.isSelected(false);
            station = null;
            playerAttackController.instance.station = null;
            panel.SetActive(false);
        }
        

    }

    public void levelUp()
    {
        stationLevel++;
        Shop.instance.updateButtons();
        if (stationLevel == maxLevel)
            Shop.instance.removeUpgradeButton();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
