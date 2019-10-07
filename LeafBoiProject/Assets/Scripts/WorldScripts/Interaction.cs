using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public GameObject InteractButton;

    public bool NPC;
    public bool Button;
    public bool Instrument;

    [SerializeField]
    protected bool interactable;

    private bool _interactDisplay;

    private void Start()
    {
        _interactDisplay = false;
    }

    private void Update()
    {
        if (GetComponent<NPCDialogueTrigger>() != null)
        {
            if (!GetComponent<NPCDialogueTrigger>().inDialogue)
            {
                InteractButton.SetActive(_interactDisplay);
            }
            else {
                InteractButton.SetActive(false);
            }
        }
        else {
            InteractButton.SetActive(_interactDisplay);
        }
        
        InteractButton.GetComponent<Transform>().LookAt(Camera.main.transform);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactable = true;
            PlayerController.instance.interactable = this.gameObject;
            _interactDisplay = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactable = true;
            PlayerController.instance.interactable = this.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactable = false;
            PlayerController.instance.interactable = null;
            _interactDisplay = false;
        }
        
    }
}
