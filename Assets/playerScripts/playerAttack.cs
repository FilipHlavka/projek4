using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
   /* [SerializeField]
    public laserScriptable projectiles;*/
    public bool shouldFight = false;
    public Unit player;
    public Enemy enemy;
    public Movement playerMV;
    bool attackMode = false;
    public List<Vector3> newPositions = new List<Vector3>();
    public List<firePoint> firePoints = new List<firePoint>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = gameObject.GetComponent<Unit>();   
    }

    // Update is called once per frame
    void Update()
    {
        // sledovat a støílet na enemy + šipièka nad enemy, když po nìm støílím
        if(enemy != null)
        {
            isInRange();
        }
    }

    public void isInRange()
    {
        
        if(Vector3.Distance(enemy.transform.position,gameObject.transform.position) <= player.range)
        {
           

            if (shouldFight == false)
            {
                shouldFight = true;
                
            }

        }
        else
        {
            shouldFight = false;
            if(Vector3.Distance(enemy.transform.position, gameObject.transform.position) >= player.range*2/4)
            Follow();
        }

    }
    /*private void OnDrawGizmos()
    {
        
    }*/
    /*public void CheckForFollow() {
        bool pom = true;
        foreach (var fPoint in firePoints)
        {
            if(fPoint.readyForFire == true)
                pom = false;

        }
        if (pom)
        {
            Debug.Log("no hejbu se");

            Follow();
        }
    }*/
    public void Follow()
    {
        int i = 0;
        Vector3 difference = (enemy.transform.position - player.transform.position)/2;
        newPositions = PoziceManager.Instance.aktPosition.makeMath(enemy.transform.position - difference, enemy.attackingPlayers);
        foreach(var pl in enemy.attackingPlayers)
        {
            pl.HejbniSe(newPositions[i]);
            i++;    
        }
        newPositions.Clear();
    }
    public void stopAttacking()
    {
        if (attackMode)
        {
            enemy.attackingPlayers.Remove(playerMV);
            enemy.CheckForRemovOfPointer();
            enemy = null;
            shouldFight = false;
            attackMode = false;
        }
        
    }

    public void AttackTarget(Enemy enemy, Movement playerMV)
    {
        this.playerMV = playerMV;
        this.enemy = enemy;
        Debug.Log("né ne ne");
        shouldFight = false;
        attackMode = true;
    }
}
