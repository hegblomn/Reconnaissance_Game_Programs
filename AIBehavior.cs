using UnityEngine;
using UnityEngine.AI;

public class AIBehavior : MonoBehaviour
{
    public enum AIState {Idle, Attack, Moving}
    public AIState currentState = AIState.Idle;

    NavMeshAgent agent;
    public bool isAttackComplete = false;

    [Header("Navigation Settings")]
    public Transform[] moveSpots;
    public int currentSpotIndex = 0;

    [Header("Animation Settings")]
    Animator animator;
    

    public AIDialogue dialogue;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        dialogue = dialogue != null ? dialogue : GetComponent<AIDialogue>();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IdleState();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case AIState.Idle:        UpdateIdle();
                break;
            case AIState.Attack: UpdateAttack();
                break;
            case AIState.Moving:      UpdateMoving();
                break;
        }
    }

    void IdleState()
    {
        currentState = AIState.Idle;
        agent.isStopped = true;
        agent.ResetPath();
        SetAnim("Idle");
    }

    void UpdateIdle()
    {
        
    }

    public void Attack()
    {
        if (!dialogue.HasDialogueRemaining()) return;
        currentState = AIState.Attack;
        agent.isStopped = true;
        SetAnim("Talking");
        isAttackComplete = false;
        dialogue.StartDialogue(OnDialogueComplete);
    }

    void UpdateAttack()
    {
    
    }   

    void OnDialogueComplete()
    {
        if (moveSpots != null && moveSpots.Length > 0)
            MovingState();
        else
            IdleState();
    }

    void MovingState()
    {
        currentState = AIState.Moving;
        agent.isStopped = false;
        SetAnim("Moving");

        currentSpotIndex = (currentSpotIndex + 1) % moveSpots.Length;
        agent.SetDestination(moveSpots[currentSpotIndex].position);
    }

    void UpdateMoving()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude < 0.01f)
                IdleState();
        }
    }

    void SetAnim(string trigger)
    {
        if (!animator) return;
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Moving");
        animator.ResetTrigger("Talking");
        animator.SetTrigger(trigger);
    }

    public bool IsInteractable() => currentState == AIState.Idle;
}
