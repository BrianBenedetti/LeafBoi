using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour{
   
   public void Continue(){

   }
   public void NewGame(){
       SceneManager.LoadScene("Game");
   }
   public void Quit(){
       Application.Quit();
   }
}
