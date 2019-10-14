using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour{
    
    public Dialogue dialogue;
    public GameObject player;
    public bool inDialogue;

    private Vector3 _targetDir;
    private Quaternion _startRotation;
    private Rigidbody _rb;

    [SerializeField] protected Collider col;
    [SerializeField] protected Animator anim;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _startRotation = transform.rotation;
    }

    private void Update()
    {
        if (name.Contains("moving"))
        anim.SetBool("inDialogue", inDialogue);

        if (inDialogue)
        {
            LookAtPlayer();
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, _startRotation, 0.05f);
        }

        col.enabled = !inDialogue;
    }

    public void ResetRotation() {
        inDialogue = false;
    }

    public void TriggerDialogue(){
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        inDialogue = true;
    }

    public void LookAtPlayer() {
        if (name != "Elder")
        {
            Vector3 lookDir = new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z);
            Quaternion lookRotation = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2f);
        }
    }
}
