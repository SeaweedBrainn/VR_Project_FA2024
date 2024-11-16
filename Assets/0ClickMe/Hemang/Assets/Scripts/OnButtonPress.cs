using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem;

public class OnButtonPress : MonoBehaviour
{
    [Tooltip("Actions to check")] 
    public InputAction action = null;
    public UnityEvent onPress = new UnityEvent();
    public UnityEvent onRelease = new UnityEvent();

    private void Awake()
    {
        action.started += Pressed;
        action.canceled += Released;
    }

    private void OnDestroy()
    {
        action.started -= Pressed;
        action.canceled -= Released;
    }

    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }

    private void Pressed(InputAction.CallbackContext context)
    {
        onPress.Invoke();
    }

    private void Released(InputAction.CallbackContext context)
    {
        onRelease.Invoke();
    }
}
