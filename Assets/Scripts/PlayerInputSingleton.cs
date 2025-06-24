using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSingleton : MonoBehaviour
{
    public static PlayerInputSingleton Instance { get; private set; }

    [SerializeField] private PlayerInput _input;

    public InputActionAsset Actions => _input.actions;

    private void Awake()
    {

        
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
