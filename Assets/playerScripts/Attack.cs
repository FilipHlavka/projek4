using UnityEngine;

public class StationAttack : playerAttack
{
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override void isInRange()
    {
        Debug.Log(enemy.transform.position + gameObject.transform.position);
        if (Vector3.Distance(enemy.transform.position, gameObject.transform.position) <= player.range)
        {


            if (shouldFight == false)
            {
                shouldFight = true;

            }

        }
        else
        {
            shouldFight = false;
         
        }

       
    }
}
