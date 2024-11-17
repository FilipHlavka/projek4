using UnityEngine;

public class Pauza : MonoBehaviour
{
    bool stopped = false;
    public static Pauza pauza;
    [SerializeField]
    GameObject panel;

    public bool canChange = true;

    private void Awake()
    {
        pauza = this;
    }
    // Update is called once per frame
    void Update()
    {



        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!stopped)
            {
                Stop();
            }
            else
            {
                UnStop();

            }
        }
    }

    public void ChangePozice(int id)
    {
        PoziceManager.Instance.Changed(id);
    }

    public void Stop()
    {
        canChange = false;
        Time.timeScale = 0;
        panel.SetActive(true);
        stopped = true;
    }
    public void UnStop()
    {
        canChange = true;
        panel.SetActive(false);
        Time.timeScale = 1f;
        stopped = false;
    }
}
