using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float currentDistance;

    [Header("Health")]
    [SerializeField] private float enemyHealth = 100;

    [Header("Movement")]

    [Header("Attack")]
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float damamge;
    [SerializeField] private float attackExitBuffer = 0.25f;

    [Header("Detection")]
    [SerializeField] private float detectionRange = 6f;
    private bool isDetected = false;






    [Header("EnemyState")]
    [SerializeField] private bool isAlive;


    public Transform playerBody;

    public enum EnemyState
    {

        Patrol,
        Attack,
        Chase



    }

    [SerializeField] private EnemyState currentEnemyState = EnemyState.Patrol;

    void Start()
    {

    }

    void Update()
    {

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }

        UpdateState();
        currentDistance = Vector3.Distance(transform.position, playerBody.position);

    }

    public void TakeDamage(float damage)
    {

        enemyHealth -= damage;
        enemyHealth = math.clamp(enemyHealth, 0f, 100f);


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
                Debug.Log("Attack");

            }
            else if (currentDistance <= detectionRange)
            {

                currentEnemyState = EnemyState.Chase;
                Debug.Log("chase");
            }
            else
            {

                currentEnemyState = EnemyState.Patrol;
                Debug.Log("Patrol");

            }



        }



    }

}
