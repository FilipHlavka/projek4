using UnityEngine;

public class enemyFirePoint : MonoBehaviour
{
    [SerializeField]
    protected float shootCooldown;
    [SerializeField]
    protected Enemy enemy;
    [SerializeField]
    protected int accuracyOffset;

    [SerializeField]
    protected int viewAngle;
    public bool readyForFire = false;
    protected float timer;
    [SerializeField]
    protected projectileType projectileType;

    /*
        [SerializeField]
        public fireMode fireMode;*/

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        if(enemy.playerToAttack != null)
        Fire();
    }

    private void Fire()
    {
        if (enemy.shouldAttackPlayer)
        {
            if (Vector3.Angle(transform.forward, enemy.playerToAttack.transform.position - transform.position) <= viewAngle)
            {
                //Debug.Log("zde" + transform.name);

                if (Physics.Raycast(transform.position, enemy.playerToAttack.transform.position - transform.position, out RaycastHit hit))
                {
                    //Debug.Log("zde" + hit.transform.name);

                    if (hit.transform == enemy.playerToAttack.transform)
                    {
                        Debug.DrawRay(transform.position, enemy.playerToAttack.transform.position - transform.position, Color.green);
                        readyForFire = true;
                        //Debug.Log("haaaaaaaaaaaloooooo");
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

                    inicializeFire();
                }
            }



        }
    }

    protected Vector3 GenerateFireDestination()
    {
        int x = Random.Range(-accuracyOffset, accuracyOffset);
        int z = Random.Range(-accuracyOffset, accuracyOffset);
        Vector3 destination = enemy.playerToAttack.transform.position + new Vector3(x, 0, z);

        return destination;
    }

    public virtual void inicializeFire() { }
}

