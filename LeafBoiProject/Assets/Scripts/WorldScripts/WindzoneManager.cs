using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindzoneManager : MonoBehaviour
{
    [SerializeField]
    protected Vector3 _windDirection;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(_windDirection);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(_windDirection);
        }
    }
}
