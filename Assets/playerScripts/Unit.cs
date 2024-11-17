using System.Data;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public int HP;
    [SerializeField]
    public int MaxHP;

    public int Shields;
    [SerializeField]
    public int MaxShields;
    [SerializeField]
    public int range;
    [SerializeField]
    public float Speed;
    NavMeshAgent agent;

    public void Start()
    {
        HP = MaxHP;
        Shields = MaxShields;
        agent =  gameObject.GetComponent<NavMeshAgent>();
        agent.speed = Speed;
    }
}
