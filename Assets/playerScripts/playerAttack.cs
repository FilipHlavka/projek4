using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
   /* [SerializeField]
    public laserScriptable projectiles;*/
    public Unit player;
    public Movement playerMV;
    bool attackMode = false;
    public List<Vector3> newPositions = new List<Vector3>();
    public List<firePoint> firePoints = new List<firePoint>();
    public bool shouldFight = false;
    public Enemy enemy;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        player = gameObject.GetComponent<Unit>();   
    }

    // Update is called once per frame
    public void Update()
    {
        // sledovat a støílet na enemy + šipièka nad enemy, když po nìm støílím
        if(enemy != null)
        {
            isInRange();
        }
    }

    public virtual void isInRange()
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
            if(Vector3.Distance(enemy.transform.position, gameObject.transform.position) >= player.range* 0.5f)
            Follow();
        }

        CheckForRotation();
    }
    /*private void OnDrawGizmos()
    {
        
    }*/
    public virtual void CheckForRotation() {
        // bool pom = true;
        int pom = 0;
        foreach (var fPoint in firePoints)
        {
            if(fPoint.readyForFire == true)
                pom++;

           
        }
        if (pom < firePoints.Count/2)
        {
            Debug.Log("no hejbu se");

            LookAtEnemy();
        }

    }
    public virtual void OnDestroy()
    {
        if(enemy != null)
        {
           
            enemy.attackingPlayers.Remove(playerMV);

        }
    }
    public virtual void LookAtEnemy()
    {
        Vector3 direction = enemy.transform.position - transform.position;    
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 4f);

        
    }
   
    public virtual void Follow()
    {
        int i = 0;
        Vector3 difference = (enemy.transform.position - player.transform.position)/2;
        newPositions = PoziceManager.Instance.aktPosition.makeMath(enemy.transform.position - difference, enemy.attackingPlayers);
        foreach(var pl in enemy.attackingPlayers)
        {
            if(pl != null)
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
           //enemy.CheckForRemovOfPointer();
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
