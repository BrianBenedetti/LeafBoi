using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    [SerializeField] protected PlayerController player;
    [SerializeField] protected GameManager gm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SaveSystem.SavePlayer(player, gm);
            print("Saved Player Data");
        }
    }
}
