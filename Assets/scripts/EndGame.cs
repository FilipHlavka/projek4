using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField]
    GameObject panelWin;
    [SerializeField]
    GameObject panelLose;
    public static EndGame instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        panelLose.SetActive(false);
        panelWin.SetActive(false);
    }

    public void EndThisGood()
    {
        if (Time.timeScale != 0f)
        {
            panelWin.SetActive(true);
            Invoke("SwitchScenes", 5);
        }
       
    }

    public void EndThisBad()
    {
        if (Time.timeScale != 0f)
        {
            panelLose.SetActive(true);
            Invoke("SwitchScenes", 5);
        }
        
    }

    public void SwitchScenes()
    {
        Time.timeScale = 0f;
        SceneLoader.Instance.Load("Maps");
    }
}
