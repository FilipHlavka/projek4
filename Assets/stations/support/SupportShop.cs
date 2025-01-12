using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SupportShop : MonoBehaviour
{
    public static SupportShop instance;
   
    [SerializeField]
    GameObject panel;
    [SerializeField]
    Button Button;
    [SerializeField]
    Sprite heal;
    [SerializeField]
    Sprite atck;
    [SerializeField]
    int upgradePrice;
    Button healButton;
    Button atckButton;
    [SerializeField]
    GameObject firework;
    public int fireworkCount = 10;
   
    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        CreateButton();
        
    }
   
    void CreateButton()
    {
        healButton = Instantiate(Button, panel.transform);
        Image img = healButton.transform.GetChild(0).GetComponent<Image>();
        img.sprite = heal;
        TMP_Text text = healButton.transform.GetChild(1).GetComponent<TMP_Text>();
        text.text = $"Heal support";
        TMP_Text text2 = healButton.transform.GetChild(2).GetComponent<TMP_Text>();
        text2.text = $"Price: {upgradePrice}$";
        healButton.onClick.AddListener(() => {

            if (Buy(upgradePrice))
            {
                SpawnFireworks();

                StationController.instance.SupportStation.HealPower();
            }
        });

        atckButton = Instantiate(Button, panel.transform);
        Image img2 = atckButton.transform.GetChild(0).GetComponent<Image>();
        img2.sprite = atck;
        TMP_Text text3 = atckButton.transform.GetChild(1).GetComponent<TMP_Text>();
        text3.text = $"Attack support";
        TMP_Text text4 = atckButton.transform.GetChild(2).GetComponent<TMP_Text>();
        text4.text = $"Price: {upgradePrice}$";
        atckButton.onClick.AddListener(() => {

            if (Buy(upgradePrice))
            {
                SpawnFireworks();

                StationController.instance.SupportStation.AtckPower();
            }
            

        });
    }
 

    public void SpawnFireworks()
    {
        for (int i = 0; i <= fireworkCount; i++)
        {
            Vector3 position = GeneratePositionForFirework();

            Instantiate(firework, position, Quaternion.Euler(-90, Random.Range(0, 360), 0));
        }
    }
    public Vector3 GeneratePositionForFirework()
    {
        Vector3 position = new Vector3(Random.Range(StationController.instance.SupportStation.transform.position.x - 5, StationController.instance.SupportStation.transform.position.x + 5), 0, Random.Range(StationController.instance.SupportStation.transform.position.z - 5, StationController.instance.SupportStation.transform.position.z + 5));
        return position;
    }

    public bool Buy(int price)
    {
        if (MoneyGenerator.instance.currency - price >= 0)
        {
            MoneyGenerator.instance.currency -= price;
           return true;
        }
        return false;
    }
   
}
