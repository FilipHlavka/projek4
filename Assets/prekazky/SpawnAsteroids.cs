using System.Threading;
using UnityEngine;

public class SpawnAsteroids : MonoBehaviour
{
    [SerializeField]
    Collider Collider;
    [SerializeField]
    GameObject prefab;
    public int prefCount;
    void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        for (int i = 0; i <= prefCount; i++)
        {
            Bounds bounds = Collider.bounds;


            float x = Random.Range(bounds.min.x, bounds.max.x);
            float z = Random.Range(bounds.min.z, bounds.max.z);

            float xRotation = Random.Range(0, 360);
            float yRotation = Random.Range(0, 360);
            float zRotation = Random.Range(0, 360);

            Instantiate(prefab,new Vector3(x,0,z),Quaternion.Euler(xRotation,yRotation,zRotation),Collider.gameObject.transform);
        }
       
    }
}
