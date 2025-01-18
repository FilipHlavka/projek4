using System.Collections;
using UnityEngine;

public class StationFirePoint : MonoBehaviour
{
    [SerializeField]
    protected float shootCooldown;
    [SerializeField]
    protected SupportStation station;
    [SerializeField]
    protected int accuracyOffset;

    [SerializeField]
    protected int viewAngle;
    public bool readyForFire = false;
    protected float timer;
    [SerializeField]
    protected projectileType projectileType;

    [SerializeField]
    public AudioClip shot;
    [SerializeField]
    public AudioSource source;
    bool pom = true;

    /*
        [SerializeField]
        public fireMode fireMode;*/

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        source.clip = shot;
    }

    // Update is called once per frame
    public void Update()
    {
        if (station.enemyToAttack != null)
            Fire();
    }

    private void Fire()
    {
        if (station.shouldAttackEnemy)
        {
            if (Vector3.Angle(transform.forward, station.enemyToAttack.transform.position - transform.position) <= viewAngle)
            {
                //Debug.Log("zde" + transform.name);
                LayerMask layerMask = ~LayerMask.GetMask("pointer");

                if (Physics.Raycast(transform.position, station.enemyToAttack.transform.position - transform.position, out RaycastHit hit, 1000, layerMask))
                {
                    //Debug.Log("zde" + hit.transform.name);

                    if (hit.transform == station.enemyToAttack.transform)
                    {
                        Debug.DrawRay(transform.position, station.enemyToAttack.transform.position - transform.position, Color.green);
                        readyForFire = true;
                       // Debug.Log("jsem zde");

                    }
                    else
                    {
                        readyForFire = false;

                    }
                }
                else
                {
                    readyForFire = false;


                }

                if (readyForFire)
                {
                   // Debug.Log("jsem zde");

                    inicializeFire();
                }
            }



        }
    }

    protected Vector3 GenerateFireDestination()
    {
        int x = Random.Range(-accuracyOffset, accuracyOffset);
        int z = Random.Range(-accuracyOffset, accuracyOffset);
        Vector3 destination = station.enemyToAttack.transform.position + new Vector3(x, 0, z);
        source.Play();
        return destination;
    }

    private void OnDrawGizmosSelected()
    {
        if (viewAngle > 0)
        {
            Gizmos.color = Color.yellow;
            Vector3 forward = transform.forward;

            // Draw the cone for the view angle
            Quaternion leftRayRotation = Quaternion.Euler(0, -viewAngle, 0);
            Quaternion rightRayRotation = Quaternion.Euler(0, viewAngle, 0);

            Vector3 leftRayDirection = leftRayRotation * forward;
            Vector3 rightRayDirection = rightRayRotation * forward;

            // Draw the base direction
            Gizmos.DrawRay(transform.position, forward * 5);

            // Draw the left and right bounds of the angle
            Gizmos.DrawRay(transform.position, leftRayDirection * 5);
            Gizmos.DrawRay(transform.position, rightRayDirection * 5);

            // Optionally, draw an arc
            DrawArc(forward, viewAngle, 5, 20);
        }
    }

    private void DrawArc(Vector3 forward, float angle, float radius, int segments)
    {
        Vector3 startPoint = Quaternion.Euler(0, -angle, 0) * forward * radius + transform.position;
        Vector3 previousPoint = startPoint;

        for (int i = 1; i <= segments; i++)
        {
            float currentAngle = -angle + (i * (angle * 2 / segments));
            Vector3 nextPoint = Quaternion.Euler(0, currentAngle, 0) * forward * radius + transform.position;

            Gizmos.DrawLine(previousPoint, nextPoint);
            previousPoint = nextPoint;
        }

        // Close the arc back to the center
        Gizmos.DrawLine(previousPoint, startPoint);
    }


    public void inicializeFire() {

        if (pom)
        {
            Debug.Log("jsem zde");
            StartCoroutine(inicializeCoroutine());
            pom = false;
        }

    }


    private IEnumerator inicializeCoroutine()
    {

        //spawn laseru, dodìlání logiky v laseru


        Projectile pr = Instantiate(universalDataHolder.instance.projectiles.prefs[(int)projectileType].projectile, transform.position, Quaternion.Euler(0, 0, 0));
        pr.destination = GenerateFireDestination();
        pr.transform.LookAt(pr.destination);
        pr.transform.rotation = pr.transform.rotation * Quaternion.Euler(0, -90, 90);
        pr.direction = (pr.destination - pr.transform.position).normalized;
        pr.spawnedByPlayer = true;
        pr.pom = true;

        timer = 0f;
        while (timer < shootCooldown)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        pom = true;
    }
}
