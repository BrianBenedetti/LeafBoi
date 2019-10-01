using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour{

    private void Update()
    {
        if (Input.anyKeyDown && !(Input.GetKeyDown(KeyCode.Escape)))
        {
            NewGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }

    public void Continue(){

    }

    public void NewGame(){
       SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void Quit(){
       Application.Quit();
    }
}
