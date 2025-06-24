using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _elevation;
    [SerializeField] private Transform _cameraPoint;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _verticalSpeed;
    [SerializeField] private bool _invertMouse;
    [SerializeField] private LayerMask _collisionMask;
    [SerializeField] private float _desiredArmLenght = 2;


    private float _veritcalInput;
    private float _horizontalInput;

    private float _currentArmLenght;

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(_cameraPoint.position, _target.position + _offset);
    }

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


        IsCameraOccluded();
    }

    private void LateUpdate()
    {
        transform.position = GetWantedPosition();
    }

    private void SetArmLength(float lenght)
    {
        _currentArmLenght = lenght;
        _cameraPoint.localPosition = new Vector3(0, 0, -lenght);
    }

    private void IsCameraOccluded()
    {
        RaycastHit hit;
        Ray ray = new Ray(_target.position + _offset, _cameraPoint.position - (_target.position + _offset));
        if (Physics.SphereCast(ray, 0.25f, out hit, _desiredArmLenght, _collisionMask))
        {
            SetArmLength(hit.distance);
        }
        else
        {
            SetArmLength(_desiredArmLenght);
        }
    }
}
