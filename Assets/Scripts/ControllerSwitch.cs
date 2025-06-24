using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ControllerSwitch : MonoBehaviour
{
    [SerializeField] private GameObject _firstPersonPlayer;
    [SerializeField] private GameObject _thirdPersonPlayer;
    [SerializeField] private GameObject _thirdPersonCamera;

    bool _thirdPerson = false;

    public UnityEvent OnFirstPerson;
    public UnityEvent OnThirdPerson;

    private void Start()
    {
        PlayerInputSingleton.Instance.Actions["Switch"].started += SwitchControllers;
    }

    private void SwitchControllers(InputAction.CallbackContext context)
    {
       if(_thirdPerson)
       {
            _firstPersonPlayer.transform.position = _thirdPersonPlayer.transform.position;
            _firstPersonPlayer.transform.rotation = _thirdPersonPlayer.transform.rotation;
            _thirdPersonPlayer.SetActive(false);
            OnFirstPerson.Invoke();
            _firstPersonPlayer.SetActive(true);
            _thirdPerson = false;
            _thirdPersonCamera.SetActive(false);
       }
       else
       {
            _thirdPersonPlayer.transform.position = _firstPersonPlayer.transform.position;
            _thirdPersonPlayer.transform.rotation = _firstPersonPlayer.transform.rotation;
            _thirdPersonPlayer.SetActive(true);
            OnThirdPerson.Invoke();
            _firstPersonPlayer.SetActive(false);
            _thirdPerson = true;
            _thirdPersonCamera.SetActive(true); 
       }
    }

}
