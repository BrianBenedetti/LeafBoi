﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] protected GameObject controls;
    [SerializeField] protected GameObject options;

    public bool isPaused = false;
    public bool secondMenuOpen = false;

    public GameObject pauseBackground;
    private Animator _animator1;
    private Animator _animator2;
    private InputMaster _controls;

    void Awake()
    {
        _animator1 = GameObject.Find("PauseMainBoard").GetComponent<Animator>();
        _animator2 = GameObject.Find("PauseSecondaryBoard").GetComponent<Animator>();

        _controls = new InputMaster();

        _controls.Player.Pause.performed += context => HandlePause();
    }

    // Update is called once per frame
    private void Update()
    {
        _animator1.SetBool("IsOpen",isPaused);
        _animator2.SetBool("SecondaryOpen",secondMenuOpen);

        if (isPaused)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
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
        controls.SetActive(false);
        options.SetActive(false);
    }

    public void OpenSecondMenu(){
       secondMenuOpen = true;
    }

    public void CloseSecondMenu() {
        secondMenuOpen = false;
        controls.SetActive(false);
        options.SetActive(false);
    }

    private void HandlePause()
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    //Enables controls when this object is enabled
    private void OnEnable()
    {
        _controls.Enable();
    }

    //Disables controls when this object is enabled
    private void OnDisable()
    {
        _controls.Disable();
    }
}
