using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class spawController : MonoBehaviour
{
    public static spawController instance;
    [SerializeField]
    public List<Unit> ownedUnits = new List<Unit>();

    [SerializeField]
    public List<Unit> unitsInList = new List<Unit>();


    [SerializeField]
    Button Button;
    [SerializeField]
    GameObject panelContainer;

    [SerializeField]
    GameObject panel;
    [SerializeField]
    Button expandButton;
    RectTransform panelTF;
    [SerializeField]
    hracScriptable plScriptable;
    [SerializeField]
    List<Button> buttons = new List<Button>();
    bool waitingToSpawn = false;
    [SerializeField]
    Unit unitToSpawn;
    [SerializeField]
    GameObject SpawnGameObject;


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        expandButton.onClick.AddListener(() => { Expand(); });
        panelTF = panel.GetComponent<RectTransform>();
    }

    #region ui
    public void deleteButtons()
    {
        foreach (var button in buttons)  
            Destroy(button.gameObject);

        buttons.Clear();    
    }

    public void SpawnButton()
    { 
        deleteButtons();
        foreach (var unit in ownedUnits)
        {
            if (!unitsInList.Contains(unit))
            {
                string unitName = "";
                Sprite unitImg = null;
                foreach (var unitt in plScriptable.prefs)
                {
                    if (unit == unitt.unit)
                    {
                        unitName = unitt.Name;
                        unitImg = unitt.image;
                    }
                }
                Button newButton = Instantiate(Button, panelContainer.transform);
                Image img = newButton.transform.GetChild(0).GetComponent<Image>();
                img.sprite = unitImg;
                TMP_Text text = newButton.transform.GetChild(1).GetComponent<TMP_Text>();
                text.text = $"{unitName}";
                TMP_Text text2 = newButton.transform.GetChild(2).GetComponent<TMP_Text>();
                int count = 0;
                foreach(var ownedUnit in ownedUnits)
                {
                    if(ownedUnit.Name == unit.Name)
                    {
                        count++;
                        
                    }
                }

                text2.text = $"Count: {count}";
                newButton.onClick.AddListener(() => SpawnUnit(unit));
                buttons.Add(newButton);
               
                unitsInList.Add(unit);
            }
           
        }
        unitsInList.Clear();

        RectTransform tf = panelContainer.GetComponent<RectTransform>();
        RectTransform tfb = Button.GetComponent<RectTransform>();
        tf.sizeDelta = new Vector2(tf.sizeDelta.x, tfb.sizeDelta.y * buttons.Count * 1.6f);
    }

    void DeleteFromList(Unit unit)
    {
        ownedUnits.Remove(unit);
        SpawnButton();
    }

    public void SpawnUnit(Unit unit)
    {
       // DeleteFromList(unit);
        waitingToSpawn = true;
        unitToSpawn = unit;
    }

    public void Expand()
    {
        Debug.Log(panelTF.anchoredPosition.x);

        if (panelTF.anchoredPosition.x > 0)
        {
            panelTF.anchoredPosition = new Vector3(-panelTF.anchoredPosition.x,panelTF.anchoredPosition.y);
        }
        else
        {
            panelTF.anchoredPosition = new Vector3(Mathf.Abs(panelTF.anchoredPosition.x), panelTF.anchoredPosition.y);
           
        }
    }
    #endregion
    // Update is called once per frame
    void Update()
    {
        if (waitingToSpawn)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && Pauza.pauza.canChange)
            {

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                LayerMask layerMask = ~LayerMask.GetMask("pointer");

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
                {
                    if (hit.transform.gameObject.tag != "unit" && hit.transform.gameObject.tag != "enemy")
                    {
                        //Debug.Log(hit.transform.gameObject.tag);

                        Instantiate(unitToSpawn,hit.point, Quaternion.Euler(new Vector3(0,Random.Range(0,360),0)) ,SpawnGameObject.transform);
                        waitingToSpawn = false;
                        DeleteFromList(unitToSpawn);

                    }

                }



            }
        }
    }
}
