using UnityEngine;

public class universalDataHolder : MonoBehaviour
{

    public static universalDataHolder instance;
    [SerializeField]
    public laserScriptable projectiles;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
