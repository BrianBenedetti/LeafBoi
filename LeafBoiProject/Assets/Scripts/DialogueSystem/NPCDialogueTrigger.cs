using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour{
    
    public Dialogue dialogue;
    public GameObject player;

    private bool _inDialogue;
    private Vector3 _targetDir;
    private Quaternion startRotation;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        startRotation = transform.rotation;
    }

    private void Update()
    {
        if (_inDialogue)
        {
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
        if (name != "Elder")
        {
            Vector3 lookDir = new Vector3(player.transform.position.x - transform.position.x, transform.position.y, player.transform.position.z - transform.position.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), Time.deltaTime * 2f);
        }
    }
}
