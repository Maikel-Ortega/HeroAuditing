using StarterAssets;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour, IContradictionItem
{
    enum GoblinState
    {
        IDLE,
        CHASING,
        ATTACK,
        DEAD,
        DIALOGUE
    }

    public GameEntityDataComponent entityData;
    public GameObject aliveGraphics;
    public GameObject deadGraphics;
    public GameObject dialogInteraction;
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
        entityData.OnAlterCommand += AlterCommand;
    }

    void AlterCommand()
    {
        bool currentState = entityData.blackboard.GetBool(stateKey_Alive);
        SetGraphics(!currentState);
        entityData.blackboard.SetBool(stateKey_Alive,!currentState);
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

    public void SetAlive(bool alive)
    {
        SetGraphics(alive);
        entityData.blackboard.SetBool(stateKey_Alive, alive);
    }

    public void OnDeath()
    {
        entityData.blackboard.SetBool(stateKey_Alive, false);
        SetGraphics(false);
        currentState = GoblinState.DEAD;
    }


    void Update()
    {
        if (DialogManager.Instance.dialogRunning)
        {
            agent.isStopped = true;
            return;
        }

        if (currentState == GoblinState.IDLE)
        {
            agent.isStopped = true;
            if(Vector3.Distance(heroReference.transform.position, gameObject.transform.position) < activationDistance)
            {
                currentState =  GoblinState.CHASING;
                agent.isStopped = false;
                animator.SetFloat("Movement", 1);
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
                animator.SetFloat("Movement", 0);
            }
        }
        if(currentState == GoblinState.ATTACK)
        {
            if (weaponController.isAttacking == false)
            {
                currentState = GoblinState.IDLE;
            }
        }
    }

    public ScriptableID GetId()
    {
        return entityData.id;
    }

    public void SetDialogueState()
    {
        currentState = GoblinState.DIALOGUE;
        dialogInteraction.SetActive(true);
        agent.isStopped = true;
        animator.SetFloat("Movement", 0);
    }
}
