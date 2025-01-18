using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    Image healthImage;

    [Header("HP")]
    private int hp;
    public int HP
    {
        get { return hp; }
        set
        {
            hp = Mathf.Clamp(value, 0, MaxHP);
            HPslider.value = hp;
            if (hp == 0)
                Destroy(gameObject);
            if (hp < MaxHP / 2.7)
                healthImage.color = Color.red;

        }
    }
    [SerializeField]
    public int MaxHP;
    [SerializeField]
    Slider HPslider;




    [Header("Def")]
    private int sh;
    [SerializeField]
    public int MaxSH;
    public int SH
    {
        get { return sh; }
        set
        {
            sh = Mathf.Clamp(value, 0, MaxSH);
            SHslider.value = sh;
        }
    }

    [SerializeField]
    Slider SHslider;


    public bool isStation;
    [SerializeField]
    public GameObject destroyExplosion;
    public bool isInAnimation = true;
    [SerializeField]
    public AudioSource source;
    [SerializeField]
    public AudioClip clip;
    float elTimer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        stManager = GetComponent<EnemyStateManager>();
        EnemyPreviewManagement.Instance.AddEnemy(this);
        InvokeRepeating("ClearList",3,3);

        HP = MaxHP;
        SH = MaxSH;

        SHslider.maxValue = MaxSH;
        HPslider.maxValue = MaxHP;
        SHslider.value = MaxSH;
        HPslider.value = MaxHP;
    }
    public void ShowPointer(bool show)
    {
        if(Pointer != null)
        Pointer.SetActive(show);
    }

    // Update is called once per frame
    public void Update()
    {
        CheckForRemovOfPointer();

        
        elTimer += Time.deltaTime;
        if (elTimer >= 1f)
        {
            elTimer = 0f;
            CheckForListClear();

        }  
        
    }

    public virtual void OnDestroy()
    {
        if (Time.timeScale != 0)
            Instantiate(destroyExplosion,transform.position,transform.rotation);

        if(isStation)
            EndGame.instance.EndThisGood();

        EnemyPreviewManagement.Instance.enemies.Remove(this);
    }

    public void PlayClip(AudioClip clip)
    {
        source.Stop();
        source.clip = clip;
        source.Play();

    }
    public void CheckForRemovOfPointer()
    {

        if (attackingPlayers.Count == 0 && Pointer.activeInHierarchy && attackingStation == null)
        {
            ShowPointer(false);
        }
    }

    public void CheckForListClear()
    {
        int j = 0;
        for (int i = 0; i < attackingPlayers.Count - j; i++)
        {
            if (attackingPlayers[i].enemyToAttack != this)
            {
                attackingPlayers.RemoveAt(i);
                j++;
            }
        }
    }
    /*&& en.stManager.CurrentState is not HelpState*/

    public void CallForHelp(Vector3 toWhere)
    {
       
        foreach(var en in EnemyPreviewManagement.Instance.enemies)
        {
            if(en.stManager.CurrentState is not AttackState && !en.isStation )
            {
                en.stManager.ChangeState(new HelpState());
                
                en.stManager.CurrentState.GoTo(toWhere);
            }
        }
        
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
