using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class EnemyAI : MonoBehaviour
{
    public AudioClip[] muertesBrocoli;
    public GameObject particulasBrocoli;
    public AudioClip aggroBrocoli;
    private AudioSource audiosource;
    public int puntos = 20;
    public enum EnemyState { Patrol, Chase, Dead}
    
    
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


    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();  
    }

    void Start()
    {
        SetState(EnemyState.Patrol);
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
        if (currentState == newState) return; 

        currentState = newState;

        switch (currentState)
        {
            case EnemyState.Patrol:
                agent.speed = patrolSpeed;
                Debug.Log("Te he perdido");

                break;
            case EnemyState.Chase:
                agent.speed = chaseSpeed;
                PlayAggro();
                Debug.Log("Te sigo");
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
            SetState(EnemyState.Chase);
    }

    public void ResetToPatrol()
    {
        SetState(EnemyState.Patrol);
        GoToNextPoint();
    }
    public void EnemigoDisparado()
    {
        SetState(EnemyState.Dead);
        GameManager.instance.scoreManager.AddPoints(puntos);
        if (particulasBrocoli != null)
        {
            Instantiate(particulasBrocoli, transform.position, Quaternion.identity);
        }


        if (muertesBrocoli.Length > 0)
        {
            int randomIndex = Random.Range(0, muertesBrocoli.Length);
            AudioSource.PlayClipAtPoint(muertesBrocoli[randomIndex], transform.position);
        }



        gameObject.SetActive(false);
        //GameManager.instance.muerteEnemigo(this);

    }
    void PlayAggro()
    {
        if (aggroBrocoli != null && audiosource != null)
        {
            audiosource.PlayOneShot(aggroBrocoli);
        }
    }
}