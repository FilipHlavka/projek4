using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.AI;
using UnityEngine.AI;
using System;
using UnityEngine.UI;

public class MoneyGenerator : MonoBehaviour
{
    public static MoneyGenerator instance;
    public int currency;
  
    [SerializeField]
    public int Max;
    public int byHowMany;
    public bool pom = true;
    public int bonusByHowMany;
    [SerializeField]
    TMP_Text Text;
    public float cooldown;
    public float bonusCooldown = 1;
    float timer = 0f;
    public bool isRunning = false;
   
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartCoroutine(AddCurrency());
        Text.text = "Cash: " + currency + " $";
        

    }

    // Update is called once per frame
    void Update()
    {
        
            if (!isRunning)
            {
                isRunning = true;
                StartCoroutine(AddCurrency());
            }

        if (Input.GetKeyDown(KeyCode.M))
        {
            currency = Max - 1;
        }

        
    }
    IEnumerator AddCurrency()
    {
        timer = 0f;

        while (timer < cooldown * bonusCooldown)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        if(currency + byHowMany <= Max)
        ChangeText(currency + byHowMany);
        isRunning = false;

    }
    public void ChangeText(int after)
    {
  
        Text.text = "Cash: " + after +" $";
        currency = after;

    }
 
}
