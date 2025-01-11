using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopSupportStationPart : MonoBehaviour
{
    [SerializeField]
    Slider slider;
    [SerializeField]
    supportAttack atck;
    [SerializeField]
    SupportStation station;
    [SerializeField]
    Slider ring;
    public bool isInPowerMode = false;
    float timer;
    [SerializeField]
    float cooldown;
    bool canHeal = false;
    float timer2;
    [SerializeField]
    float healInterval;

    public void isSelected(bool selected)
    {
        if (selected)
            ring.value = 1;
        else
            ring.value = 0;
    }

    public void HealPower()
    {
        StationController.instance.RemoveSupportStation();
        isInPowerMode = true;
        slider.gameObject.SetActive(true);
        slider.value = slider.maxValue;
        canHeal = true;
        StartCoroutine(Cooldown());
       // Debug.Log("jsem tu");

    }

    public void AtckPower()
    {
        StationController.instance.RemoveSupportStation();
        isInPowerMode = true;
        slider.gameObject.SetActive(true);
        atck.gameObject.SetActive(true);
        slider.value = slider.maxValue;

        StartCoroutine(Cooldown());

       
    }
    private void Update()
    {
        if (canHeal)
        {
            timer2 += Time.deltaTime;

            if (timer2 >= healInterval)
            {
                timer2 -= healInterval;

                HealUnits();
            }
        }
        
    }

    public void HealUnits()
    {
       // Debug.Log("jsem tu");

        foreach (var unit in MovementController.instance.units)
        {
            if(Vector3.Distance(unit.gameObject.transform.position,gameObject.transform.position) <= station.range / 2)
            {
                Unit u = unit.gameObject.GetComponent<Unit>();
                u.HP += (u.MaxHP / 100) * 7;
                //Debug.Log("jsem tu" + u.HP + "  " + (u.MaxHP / 100) * 5);

            }
        }
    }

    public void End()
    {
        canHeal = false;
        isInPowerMode = false;
        atck.gameObject.SetActive(false);
        station.enemyToAttack = null;
        slider.gameObject.SetActive(false);
    }

    private IEnumerator Cooldown()
    {
        timer = 0;
        while (timer <= cooldown)
        {
            timer += Time.deltaTime;
            float progress = timer / cooldown;

            slider.value = Mathf.Lerp(slider.maxValue, 0, progress);

            yield return null;
        }
            End();

    }
}
