using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;
using System;
using UnityEngine.UI;
public class MineStation : MonoBehaviour
{
    [SerializeField]
    Slider playerSlider;

    [SerializeField]
    Slider enemySlider;

    [SerializeField]
    public List<Unit> units = new List<Unit>();
    [SerializeField]
    public List<Enemy> Enemies = new List<Enemy>();

    [SerializeField]
    float captureTime = 3;
    float timer = 0;
    float elTimer = 0;
    bool isRunning = false;
    public bool belongsToPlayer = false;
    public bool alreadyBelongsToPlayer = false;
    public bool alreadyBelongsToEnemy = false;
    public bool hasBeenCapturedByPlayerBefore = false;
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(CheckCoroutine());
        playerSlider.maxValue = captureTime;
        enemySlider.maxValue = captureTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<Unit>(out Unit unit) ||
       (unit = other.transform.GetComponentInParent<Unit>()) != null)
        {

            if (!units.Contains(unit))
            units.Add(unit);
            CheckForCapture();
        }

       
        if (other.transform.TryGetComponent<Enemy>(out Enemy enemy) ||
            (enemy = other.transform.GetComponentInParent<Enemy>()) != null)
        {
            
            if (!Enemies.Contains(enemy))
            Enemies.Add(enemy);
            CheckForCapture();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.TryGetComponent<Unit>(out Unit unit) ||
         (unit = other.transform.GetComponentInParent<Unit>()) != null)
        {
            if (units.Contains(unit))
            {
                units.Remove(unit);
                CheckForCapture();
            }
        }

        if (other.transform.TryGetComponent<Enemy>(out Enemy enemy) ||
            (enemy = other.transform.GetComponentInParent<Enemy>()) != null)
        {
            if (Enemies.Contains(enemy))
            {
                Enemies.Remove(enemy);
                CheckForCapture();
            }
        }
    }

    private void CheckForCapture()
    {
        if (Enemies.Count == 0 && units.Count == 0) { return; }
        else
        {
            if (Enemies.Count < units.Count)
                belongsToPlayer = true;
            else
                belongsToPlayer = false;

            if ((belongsToPlayer != alreadyBelongsToPlayer || !belongsToPlayer != alreadyBelongsToEnemy) && !isRunning)
                StartCoroutine(CapturCoroutine());
        }

       
    }

    private IEnumerator CapturCoroutine()
    {
        isRunning = true;
        if (belongsToPlayer)
            enemySlider.value = 0;
        else
            playerSlider.value = 0;

       

        timer = 0;
        while (timer <= captureTime + 0.3f)
        {
            timer += Time.deltaTime;

            elTimer += Time.deltaTime;
            if (elTimer >= 1f)
            {
                elTimer = 0f;

                if (belongsToPlayer)
                    playerSlider.value = timer;
                else
                    enemySlider.value = timer;
            }

            yield return null;
        }
        alreadyBelongsToPlayer = belongsToPlayer;
        alreadyBelongsToEnemy = !belongsToPlayer;
        isRunning = false;
        if (belongsToPlayer)
        {
            playerSlider.value = playerSlider.maxValue;
            hasBeenCapturedByPlayerBefore = true;
        }
        else
            enemySlider.value = enemySlider.maxValue;

        if (belongsToPlayer)
            MoneyGenerator.instance.bonusCooldown *= 0.3f;
        else if(hasBeenCapturedByPlayerBefore)
            MoneyGenerator.instance.bonusCooldown /= 0.3f;

    }

    private IEnumerator CheckCoroutine()
    {

       


        yield return new WaitForSeconds(3f);

        for (int i = 0; i < units.Count; i++)
        {
            if (units[i] == null)
            {
                units.RemoveAt(i);
                CheckForCapture();
                i--;
            }
          
        }
        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i] == null)
            {
                Enemies.RemoveAt(i);
                CheckForCapture();
                i--;
            }

        }

        StartCoroutine(CheckCoroutine());
    }

}
