using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public List<Button> list;

    // Start is called before the first frame update
    public string Map;
    void Start()
    {
        foreach (var bt in list)
        {
            bt.onClick.AddListener(() => { Map = bt.name; });
        }
    }
    // ingamemanager.instance.PrepniNascenu( tlac.name ,true);
    // Update is called once per frame

    public void startGame()
    {
        if (Map != "")
            SceneLoader.Instance.Load(Map);
    }
}

