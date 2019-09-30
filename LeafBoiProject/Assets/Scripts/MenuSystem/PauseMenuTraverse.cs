using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuTraverse : MonoBehaviour
{
    [SerializeField] protected Button[] buttons;
    [SerializeField] protected float transitionLimit;
    [SerializeField] protected PauseMenu pause;

    private InputMaster _controls;

    private bool _canChange = true;
    private Vector2 _moveAxis;
    private int _index = 0;

    private float _transitionTime;

    private void Awake()
    {
        _controls = new InputMaster();
        _controls.Player.Movement.performed += context => _moveAxis = context.ReadValue<Vector2>();
        _controls.Player.Movement.performed += context => HandleTraversal();
        _controls.Player.Movement.canceled += context => _moveAxis = Vector2.zero;

        _controls.Player.Jump.performed += context => HandleButton();
    }

    private void HandleButton()
    {
        if (pause.isPaused)
        {
            buttons[_index].onClick.Invoke();
        }
    }

    public void StartPause()
    {
        buttons[0].Select();
    }

    private void HandleTraversal()
    {
        if (_moveAxis.y < 0 && _canChange)
        {
            _index++;
            if (_index > buttons.Length - 1)
            {
                _index = 0;
            }
            buttons[_index].Select();
            _canChange = false;
        }

        if(_moveAxis.y > 0 && _canChange)
        {
            _index--;
            if (_index < 0)
            {
                _index = buttons.Length - 1;
            }

            buttons[_index].Select();
            _canChange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_canChange)
        {
            _transitionTime += Time.fixedUnscaledDeltaTime;

            if (_transitionTime > transitionLimit)
            {
                _canChange = true;
                _transitionTime = 0;
            }
        }

    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }
}
