using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour{
    
    public Dialogue dialogue;
    public GameObject player;

    private bool _inDialogue;

    private void Start()
    {
    }

    private void Update()
    {
        if (_inDialogue)
        {
            LookAtPlayer();
        }
    }

    public void TriggerDialogue(){
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public void LookAtPlayer() {
        Vector3 targetDir = new Vector3(player.transform.position.x - transform.position.x, transform.position.y, player.transform.position.z - transform.position.z);

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 1f, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDir);
    }
}
