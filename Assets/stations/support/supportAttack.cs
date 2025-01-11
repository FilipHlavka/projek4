using UnityEngine;

public class supportAttack : MonoBehaviour
{
    bool idle = false;
    float smallestDistance = float.MaxValue;
    private float scanCooldown = 1.5f;
    private float timer = 0;
    [SerializeField]
    SupportStation station;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= scanCooldown)
        {
            timer = 0f;
            ScanForUnits();
            
        }
    }

    public void ScanForUnits()
    {
        ChangeTarget();
        idle = true;
        // enemak bude utocit na nejblizsi jednotku, pokud na nej budou utocit unity s celkovim poctem zivotu veci nez 1 a pul zivotu enemy, enemy zavola o pomoc
        foreach (var m in EnemyPreviewManagement.Instance.enemies)
        {
            if (Vector3.Distance(m.transform.position, station.transform.position) < station.range)
            {
                idle = false;
                break;
            }
        }
        foreach (var m in EnemyPreviewManagement.Instance.enemies)
        {

            if (Vector3.Distance(m.transform.position, station.transform.position) < station.range)
            {
                idle = false;
                break;
            }
        }

    }

    public void ChangeTarget()
    {
        smallestDistance = float.MaxValue;
        Enemy closestUnit = null;
        foreach (var m in EnemyPreviewManagement.Instance.enemies)
        {
            if (Vector3.Distance(m.transform.position, station.transform.position) < station.range)
            {
                if (Vector3.Distance(m.transform.position, station.transform.position) < smallestDistance)
                {
                    smallestDistance = Vector3.Distance(m.transform.position, station.transform.position);
                    closestUnit = m;
                }

            }
        }

        foreach (var m in EnemyPreviewManagement.Instance.enemies)
        {
            if (Vector3.Distance(m.transform.position, station.transform.position) < station.range)
            {
                if (Vector3.Distance(m.transform.position, station.transform.position) < smallestDistance)
                {
                    smallestDistance = Vector3.Distance(m.transform.position, station.transform.position);
                    closestUnit = m;
                }

            }
        }

        // Debug.Log("nooo: " + closestUnit);
        station.enemyToAttack = closestUnit;
        station.shouldAttackEnemy = true;
    }
}
