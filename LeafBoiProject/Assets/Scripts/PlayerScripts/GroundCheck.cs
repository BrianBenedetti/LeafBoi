using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public Rigidbody rb;
    public float dToGround;
    public PlayerController player;


    // Update is called once per frame
    void FixedUpdate()
    {
        bool groundRay = Physics.Raycast(transform.position, Vector3.down, dToGround);

        if (groundRay)
        {
            player.Grounded();
        }
    }
}
