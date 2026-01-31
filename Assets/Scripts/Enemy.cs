using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IContradictionItem
{
    enum GoblinState
    {
        IDLE,
        CHASING,
        ATTACK,
        DEAD
    }

    public GameEntityDataComponent entityData;
    public GameObject aliveGraphics;
    public GameObject deadGraphics;

    const string stateKey_Alive = "ALIVE";

    [Space(10)]
    public float activationDistance;
    public float attackDistance;
    public float moveSpeed;

    GoblinState currentState = GoblinState.IDLE;
    NavMeshAgent agent;
    float currentMoveSpeed;
    GameObject heroReference;
    WeaponController weaponController;
    Animator animator;

    void Awake()
    {
        Init();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.isStopped = true;
        heroReference = FindAnyObjectByType<FirstPersonController>().gameObject;
        weaponController = GetComponentInChildren<WeaponController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Init()
    {
        entityData.Init();
        entityData.blackboard.SetBool(stateKey_Alive, true);
        SetGraphics(true);
    }

    public void SetGraphics(bool alive)
    {
        aliveGraphics.SetActive(alive);
        deadGraphics.SetActive(!alive);
    }

    public void OnDeath()
    {
        entityData.blackboard.SetBool(stateKey_Alive, false);
        SetGraphics(false);
        currentState = GoblinState.DEAD;
    }

    public bool IsContradiction(Blackboard data)
    {
        return false;
    }

    void Update()
    {
        if (currentState == GoblinState.IDLE)
        {
            agent.isStopped = true;
            if(Vector3.Distance(heroReference.transform.position, gameObject.transform.position) < activationDistance)
            {
                currentState =  GoblinState.CHASING;
                agent.isStopped = false;
            }
        }
        if(currentState == GoblinState.CHASING)
        {
            agent.SetDestination(heroReference.transform.position);
            if(Vector3.Distance(heroReference.transform.position, gameObject.transform.position) < attackDistance)
            {
                currentState = GoblinState.ATTACK;
                agent.isStopped = true;
                weaponController.Attack();
                animator.SetTrigger("Attack");
            }
        }
        if(currentState == GoblinState.ATTACK)
        {
            if (weaponController.isAttacking == false)
                currentState = GoblinState.IDLE;
        }
    }
}
