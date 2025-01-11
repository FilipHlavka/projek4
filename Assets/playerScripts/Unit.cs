using System.Collections;
using System.Data;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    [Header("HP")]
    private float hp;
    public float HP
    {
        get { return hp; }
        set
        {
            hp = Mathf.Clamp(value, 0, MaxHP);
            HPslider.value = hp;
            if (hp == 0)
                Destroy(gameObject);
            if (hp < MaxHP/2.7)
                healthImage.color = Color.red;
            else 
                healthImage.color = Color.green;

        }
    }
    [SerializeField]
    public float MaxHP;
    [SerializeField]
    Slider HPslider;

    MeshRenderer meshRenderer;


    [Header("Def")]
    private int sh;
    [SerializeField]
    public int MaxShields;
    public int SH 
    { 
        get { return sh; }
        set 
        {
            sh = Mathf.Clamp(value, 0, MaxShields);
            SHslider.value = sh;
        } 
    }
    
    [SerializeField]
    Slider SHslider;

    [Header("Slider")]
    [SerializeField]
    Image healthImage;
    [SerializeField]
    public int range;
    [SerializeField]
    public float Speed;
    NavMeshAgent agent;
    public bool isStation = false;
    [SerializeField]
    public string Name;
    [SerializeField]
    public AudioClip spawn;
    [SerializeField]
    public GameObject explosion;
    public bool isInvincible = false;
    float timer2;
    float captureTime;
    [SerializeField]
    public AudioSource source;
    [SerializeField]
    MeshRenderer shield;
    [SerializeField]
    public Enemy enemyToAttack;





    public void Start()
    {
        SHslider.maxValue = MaxShields;
        HPslider.maxValue = MaxHP;
        SHslider.value = MaxShields;
        HPslider.value = MaxHP;



        shield.enabled = false;
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        HP = MaxHP;
        SH = MaxShields;
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.speed = Speed;
    }

    public void PlayClip(AudioClip clip)
    {
        source.Stop();
        source.clip = clip;
        source.Play();

    }
   

    public void ActivateShield(float time, AudioClip clip)
    {
        SH = MaxShields;
        source.clip = clip;
        source.Play();
        shield.enabled = true;
        meshRenderer.enabled = false;
        isInvincible = true;
        captureTime = time;
        Debug.Log("zaèátek");
        StartCoroutine(CapturCoroutine());
    }
    protected void OnDestroy()
    {
        Movement mv = gameObject.GetComponent<Movement>();
        MovementController.instance.units.Remove(mv);
        if(MovementController.instance.selectedUnits.Contains(mv))
            MovementController.instance.selectedUnits.Remove(mv);

        if(playerAttackController.instance.selectedUnits.Contains(mv))
            playerAttackController.instance.selectedUnits.Remove(mv);

        if (Time.timeScale != 0)
            Instantiate(explosion,transform.position,transform.rotation);
    }

    private IEnumerator CapturCoroutine()
    {
      

        timer2 = 0;
        while (timer2 <= captureTime)
        {
            timer2 += Time.deltaTime;

            yield return null;
        }
        Debug.Log("konec");
        isInvincible = false;
        meshRenderer.enabled = true;
        shield.enabled = false;
    }
}
