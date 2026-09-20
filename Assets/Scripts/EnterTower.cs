using UnityEngine;

public class EnterTower : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            other.transform.position = new Vector3(218f, 14f, -13f);
            
        }
    }
}
