using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("EnemyValues")]
    [SerializeField] private float enemyHealth = 100;
    

    [Header("EnemyState")]
    [SerializeField] private bool isAlive;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
        
    }

    public void TakeDamage(float damage)
    {

        enemyHealth -= damage;
        enemyHealth = math.clamp(enemyHealth, 0f, 100f);
    
    
    }

}
