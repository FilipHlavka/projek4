using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStateManager stManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stManager = GetComponent<EnemyStateManager>();
        EnemyPreviewManagement.Instance.AddEnemy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
