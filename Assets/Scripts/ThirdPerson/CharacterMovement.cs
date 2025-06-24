using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class CharacterMovement : MonoBehaviour
{
    private const float MIN_MOVE_SPEED = 0.1f;

    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private float _rotationSpeed;



    private Vector3 _currentSpeed;
    private Vector3 _wantedSpeed;

    private float _speedMagnitude;
      
    private float _vertical;
    private float _horizontal;

    private InteractionPoint _activePoint;



    private void Start()
    {
        _speedMagnitude = _walkSpeed;
        PlayerInputSingleton.Instance.Actions["Move"].performed += OnMoveInput;
        PlayerInputSingleton.Instance.Actions["Sprint"].started += OnSprintStart;
        PlayerInputSingleton.Instance.Actions["Sprint"].canceled += OnSprintEnd;
        PlayerInputSingleton.Instance.Actions["Interact"].started += OnInteractStarted;
    }

    private void OnDestroy()
    {
        PlayerInputSingleton.Instance.Actions["Move"].performed -= OnMoveInput;
        PlayerInputSingleton.Instance.Actions["Sprint"].started -= OnSprintStart;
        PlayerInputSingleton.Instance.Actions["Sprint"].canceled -= OnSprintEnd;
        PlayerInputSingleton.Instance.Actions["Interact"].started -= OnInteractStarted;
    }

    private void OnInteractStarted(InputAction.CallbackContext context)
    {
        if (_activePoint) _activePoint.Interact();
    }


    public void SetInteractionPoint(InteractionPoint point)
    {
        _activePoint = point;

    }


    private void Update()
    {
        _wantedSpeed.z = _vertical * _speedMagnitude;
        _wantedSpeed.x = _horizontal * _speedMagnitude;

        if(Mathf.Abs(_horizontal) > MIN_MOVE_SPEED || Mathf.Abs(_vertical) > MIN_MOVE_SPEED)
        {
            OrientCharacterToCamera();
        }

        _currentSpeed = Vector3.MoveTowards(_currentSpeed, _wantedSpeed, _acceleration * Time.deltaTime);

        _characterController.SimpleMove(_cameraPivot.TransformDirection(_currentSpeed));

        UpdateAnimator();
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();

        Debug.Log($"Input Vector = {inputVector}");

        _horizontal = inputVector.x;
        _vertical = inputVector.y;
    }

    private void OnSprintStart(InputAction.CallbackContext context)
    {
        _speedMagnitude = _runSpeed;
    }

    private void OnSprintEnd(InputAction.CallbackContext context)
    {
        _speedMagnitude = _walkSpeed;
    }

    private void OrientCharacterToCamera()
    {
        Vector3 eulerRotation = Vector3.MoveTowards(transform.rotation.eulerAngles, _cameraPivot.rotation.eulerAngles, _rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(eulerRotation);
    }

    private void UpdateAnimator()
    {
        _animator.SetFloat("Speed Z", _currentSpeed.z);
        _animator.SetFloat("Speed X", _currentSpeed.x);
    }
}
