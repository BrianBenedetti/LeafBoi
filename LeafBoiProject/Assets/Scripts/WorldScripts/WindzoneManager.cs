using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindzoneManager : MonoBehaviour
{
    [SerializeField]
    protected float _glideVelocity;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<PlayerController>().gliding)
            {
                Rigidbody playerRg = other.gameObject.GetComponent<Rigidbody>();
                playerRg.velocity = new Vector3(playerRg.velocity.x, _glideVelocity, playerRg.velocity.z);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<PlayerController>().gliding)
            {
                Rigidbody playerRg = other.gameObject.GetComponent<Rigidbody>();
                playerRg.velocity = new Vector3(playerRg.velocity.x, _glideVelocity, playerRg.velocity.z);
            }
        }
    }
}
