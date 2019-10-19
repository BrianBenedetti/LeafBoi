using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour{

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public bool convActive;

    AudioSource source;
    AudioClip ElderVoice;
    AudioClip VillagerVoice;

    public Animator animator;

    private string _sentence;
    private Queue<string> _sentences;
    private bool _typing;
    private InputMaster _controls;
    private Dialogue currDialogue;
    

    [SerializeField] protected GameObject player;
    [SerializeField] protected GameObject[] villagers;

    // Start is called before the first frame update
    void Start(){
        _sentences = new Queue<string>();
    }

    private void Update()
    {
        if (animator.GetBool("IsOpen"))
        {
            player.GetComponent<PlayerController>().inDialogue = true;
        }
    }

    public void StartDialogue(Dialogue dialogue){
        currDialogue = dialogue;

        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        _sentences.Clear();

        foreach(string sentence in dialogue.sentences){

            _sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence(){

        if (!_typing)
        {
            

            if (currDialogue.name.Contains("Elder"))
            {
                source.PlayOneShot(ElderVoice);
            }
            else
            if(currDialogue.name.Contains("Villager"))
            {
                source.PlayOneShot(VillagerVoice);
            }

            if (_sentences.Count == 0)
            {

                EndDialogue();
                return;
            }

            _sentence = _sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(_sentence));
        }
        else
        {
            StopAllCoroutines();
            dialogueText.text = _sentence;
            _typing = false;
        }
    }

    IEnumerator TypeSentence(string sentence){
        _typing = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()){
            
            dialogueText.text += letter;
            yield return null;
        }
        _typing = false;
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
