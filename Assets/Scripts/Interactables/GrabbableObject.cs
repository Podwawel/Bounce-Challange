using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : InteractableObject, IGrabbable
{
    [SerializeField]
    private float _scaleMultiplier = 1;

    private Rigidbody _rigidbody;

    public override void Initialize()
    {
        base.Initialize();

        _rigidbody = GetComponent<Rigidbody>();
    }

    public override void Action()
    {
        base.Action();

        Grab();
    }

    public override void OnRaycastUpdate()
    {
        if (GameManager.Instance.WeaponController.CurrentWeaponType != WeaponType.NONE || GameManager.Instance.PlayerController.PlayerActions.IsHoldingGrabbable)
        {
            _material.color = _color;
            _interactionController.DesactivateInteractionCanvases();
            return;
        }

        base.OnRaycastUpdate();
    }

    public override void OnRaycastHit()
    {
        if (GameManager.Instance.WeaponController.CurrentWeaponType != WeaponType.NONE || GameManager.Instance.PlayerController.PlayerActions.IsHoldingGrabbable) return;

        base.OnRaycastHit();
    }

    public void Grab()
    {
        if (GameManager.Instance.WeaponController.CurrentWeaponType != WeaponType.NONE) return;

        _playerController.PlayerActions.SnapObjectToGrabbingPoint(gameObject, _scaleMultiplier);
        _playerController.PlayerActions.IsHoldingGrabbable = true;
        _rigidbody.useGravity = false;
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX;
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationY;
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;
        _playerController.PlayerActions.OnInteractEvent -= Action;
        _playerController.PlayerActions.OnInteractEvent += PutDown;
    }

    public void PutDown()
    {
        if (GameManager.Instance.RaycastController.ObstacleWithinRange) return;

        _playerController.PlayerActions.IsHoldingGrabbable = false;
        _playerController.PlayerActions.SnapObjectFromGrabbingPoint(Camera.main.transform.position + Camera.main.transform.forward * 3);
        _rigidbody.useGravity = true;
        _rigidbody.constraints = RigidbodyConstraints.None;
        _playerController.PlayerActions.OnInteractEvent -= PutDown;
    }
}
