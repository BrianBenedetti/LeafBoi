using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour{
   
   public void Continue(){

   }

   public void NewGame(){
       SceneManager.LoadScene(1);
   }

   public void Quit(){
       Application.Quit();
   }
}
