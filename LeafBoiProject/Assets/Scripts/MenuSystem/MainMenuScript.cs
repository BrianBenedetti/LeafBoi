using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour{

    public GameObject mainMenu;
    public GameObject pressText;
    public Animator optionsAnim;

    private void Update()
    {
        if (Input.anyKeyDown && !(Input.GetKeyDown(KeyCode.Escape)))
        {
            mainMenu.SetActive(true);
            pressText.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }

    public void Options()
    {
        optionsAnim.SetBool("SecondaryOpen", true);
    }

    public void Continue(){
        //Don't Delete Script at this point Run a Coroutine that loads the old state of the system then delete this script
    }

    public void NewGame(){
       SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void Quit(){
       Application.Quit();
    }

    public void Credits(){
        SceneManager.LoadScene(2);
    }
}
