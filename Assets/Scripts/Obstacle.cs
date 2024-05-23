using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Obstacle : MonoBehaviour
{

    private CinemachineImpulseSource impulseSource;

    private void Start()
    {
        // Ensure the GameObject has a CinemachineImpulseSource component
        impulseSource = GetComponent<CinemachineImpulseSource>();
        if (impulseSource == null)
        {
            Debug.LogError("CinemachineImpulseSource component not found on this object.", gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision){

        if (collision.gameObject.tag == "Player"){
            HitPlayer(collision.gameObject);
        }
    }

    public virtual void HitPlayer(GameObject player){

        if (impulseSource != null)
        {
            impulseSource.GenerateImpulse();
        }
        
        Debug.Log("I hit the player");
        PlayerEvents.PlayerHit();
    }
}
