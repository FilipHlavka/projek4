using UnityEngine;
using UnityEngine.EventSystems;

public class projectile : MonoBehaviour
{
    
    [SerializeField]
    public float speed;
    public Vector3 direction;
    public Vector3 destination;
    public bool pom = false;
    [SerializeField]
    int damage;
    [SerializeField]
    int TTL;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void Start()
    {
        Invoke("DestroyObject",TTL);
    }
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    public void Update()
    {
        if (pom)
        {
            transform.position += direction * speed * Time.deltaTime;
         
        }
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.TryGetComponent<Enemy>(out Enemy en))
        {
          
           if(en.SH - damage <= 0)
           {
                damage = damage - en.SH;
                en.SH = 0;
                en.HP -= damage;
                if (en.HP <= 0)
                {
                    Destroy(en.gameObject);
                }
            }
            else
            {
                en.SH -= damage;    
            }
           Destroy(gameObject);
        }
       
        if (collision.transform.TryGetComponent<Unit>(out Unit unit))
        {
            if (unit.SH - damage <= 0)
            {
                damage = damage - unit.SH;
                unit.SH = 0;
                unit.HP -= damage;
                if (unit.HP <= 0)
                {
                    Destroy(unit.gameObject);
                }
            }
            else
            {
                unit.SH -= damage;
            }
            Destroy(gameObject);
        }
    }
}
