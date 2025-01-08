using UnityEngine;
using UnityEngine.UI;

public class EndScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Button bt = gameObject.GetComponent<Button>();

        bt.onClick.AddListener(() => Application.Quit());   
    }

  
}
