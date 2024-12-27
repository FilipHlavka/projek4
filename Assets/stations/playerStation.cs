using UnityEngine;

public class playerStation : Unit
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        base.Start();

    }

    public new void OnDestroy()
    {
        StationController.instance.stations.Clear();
        if(StationController.instance.panel != null)
        StationController.instance.panel.SetActive(false);
    }
}
