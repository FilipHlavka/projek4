using System.Collections;
using System.Data;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public int HP;
    [SerializeField]
    public int MaxHP;
    MeshRenderer meshRenderer;
    public int SH;
    [SerializeField]
    public int MaxShields;
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
    float timer;
    float captureTime;
    [SerializeField]
    AudioSource source;
    [SerializeField]
    MeshRenderer shield;
    public void Start()
    {
        
        shield.enabled = false;
        meshRenderer = gameObject.GetComponent<MeshRenderer>();    
        HP = MaxHP;
        SH = MaxShields;
        agent =  gameObject.GetComponent<NavMeshAgent>();
        agent.speed = Speed;
    }

    public void ActivateShield(float time, AudioClip clip)
    {
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
      

        timer = 0;
        while (timer <= captureTime)
        {
            timer += Time.deltaTime;

            yield return null;
        }
        Debug.Log("konec");
        isInvincible = false;
        meshRenderer.enabled = true;
        shield.enabled = false;
    }
}
