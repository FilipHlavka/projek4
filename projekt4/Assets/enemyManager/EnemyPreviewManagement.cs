using System.Collections.Generic;
using UnityEngine;

public class EnemyPreviewManagement : MonoBehaviour
{
    public static EnemyPreviewManagement Instance;
    public List<Enemy> enemies = new List<Enemy>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Instance = this;
    }
    public void AddEnemy(Enemy en)
    {
        enemies.Add(en);
    }
    public void RemoveEnemy(Enemy en)
    {
        enemies.Remove(en);
    }
}
