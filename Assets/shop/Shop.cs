using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.UI.CanvasScaler;

public class Shop : MonoBehaviour
{
    public static Shop instance;
    [SerializeField]
    hracScriptable plScriptable;
    [SerializeField]
    GameObject panel;
    [SerializeField]
    Button Button;
    [SerializeField]
    Sprite upgrade;
    [SerializeField]
    int upgradePrice;
    Button upgradeButton;
    [SerializeField]
    GameObject firework;
    public int fireworkCount = 50;
    [SerializeField]
    List<Button> buttons = new List<Button>();
    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        makeUpgradeButton();
        spawnButtons();
    }

    private void spawnButtons()
    {
        int count = 0;
        foreach (var unit in plScriptable.prefs)
        {
            if (unit.neededLevel <= StationController.instance.stationLevel)
            {
                Button newButton = Instantiate(Button, panel.transform);
                Image img = newButton.transform.GetChild(0).GetComponent<Image>();
                img.sprite = unit.image;
                TMP_Text text = newButton.transform.GetChild(1).GetComponent<TMP_Text>();
                text.text = $"{unit.Name}";
                TMP_Text text2 = newButton.transform.GetChild(2).GetComponent<TMP_Text>();
                text2.text = $"Price: {unit.Price}$";
                newButton.onClick.AddListener(() => Buy(unit.unit, unit.Price));
                buttons.Add(newButton);
                count++;

            }
        }
        RectTransform tf = panel.GetComponent<RectTransform>();
        RectTransform tfb = Button.GetComponent<RectTransform>();
        tf.sizeDelta = new Vector2(tf.sizeDelta.x, tfb.sizeDelta.y * (1 + count) * 1.6f);
    }

    public void updateButtons()
    {
        foreach(var btn in buttons)
        {
            Destroy(btn.gameObject);
        }
        buttons.Clear();
        
        spawnButtons();

    }
    void makeUpgradeButton()
    {
        upgradeButton = Instantiate(Button, panel.transform);
        Image img = upgradeButton.transform.GetChild(0).GetComponent<Image>();
        img.sprite = upgrade;
        TMP_Text text = upgradeButton.transform.GetChild(1).GetComponent<TMP_Text>();
        text.text = $"Station upgrade";
        TMP_Text text2 = upgradeButton.transform.GetChild(2).GetComponent<TMP_Text>();
        text2.text = $"Price: {upgradePrice}$";
        upgradeButton.onClick.AddListener(() => { 
            
            Upgrade(upgradePrice);

            SpawnFireworks();
        
        });
    }
    public void Buy(Unit unit, int price)
    {
        if (MoneyGenerator.instance.currency - price >= 0)
        {
            MoneyGenerator.instance.currency -= price;
            spawController.instance.ownedUnits.Add(unit);
            spawController.instance.SpawnButton();
        }

    }

    public void SpawnFireworks()
    {
        for(int i = 0; i <= fireworkCount; i++)
        {
            Vector3 position = GeneratePositionForFirework();

            Instantiate(firework,position,Quaternion.Euler(-90,Random.Range(0,360),0));
        }
    }
    public Vector3 GeneratePositionForFirework()
    {
        Vector3 position = new Vector3(Random.Range(StationController.instance.station.transform.position.x - 15, StationController.instance.station.transform.position.x + 15), 0,Random.Range(StationController.instance.station.transform.position.z - 15, StationController.instance.station.transform.position.z + 15));
        return position;
    }

    public void Upgrade(int price)
    {
        if (MoneyGenerator.instance.currency - price >= 0)
        {
            MoneyGenerator.instance.currency -= price;
            StationController.instance.levelUp();
        }
    }
    public void removeUpgradeButton()
    {
        Destroy(upgradeButton.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
