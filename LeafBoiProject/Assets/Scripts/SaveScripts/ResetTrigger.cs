using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTrigger : MonoBehaviour
{
    [SerializeField] protected PlayerController player;
    [SerializeField] protected float delay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(TriggerReset());
        }
    }

    private IEnumerator TriggerReset()
    {
        yield return new WaitForSeconds(delay);
        player.LoadGame();
    }
}
