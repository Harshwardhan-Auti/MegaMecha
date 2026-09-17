
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float currentDistance;

    [Header("Health")]
    [SerializeField] private float enemyHealth = 100;

    [Header("Patrol")]
    [SerializeField] private float patrolingRadius = 10f;


    [Header("Attack")]
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float damamge = 100f;
    [SerializeField] private float attackExitBuffer = 0.25f;
    [SerializeField] private float attackCoolDown = 1f;
    [SerializeField] private float nextAttackTime = 0f;

    [Header("Detection")]
    [SerializeField] private float detectionRange = 6f;
    private bool isDetected = false;


    private NavMeshAgent enemyAgent;





    [Header("EnemyState")]
    [SerializeField] private bool isAlive;


    public Transform playerBody;
    private PlayerController playerControllerScript;

    public enum EnemyState
    {

        Patrol,
        Attack,
        Chase



    }

    [SerializeField] private EnemyState currentEnemyState = EnemyState.Patrol;

    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        playerControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
        Patrol();

    }

    void Update()
    {

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
        currentDistance = Vector3.Distance(transform.position, playerBody.position);
        UpdateState();
        HandlePatrol();
        HandleChase();
        HandleAttack();

        //ebug.Log(currentDistance);


    }

    public void TakeDamage(float damage)
    {

        enemyHealth -= damage;
        enemyHealth = Mathf.Clamp(enemyHealth, 0f, 100f);


    }

    public void UpdateState()
    {
        if (currentEnemyState == EnemyState.Attack)
        {

            if (currentDistance > attackRange + attackExitBuffer)
            {

                currentEnemyState = EnemyState.Chase;
                Debug.Log("Chasing");
            }

        }
        else
        {

            if (currentDistance <= attackRange)
            {
                currentEnemyState = EnemyState.Attack;
                enemyAgent.isStopped = true;
                Debug.Log("Atack");
                

            }
            else if (currentDistance <= detectionRange)
            {

                currentEnemyState = EnemyState.Chase;
                enemyAgent.isStopped = false;
                Debug.Log("Chase");

            }
            else
            {

                currentEnemyState = EnemyState.Patrol;
                enemyAgent.isStopped = false;
                Debug.Log("Patrol");

            }



        }



    }

    public Vector3 GetRandomPoint()
    {


         Vector3 randomDirection = Random.insideUnitSphere * patrolingRadius;
         Vector3 randomPoint = randomDirection + transform.position;

        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, patrolingRadius, NavMesh.AllAreas))
        {

            return hit.position;
            
        }
        return transform.position;



    }

    private void Patrol()
    {

        Vector3 patrolPoint = GetRandomPoint();
        enemyAgent.SetDestination(patrolPoint);
        
    }

    private void HandlePatrol()
    { 
        if(currentEnemyState == EnemyState.Patrol)
        {

            if (!enemyAgent.pathPending && enemyAgent.remainingDistance <= enemyAgent.stoppingDistance)
            {

                Patrol();
            
            }
        
        
        
        }
        
        
    
    
    }

    private void HandleChase()
    {


        if (currentEnemyState == EnemyState.Chase)
        {

            enemyAgent.SetDestination(playerBody.position);
        
        }




    }

    private void HandleAttack()
    {

        if (currentEnemyState == EnemyState.Attack && Time.time >= nextAttackTime)
        {
            Debug.Log("Dealing Damage");
            playerControllerScript.GetDamage(damamge);
            nextAttackTime = Time.time + attackCoolDown;
        
        
        }
         
    
    
    
    }



}
