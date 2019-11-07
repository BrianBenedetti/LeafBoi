using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAudioManager : MonoBehaviour
{
    private AudioSource source;
    public AudioSource JourneySource;
    public AudioClip myClip;
    public AudioClip JourneyClip;

    void Awake()
    {
        source = gameObject.GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            StartCoroutine(TriggerEnter());
    }
}

    void OnTriggerExit(Collider other){
        if(other.CompareTag("Player")){
             StartCoroutine(TriggerExit());
        }
    }

private IEnumerator TriggerExit(){
        float t = 0.0f;
             float transitionTime = 3f;

       for (t = 0; t < transitionTime; t += Time.deltaTime){
           source.volume = (1 -(t/transitionTime));
           yield return null;
       }

       source.Stop();
       JourneySource.clip = JourneyClip;
       JourneySource.Play();

       for (t = 0; t < transitionTime; t += Time.deltaTime){
           JourneySource.volume = (t/transitionTime);
           yield return null;
        }
    }

    private IEnumerator TriggerEnter(){
             float t = 0.0f;
             float transitionTime = 3f;

       for (t = 0; t < transitionTime; t += Time.deltaTime){
           JourneySource.volume = (1 -(t/transitionTime));
           yield return null;
       }

       JourneySource.Stop();
       source.clip = myClip;
       source.Play();

       for (t = 0; t < transitionTime; t += Time.deltaTime){
           source.volume = (t/transitionTime);
           yield return null;
        }
    }
}