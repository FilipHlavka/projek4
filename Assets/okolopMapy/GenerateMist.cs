using UnityEngine;

public class GenerateMist : MonoBehaviour
{
    [SerializeField]
    Terrain tr;


    ParticleSystem mist;

    public bool x;
    public bool y;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mist = GetComponent<ParticleSystem>();
        if (x)
        {
            var shape = mist.shape;
            shape.scale = new Vector3(tr.terrainData.size.x, 60, 2);

        }
        if (y)
        {
            var shape = mist.shape;
            shape.scale = new Vector3(60, tr.terrainData.size.y, 2);

        }
    }

}
