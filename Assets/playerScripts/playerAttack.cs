using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    [SerializeField]
    public laserScriptable projectiles;
    public bool shouldFight = false;
    public Unit player;
    public Enemy enemy;
    public Movement playerMV;
    bool attackMode = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = gameObject.GetComponent<Unit>();   
    }

    // Update is called once per frame
    void Update()
    {
        // sledovat a st��let na enemy + �ipi�ka nad enemy, kdy� po n�m st��l�m
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
        }

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
        Debug.Log("n� ne ne");
        shouldFight = false;
        attackMode = true;
    }
}
