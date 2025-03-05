
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemySpawn : MonoBehaviour
{
    public static EnemySpawn instance;

    [SerializeField]
    EnemyScriptable enemies;
    [SerializeField]
    GameObject SpawnGameObject;
    [SerializeField]
    public Terrain SpawnTerrain;
    Bounds spawnBounds;
    TerrainData data;
    public bool canSpawn = true;
    public float SpawnCooldown;
    float timer;
    [SerializeField]
    float jumpDuration;
    


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        data = SpawnTerrain.terrainData;
        spawnBounds = data.bounds;
        StartCoroutine(SpawnCoroutine());
    }

    
    void Update()
    {
        //Debug.Log(Random.Range(0, enemies.Count));
    }
    
    public void ActuallySpawn(Vector3 spawnPosition)
    {


        //spawnPosition = new Vector3(spawnPosition.x,29,spawnPosition.z);
        Quaternion rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));

        Vector3 axis = rotation * Vector3.back;


        Vector3 toSpawnPosition = spawnPosition + axis * 20;

        Debug.Log(toSpawnPosition);
        Debug.Log(spawnPosition);

        if (NavMesh.SamplePosition(toSpawnPosition, out NavMeshHit hit, 1, NavMesh.AllAreas))
        {
            
           
            Enemy enemy = Instantiate(
            enemies.prefs[Random.Range(0, enemies.prefs.Count)].enemy,
            toSpawnPosition,
            rotation, SpawnGameObject.transform);
            enemy.PlayClip(enemy.clip);
            //Debug.Log(new Vector3(toSpawnPosition.x, 29, toSpawnPosition.z) + "    " + new Vector3(spawnPosition.x, 29, spawnPosition.z));
            StartCoroutine(goTo(toSpawnPosition, spawnPosition, enemy));
        }
        else
        {
            ActuallySpawn(spawnPosition);
        }



    }
    private void Spawn()
    {
        Debug.Log("spawn");
        
        Enemy NearEnemy = EnemyPreviewManagement.Instance.enemies[Random.Range(0, EnemyPreviewManagement.Instance.enemies.Count)].GetComponent<Enemy>();
        Debug.Log(NearEnemy.name);
        float x = Random.Range(NearEnemy.transform.position.x - NearEnemy.range * 0.7f, NearEnemy.transform.position.x + NearEnemy.range * 0.7f);
        float z = Random.Range(NearEnemy.transform.position.z - NearEnemy.range * 0.7f, NearEnemy.transform.position.z + NearEnemy.range * 0.7f);
        float y = SpawnTerrain.transform.position.y;

        Vector3 position = new Vector3(x, y, z);

        if (NavMesh.SamplePosition(position, out NavMeshHit hit, 1, NavMesh.AllAreas))
        {
            if (IsNotNearPlayer(position))
                ActuallySpawn(position);
        }
        else
        {
            Spawn();
            return;
        }

           
    }

   
    public bool IsNotNearPlayer(Vector3 position)
    {
        foreach (var unit in MovementController.instance.units)
        {
            if (Vector3.Distance(unit.transform.position, position) < unit.gameObject.GetComponent<Unit>().range * 0.5f)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnCoroutine()
    {


        while (canSpawn)
        {
            yield return new WaitForSeconds(SpawnCooldown);
            if(Difficulty.Instance != null)
            for (int i = 0;i< Difficulty.Instance.diff + 1; i++)
            {
                if (canSpawn && EnemyPreviewManagement.Instance.enemies.Count != 0)
                    Spawn();
            }
           

        }
    }

    public IEnumerator goTo(Vector3 from, Vector3 to, Enemy en)
    {

        timer = 0;

        while (timer < jumpDuration)
        {
            timer += Time.deltaTime;
            en.transform.position = Vector3.Lerp(from, to, timer / jumpDuration);

            yield return null;
        }

        en.source.Stop();
        en.isInAnimation = false;
        transform.position = to;
    }
    
}
