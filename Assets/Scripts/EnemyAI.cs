using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState { Patrol, Chase }

    
    [Header("REFERENCIAS")]
    public NavMeshAgent agent;
    public Transform player;
    public Transform[] patrolPoints;

    [Header("DETECCIÓN")]
    public float detectionRadius;
    public float losePlayerDistance;
    [SerializeField] LayerMask vision;

    [Header("VELOCIDADES")]
    public float patrolSpeed = 5f;
    public float chaseSpeed = 10f;
    

    private EnemyState currentState;
    private int currentPatrolIndex = 0;

  


    void Start()
    {
        currentState = EnemyState.Patrol;
        GoToNextPoint();
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrol:
                Patrol();
                DetectPlayer();
                break;

            case EnemyState.Chase:
                ChasePlayer();
                break;
        }
    }
    void SetState(EnemyState newState)
    {
        if (currentState == newState) return; // si ya está en ese estado, no hacemos nada

        currentState = newState;

        switch (currentState)
        {
            case EnemyState.Patrol:
                agent.speed = patrolSpeed;
                break;
            case EnemyState.Chase:
                agent.speed = chaseSpeed;
                break;
        }
    }

    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GoToNextPoint();
    }

    void GoToNextPoint()
    {
        if (patrolPoints.Length == 0) return;
        agent.destination = patrolPoints[currentPatrolIndex].position;
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    void DetectPlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float distance = directionToPlayer.magnitude;

        if (distance <= detectionRadius)
        {
            RaycastHit hit;
           
            if (Physics.Raycast(transform.position + Vector3.up * 1.5f, directionToPlayer.normalized, out hit, detectionRadius,vision))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    
                    SetState(EnemyState.Chase);
                }
                else
                {
                    
                    SetState(EnemyState.Patrol);
                }
            }
            else
            {
               
                SetState(EnemyState.Patrol);
            }
        }
        
    }

    void ChasePlayer()
    {
        agent.destination = player.position;
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > losePlayerDistance)
            currentState = EnemyState.Patrol;
    }

}