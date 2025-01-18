using UnityEngine;

public class Rocket : Projectile
{
    [SerializeField]
    GameObject Explosion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
        transform.rotation *= transform.rotation * Quaternion.Euler(180, 90, -100);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    private void OnDestroy()
    {
        if(Time.timeScale != 0)
        Instantiate(Explosion,transform.position,transform.rotation);
    }
}
