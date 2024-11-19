using NUnit.Framework;
using System;
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
    public bool shouldAttackPlayer = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stManager = GetComponent<EnemyStateManager>();
        EnemyPreviewManagement.Instance.AddEnemy(this);
    }
    public void ShowPointer(bool show)
    {
        Pointer.SetActive(show);
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void CheckForRemovOfPointer()
    {
        if (attackingPlayers.Count == 0)
        {
            ShowPointer(false);
        }
    }


}
