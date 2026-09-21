using UnityEngine;

public class EnterTower : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            other.transform.position = new Vector3(transform.position.x, 14f, transform.position.z);
            
        }
    }
}
