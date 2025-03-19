using UnityEngine;

public class EnemyStation : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    float regenCooldown;
    float timer;
    void Start()
    {
        base.Start();
    }

    void ShieldRegen()
    {
        SH++;

    }

    // Update is called once per frame
    void Update()
    {
        base.Update();


        if (timer < regenCooldown)
        {
            timer += Time.deltaTime;

        }
        else
        {
            ShieldRegen();

            timer = 0;
        }
    }

    
}
