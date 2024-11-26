using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField]
    hracScriptable plScriptable;
    [SerializeField]
    GameObject panel;
    [SerializeField]
    Button Button;

   
   
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
        foreach(var unit in plScriptable.prefs)
        {
            if (unit.neededLevel <= StationController.instance.stationLevel)
            {
                Button newButton = Instantiate(Button, panel.transform);
                TMP_Text text = newButton.transform.GetComponentInChildren<TMP_Text>();
                text.text = $"{unit.Name}: {unit.Price} $";
                newButton.onClick.AddListener(() => Buy(unit.unit, unit.Price));


            }
        }
    }
    public void Buy(Unit unit, int price)
    {
        if (MoneyGenerator.instance.currency - price >= 0)
        {
            MoneyGenerator.instance.currency -= price;
            spawController.instance.ownedUnits.Add(unit);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
