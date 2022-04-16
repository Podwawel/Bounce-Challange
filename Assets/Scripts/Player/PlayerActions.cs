using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public System.Action OnSquatEvent;
    public System.Action OnUnsquatEvent;
    public System.Action OnInteractEvent;

    private System.Action GrabbingUpdate;

    [SerializeField]
    private Transform _grabbingPoint;
    [SerializeField]
    private PlayerPhysics _playerPhysics;
    [SerializeField]
    private float _movementSpeed;
    [SerializeField]
    private float _inAirMovementSpeed;
    [SerializeField]
    private float _jumpHeight;

    private bool _isJumping;
    public bool IsJumping { get { return _isJumping; } }

    private bool _isSquating;

    public bool IsSquating { get { return _isSquating; } set { _isSquating = value; } }

    public bool IsHoldingGrabbable = false;

    #region CAMERA

    [SerializeField]
    private float _mouseSensitivity;
    [SerializeField]
    private Camera _camera;

    private Vector2 _lookVector;
    private float _clampedRotationOnXAxis = 0f;
    #endregion

    private GameObject _currentlyHoldingObject;
    private Vector3 _holdedObjectNorminalScale;

    private Vector2 _moveVector;

    private IPlayerInputFacade _playerInputFacade;

    public Vector2 LookVector { get { return _lookVector; } }
    public void Initialize()
    {
        _playerInputFacade = GameManager.Instance.PlayerInputFacade;

        _playerInputFacade.OnFirePerformedEvent += Fire;
        _playerInputFacade.OnJumpPerformedEvent += Jump;
        _playerInputFacade.OnInteractionPerformedEvent += Interact;
        _playerInputFacade.OnSquatPerformedEvent += Squat;
        _playerInputFacade.OnUnsquatPerformedEvent += Unsquat;
        _playerInputFacade.OnReloadPerformedEvent += Reload;
    }

    public void UpdatePlayerActions()
    {
        Look();
        if (GrabbingUpdate != null) GrabbingUpdate();
    }

    public void FixedUpdatePlayerActions()
    {
        CheckGround();
        Move();
        //OnSlope();
    }

    private void Move()
    {
        float movementSpeed;
        Vector3 movement;
        if (!_playerPhysics.CheckGround())
        {
            movementSpeed = _inAirMovementSpeed * 0.05f;
            movement = (_moveVector.y * transform.forward) + (_moveVector.x * transform.right);
        }
        else
        {
            movementSpeed = _movementSpeed * 0.025f;
            _moveVector = _playerInputFacade.MoveVector;
            movement = (_moveVector.y * transform.forward) + (_moveVector.x * transform.right);
        }

        _playerPhysics.MoveRigidbody(movement * movementSpeed);
    }

    private void Look()
    {
        _lookVector = _playerInputFacade.LookVector;

        float mouseX = _lookVector.x * _mouseSensitivity * Time.deltaTime;
        float mouseY = _lookVector.y * _mouseSensitivity * Time.deltaTime;

        _clampedRotationOnXAxis -= mouseY;
        _clampedRotationOnXAxis = Mathf.Clamp(_clampedRotationOnXAxis, -90f, 60f);

        _camera.transform.localRotation = Quaternion.Euler(_clampedRotationOnXAxis, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    public void CheckGround()
    {
        _playerPhysics.CheckGround();
        if (_playerPhysics.CheckGround()) _isJumping = false;
        else _isJumping = true;
    }

    private void Jump()
    {
        if (!_playerPhysics.CheckGround()) return;

        _playerPhysics.SetVelocity(new Vector3(_playerPhysics.GetVelocity().x, _jumpHeight, _playerPhysics.GetVelocity().z));
    }

    private void OnSlope()
    {
        if (_isJumping) return;

        _playerPhysics.OnSlope();
    }

    private void Fire()
    {
        GameManager.Instance.WeaponController.Action();
    }

    private void Interact()
    {
        OnInteractEvent?.Invoke();
    }

    private void Squat()
    {
        _isSquating = true;
        OnSquatEvent?.Invoke();
    }

    private void Unsquat()
    {
        _isSquating = false;
        OnUnsquatEvent?.Invoke();
    }

    private void Reload()
    {
        GameManager.Instance.WeaponController.ReloadWeapon();
    }

    public void SnapObjectToGrabbingPoint(GameObject gameObject, float scale)
    {
        GrabbingUpdate += KeepGrabbableInPoint;
        _holdedObjectNorminalScale = gameObject.transform.localScale;
        gameObject.transform.localScale *= scale;
        _currentlyHoldingObject = gameObject;
    }

    public void KeepGrabbableInPoint()
    {
        if (_currentlyHoldingObject == null) return;

        _currentlyHoldingObject.transform.position = _grabbingPoint.position;
        _currentlyHoldingObject.transform.rotation = _grabbingPoint.transform.rotation;
    }

    public void SnapObjectFromGrabbingPoint(Vector3 position)
    {
        if (_currentlyHoldingObject == null) return;

        GrabbingUpdate = null;
        _currentlyHoldingObject.transform.position = position;
        _currentlyHoldingObject.transform.localScale = _holdedObjectNorminalScale;
        _currentlyHoldingObject = null;
    }

    public void Deinitialize()
    {

    }
}
