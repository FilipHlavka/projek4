
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;
using System.Collections;

//using UnityEngine.UIElements;

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
    [SerializeField]
    float jumpDuration;
    [SerializeField]
    GameObject Cross;
    float timer;

    [SerializeField]
    Button CancelButton;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        expandButton.onClick.AddListener(() => { Expand(); });
        CancelButton.onClick.AddListener(unClick);
        panelTF = panel.GetComponent<RectTransform>();
    }

    #region ui

    public void unClick()
    {
        waitingToSpawn = false;
    }
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
       // Debug.Log(panelTF.anchoredPosition.x);

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
                        if (!CanBeSpawned(hit.point))
                        {
                            Instantiate(Cross, hit.point, Quaternion.Euler(0,0,0));
                            return;
                        }
                        Spawn(hit.point);
                        waitingToSpawn = false;
                        DeleteFromList(unitToSpawn);

                    }

                }



            }
        }
    }


    public void Spawn(Vector3 spawnPosition)
    {

       
        //spawnPosition = new Vector3(spawnPosition.x,29,spawnPosition.z);
        Quaternion rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));

        Vector3 axis = rotation * Vector3.back;

       
        Vector3 toSpawnPosition = spawnPosition + axis * 20;

        Debug.Log(toSpawnPosition);
        Debug.Log(spawnPosition);
    
        if (NavMesh.SamplePosition(toSpawnPosition, out NavMeshHit hit, 100, NavMesh.AllAreas))
        {
            Unit un = Instantiate(unitToSpawn, toSpawnPosition, rotation, SpawnGameObject.transform);
            soundManager.instance.PlayClip(un.spawn);
            //Debug.Log(new Vector3(toSpawnPosition.x, 29, toSpawnPosition.z) + "    " + new Vector3(spawnPosition.x, 29, spawnPosition.z));
            StartCoroutine(goTo(toSpawnPosition, spawnPosition, un));
        }
        else
        {
            Spawn(spawnPosition);
        }



    }

    private bool CanBeSpawned(Vector3 position)
    {
        foreach (var unit in MovementController.instance.units)
        {
            if(Vector3.Distance(unit.transform.position,position) < unit.gameObject.GetComponent<Unit>().range* 0.5f)
            {
               // Debug.Log(Vector3.Distance(unit.transform.position, position) + " " + unit.name);
                return IsNotNearEnemy(position);

            }
        }

        foreach (var station in StationController.instance.stations)
        {
            if (Vector3.Distance(station.transform.position, position) < station.gameObject.GetComponent<Unit>().range * 0.5f)
            {
                return IsNotNearEnemy(position);

            }
        }

        return false;
    }

    public bool IsNotNearEnemy(Vector3 position)
    {
        foreach(var enemy in EnemyPreviewManagement.Instance.enemies)
        {
            if (Vector3.Distance(enemy.transform.position, position) < enemy.gameObject.GetComponent<Enemy>().range * 0.5f)
            {
                return false;
            }
        }
        return true;
    }

    public IEnumerator goTo(Vector3 from, Vector3 to, Unit un)
    {
      
        timer = 0;

        while (timer < jumpDuration)
        {
            timer += Time.deltaTime;
            un.transform.position = Vector3.Lerp(from, to, timer / jumpDuration);

            yield return null;
        }

        soundManager.instance.stopClip();
        transform.position = to;
    }
}
