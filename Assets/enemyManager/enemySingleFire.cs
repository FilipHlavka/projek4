using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Events;

public class enemySingleFire : enemyFirePoint
{
    bool pom = true;
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

    public override void inicializeFire()
    {

        if (pom)
        {
            StartCoroutine(inicializeCoroutine());
            pom = false;
        }

    }



    private IEnumerator inicializeCoroutine()
    {

        //spawn laseru, dodìlání logiky v laseru


        Projectile pr = Instantiate(universalDataHolder.instance.projectiles.prefs[(int)projectileType].projectile, transform.position, Quaternion.Euler(0, 0, 0));
        pr.destination = GenerateFireDestination();
        pr.transform.LookAt(pr.destination);
        pr.transform.rotation = pr.transform.rotation * Quaternion.Euler(0, -90, 90);
        pr.direction = (pr.destination - pr.transform.position).normalized;

        pr.pom = true;

        timer = 0f;
        while (timer < shootCooldown)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        pom = true;
    }
}
