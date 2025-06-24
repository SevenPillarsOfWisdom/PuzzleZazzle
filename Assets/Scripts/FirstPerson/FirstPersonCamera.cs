using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _player;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _elevationSpeed;
    [SerializeField] private Vector2 _elevationDelta;
 
    private float _currentElevation = 0;
    private Vector2 _mouseLook;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        PlayerInputSingleton.Instance.Actions["Look"].performed += OnLookPerformed;
    }

    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        _mouseLook = context.ReadValue<Vector2>();

    }

    private void Update()
    {
        _player.Rotate(_player.up, _mouseLook.x * _rotationSpeed * Time.deltaTime);

        float deltaElevation = _mouseLook.y * _elevationSpeed * Time.deltaTime;

        deltaElevation = ((_currentElevation > _elevationDelta.x && deltaElevation > 0) || (_currentElevation < _elevationDelta.y && deltaElevation < 0) ? 0 : deltaElevation);

        _currentElevation += deltaElevation;

        _camera.Rotate(Vector3.right, deltaElevation, Space.Self);
    }
}
