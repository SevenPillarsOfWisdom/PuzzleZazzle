using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _elevation;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _verticalSpeed;
    [SerializeField] private bool _invertMouse;


    private float _veritcalInput;
    private float _horizontalInput;

    private Vector3 GetWantedPosition()
    {
        return _target.position + _offset;
    }

    private void OnValidate()
    {
        transform.position = GetWantedPosition();
    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxis("Mouse X");
        _veritcalInput = Input.GetAxis("Mouse Y");



        transform.Rotate(Vector3.up, _horizontalInput * _horizontalSpeed * Time.deltaTime);
        _elevation.Rotate(Vector3.right, _veritcalInput * _verticalSpeed * (_invertMouse ? 1 : -1) * Time.deltaTime);
    }

    private void LateUpdate()
    {
        transform.position = GetWantedPosition();
    }
}
