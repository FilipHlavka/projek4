using UnityEngine;

public class playerStation : Unit
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    float regenCooldown;
    float timer;
    public void Start()
    {
        base.Start();

    }

    void ShieldRegen()
    {
        SH++;
       
    }

    private void Update()
    {
        if (timer < regenCooldown) { 
            timer += Time.deltaTime;
            
        }
        else
        {
            ShieldRegen();

            timer = 0;
        }

    }

    public new void OnDestroy()
    {
        StationController.instance.stations.Clear();
        if(StationController.instance.panel != null)
        StationController.instance.panel.SetActive(false);

        if (Time.timeScale != 0)
            Instantiate(explosion, transform.position, transform.rotation);

    }
}
