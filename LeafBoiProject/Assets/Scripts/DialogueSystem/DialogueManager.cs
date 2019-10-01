using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour{

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public bool convActive;

    public Animator animator;

    private Queue<string> sentences;

    private InputMaster _controls;
    

    [SerializeField] protected GameObject player;
    [SerializeField] protected GameObject[] villagers;

    // Start is called before the first frame update
    void Start(){
        sentences = new Queue<string>();
    }

    private void Update()
    {
        if (animator.GetBool("IsOpen"))
        {
            player.GetComponent<PlayerController>().inDialogue = true;
        }
    }

    public void StartDialogue(Dialogue dialogue){

        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences){

            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence(){

        if(sentences.Count == 0){

            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence){

        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()){
            
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue(){

        animator.SetBool("IsOpen", false);
        player.GetComponent<PlayerController>().endDialogue();
        for (int i = 0; i < villagers.Length; i++)
        {
            villagers[i].GetComponent<NPCDialogueTrigger>().ResetRotation();
        }
        player.GetComponent<PlayerController>().inDialogue = false;
    }
}
