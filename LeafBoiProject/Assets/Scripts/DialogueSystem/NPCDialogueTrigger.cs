using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour{
    
    public Dialogue dialogue;
    public GameObject player;

    private bool _inDialogue;
    private Vector3 _targetDir;
    private Quaternion startRotation;

    private void Start()
    {
        startRotation = transform.rotation;
    }

    private void Update()
    {
        if (_inDialogue)
        {
            print(this.gameObject.name + "is in a Dialogue");
            LookAtPlayer();
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, startRotation, 0.05f);
        }
    }

    public void ResetRotation() {
        _inDialogue = false;
    }

    public void TriggerDialogue(){
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        _inDialogue = true;
    }

    public void LookAtPlayer() {
        _targetDir = new Vector3(player.transform.position.x - transform.position.x, transform.position.y, player.transform.position.z - transform.position.z);
        Vector3 newDir = Vector3.RotateTowards(transform.forward, _targetDir, 0.05f, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }
}
