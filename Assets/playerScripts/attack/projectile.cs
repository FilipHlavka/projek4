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
    int timeToLive;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void Start()
    {
        Invoke("DestroyObject",timeToLive);
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
}
