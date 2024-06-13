using UnityEngine;

public class SpeedFlag : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // When the player enters the trigger area, destroy the flag
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}