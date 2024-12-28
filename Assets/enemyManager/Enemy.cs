using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject Pointer;
    public EnemyStateManager stManager;
    public List<Movement> attackingPlayers = new List<Movement>();
    public List<firePoint> firePoints = new List<firePoint>();
    public Movement playerToAttack;
    public StationMovement attackingStation;
    public bool shouldAttackPlayer = false;
    [SerializeField]
    public int range;
    [SerializeField]
    public int HP;
    [SerializeField]
    public int SH;
    public bool isStation;
    [SerializeField]
    public GameObject destroyExplosion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stManager = GetComponent<EnemyStateManager>();
        EnemyPreviewManagement.Instance.AddEnemy(this);
        InvokeRepeating("ClearList",3,3);
    }
    public void ShowPointer(bool show)
    {
        if(Pointer != null)
        Pointer.SetActive(show);
    }

    // Update is called once per frame
    void Update()
    {
        CheckForRemovOfPointer();
    }

    public virtual void OnDestroy()
    {
        if (destroyExplosion != null && Application.isPlaying)
            Instantiate(destroyExplosion,transform.position,transform.rotation);
    }

    public void CheckForRemovOfPointer()
    {

        if (attackingPlayers.Count == 0 && Pointer.activeInHierarchy && attackingStation == null)
        {
            ShowPointer(false);
        }
    }

    public void CallForHelp(Vector3 toWhere)
    {
       
        foreach(var en in EnemyPreviewManagement.Instance.enemies)
        {
            if(en.stManager.CurrentState is not AttackState && !en.isStation && en.stManager.CurrentState is not HelpState)
            {
                en.stManager.ChangeState(new HelpState());
                
                en.stManager.CurrentState.GoTo(toWhere);
            }
        }
        // udìlat help state
    }

    public void noHelpNeeded()
    {

        foreach (var en in EnemyPreviewManagement.Instance.enemies)
        {
            if (en.stManager.CurrentState is HelpState)
            {
                en.stManager.ChangeState(new IdleState());
               
            }
        }
        // udìlat help state
    }

    private void ClearList()
    {
        int j = 0;
        for (int i = 0; i < attackingPlayers.Count -j; i++)
        {
            if (attackingPlayers[i] == null)
            {
                attackingPlayers.RemoveAt(i);
                j++;
            }
        }
    }

}
