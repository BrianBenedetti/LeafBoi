using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool secondMenuOpen = false;

    public GameObject pauseBackground;
    Animator animator1;
    Animator animator2;

    void Awake()
    {
        animator1 = GameObject.Find("PauseMainBoard").GetComponent<Animator>();
        animator2 = GameObject.Find("PauseSecondaryBoard").GetComponent<Animator>();
    }

   
    // Update is called once per frame
    void Update()
    {
        animator1.SetBool("IsOpen",isPaused);
        animator2.SetBool("SecondaryOpen",secondMenuOpen);

        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused){
                Resume();
            }else{
                Pause();
            }
        }
    }
       public void GoToMainMenu(){
       SceneManager.LoadScene(0);
   }
   public void Pause(){
       isPaused = true;
       Time.timeScale = 0f;
       pauseBackground.SetActive(true);
   }
   public void Resume(){
       secondMenuOpen = false;
       isPaused = false;
       Time.timeScale = 1f;
       pauseBackground.SetActive(false);
   }
   public void OpenSecondMenu(){
       secondMenuOpen = true;
   }
}
