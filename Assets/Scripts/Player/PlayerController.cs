using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerActions _playerActions;
    public PlayerActions PlayerActions { get { return _playerActions; } }
    [SerializeField]
    private PlayerTrampolineController _playerTrampolineController;
    [SerializeField]
    private Camera _handCamera;

    public Camera HandCamera { get { return _handCamera;} }

    public void Initialize()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _playerActions.Initialize();
        _playerTrampolineController.Initialize();
    }

    public void UpdatePlayer()
    {
        _playerTrampolineController.BounceableUpdate();
        _playerActions.UpdatePlayerActions();

        if (_playerActions.IsSquating) _playerTrampolineController.BounceMultiplier = _playerTrampolineController.BounceMultiplierOnSquat;
        else _playerTrampolineController.BounceMultiplier = _playerTrampolineController.BounceMultiplierAfterFirstBounce;
    }

    public void FixedUpdatePlayer()
    {
        _playerActions.FixedUpdatePlayerActions();
    }

    public void Deinitialize()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
