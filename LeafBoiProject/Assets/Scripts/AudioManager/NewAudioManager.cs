using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAudioManager : MonoBehaviour
{
 private static NewAudioManager instance;

 private AudioSource musicSource1;
 private AudioSource musicSource2;

 private bool firstMusicSourceIsPlaying;

 void Awake()
 {
     if(instance == null){
         instance = this;
     }else if (instance != this){
         Destroy(this.gameObject);
         return;
     }
     DontDestroyOnLoad(this.gameObject);

     musicSource1 = this.gameObject.AddComponent<AudioSource>();
     musicSource2 = this.gameObject.AddComponent<AudioSource>();

     musicSource1.loop = true;
     musicSource2.loop = true;
 }

 public void PlayMusic(AudioClip musicClip){
     AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource1 : musicSource2;

     activeSource.clip = musicClip;
     activeSource.volume = 1f;
     activeSource.Play();
 }

 public void PlayMusicWithFade(AudioClip newClip, float transitionTime = 1f){
     AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource1 : musicSource2;

     StartCoroutine(UpdateMusicWithFade(activeSource,newClip,transitionTime));
 }

 public void PlayMusicWithCrossFade(AudioClip musicClip, float transitionTime = 1f){

     AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource1 : musicSource2;
     AudioSource newSource = (firstMusicSourceIsPlaying) ? musicSource2 : musicSource1;

     firstMusicSourceIsPlaying = !firstMusicSourceIsPlaying;

     newSource.clip = musicClip;
     newSource.Play();
     StartCoroutine(UpdateMusicWithCrossFade(activeSource,newSource,transitionTime));
 }

 public void SetMusicVolume(float volume){
     musicSource1.volume = volume;
     musicSource2.volume = volume;
 }
 private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime){
     if(!activeSource.isPlaying){
         activeSource.Play();

       float t = 0.0f;

       for (t = 0; t < transitionTime; t += Time.deltaTime){
           activeSource.volume = (1 -(t/transitionTime));
           yield return null;
       }

       activeSource.Stop();
       activeSource.clip = newClip;
       activeSource.Play();

       for (t = 0; t < transitionTime; t += Time.deltaTime){
           activeSource.volume = (t/transitionTime);
           yield return null;
       }
     }
 }

 private IEnumerator UpdateMusicWithCrossFade(AudioSource original, AudioSource newSource, float transitionTime){

     float t = 0f;

     for (t = 0; t <= transitionTime; t += Time.deltaTime){
         original.volume = (1- (t/transitionTime));
         newSource.volume = (t/transitionTime);
         yield return null;
     }

     original.Stop();
 }
}
