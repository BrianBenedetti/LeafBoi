using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    [SerializeField] protected PlayerController player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SaveSystem.SavePlayer(player);
            print("Saved Player Data");
        }
    }
}
