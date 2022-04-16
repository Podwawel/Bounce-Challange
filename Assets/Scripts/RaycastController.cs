using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    [SerializeField]
    private LayerMask[] _ignoreRaycastMask;
    private int _layerMasksToIgnoreValue;
    private Ray _crosshairRay;
    private RaycastHit _crosshairRayHitData;
    [SerializeField]
    private float _crosshairRayDistance;

    [SerializeField]
    private float _obstacleCheckRayDistance;
    private Ray _obstacleCheckRay;
    private RaycastHit _obstacleCheckRayHitData;

    [SerializeField]
    private LayerMask _combatRayMask;
    private Ray _combatRay;
    private RaycastHit _combatRayHitData;
    
    public bool ObstacleWithinRange { get; private set; }

    private int _currentHitID;
    private IInteractable _previousInteractable;

    [SerializeField]
    private float _raycastUpdateTime;
    private float _timeSinceLastRaycastUpdate;

    public void Initialize()
    {
        _currentHitID = 0;
        _timeSinceLastRaycastUpdate = 0;
        _layerMasksToIgnoreValue = CalculateBinaryLayersToIgnore();
    }

    public void UpdateRaycastController()
    {
        _timeSinceLastRaycastUpdate += Time.deltaTime;

        if (_timeSinceLastRaycastUpdate >= _raycastUpdateTime)
        {
            CroshairRaycastUpdate();
            ObstacleCheckRaycastUpdate();
        }
    }

    private void ObstacleCheckRaycastUpdate()
    {
        _obstacleCheckRay.origin = Camera.main.transform.position;
        _obstacleCheckRay.direction = Camera.main.transform.forward;

        if (Physics.Raycast(_obstacleCheckRay, out _obstacleCheckRayHitData, _obstacleCheckRayDistance, ~_layerMasksToIgnoreValue))
        {
            ObstacleWithinRange = true;
        }
        else ObstacleWithinRange = false;
    }
    public IDamageable CombatRaycastCheck()
    {
        if (GameManager.Instance.WeaponController.CurrentWeaponStyle == WeaponStyle.RANGED) return null;

        _combatRay.origin = Camera.main.transform.position;
        _combatRay.direction = Camera.main.transform.forward;

        if (Physics.Raycast(_combatRay, out _combatRayHitData, GameManager.Instance.WeaponController.CurrentWeaponRange, _combatRayMask))
        {
            IDamageable damageable = _combatRayHitData.collider.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                return damageable;
            }
        }

        return null;
    }
    private void CroshairRaycastUpdate()
    {
        _crosshairRay.origin = Camera.main.transform.position;
        _crosshairRay.direction = Camera.main.transform.forward;
        Debug.DrawRay(_crosshairRay.origin, _crosshairRay.direction, Color.red);

        if (Physics.Raycast(_crosshairRay, out _crosshairRayHitData, _crosshairRayDistance, ~_layerMasksToIgnoreValue))
        {
            if (_currentHitID != _crosshairRayHitData.collider.GetInstanceID() && _previousInteractable != null)
            {
                _previousInteractable.OnRaycastEnd();
                _previousInteractable = null;
            }

            IInteractable interactable = _crosshairRayHitData.collider.gameObject.GetComponent<IInteractable>();

            if (_currentHitID != _crosshairRayHitData.collider.GetInstanceID() && interactable != null)
            {
                interactable.OnRaycastHit();
                _previousInteractable = interactable;
            }
            _currentHitID = _crosshairRayHitData.collider.GetInstanceID();

            if (interactable != null)
            {
                interactable.OnRaycastUpdate();
            }
        }
        else
        {
            if (_previousInteractable != null)
            {
                _currentHitID = 0;
                _previousInteractable.OnRaycastEnd();
                _previousInteractable = null;
            }
        }
        _timeSinceLastRaycastUpdate = 0;
    }

    private int CalculateBinaryLayersToIgnore()
    {     
        int binaryLayersToIgnore = 0;
        for(int i =0; i<_ignoreRaycastMask.Length;i++)
        {
            binaryLayersToIgnore += _ignoreRaycastMask[i].value;
        }

        return binaryLayersToIgnore;
    }
}
