using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class ShieldPower : MonoBehaviour
{
    [SerializeField]
    Button Button;
    [SerializeField]
    float cooldown;
    [SerializeField]
    float baseCooldown;
    public static ShieldPower instance;
    public List<Movement> selectedUnits = new List<Movement>();
    float timer;
    float elTimer;
    [SerializeField]
    Slider slider;
    [SerializeField]
    float howLong;
    [SerializeField]
    AudioClip clip;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Button.onClick.AddListener(ActivateShields);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateShields()
    {
        selectedUnits = MovementController.instance.selectedUnits;

        foreach (Movement m in selectedUnits)
        {

            m.GetComponent<Unit>().ActivateShield(howLong,clip);
        }
        if (selectedUnits.Count > 0)
        {
          
            cooldown = baseCooldown * (selectedUnits.Count * 0.2f) + baseCooldown;
            Button.interactable = false;
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        timer = 0;
        while (timer <= cooldown)
        {
            timer += Time.deltaTime;
            float progress = timer / cooldown;

            slider.value = Mathf.Lerp(slider.maxValue,0,progress);

            yield return null;
        }
        Button.interactable = true;
    }
}