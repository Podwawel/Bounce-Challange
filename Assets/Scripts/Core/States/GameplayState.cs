using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayState : IGameState
{
    private CanvasController _canvasController;
    private PlayerController _playerController;
    private BounceableObjectsController _bounceableObjectsController;
    private InteractableObjectsController _interactableObjectsController;
    private ObjectFinder _objectFinder;
    private RaycastController _raycastController;
    private WeaponController _weaponController;
    public void OnStateEnter()
    {
        Debug.Log("GAMEPLAY STATE");
        _canvasController = GameManager.Instance.CanvasController;
        _playerController = GameManager.Instance.PlayerController;
        _objectFinder = GameManager.Instance.ObjectFinder;
        _raycastController = GameManager.Instance.RaycastController;
        _weaponController = GameManager.Instance.WeaponController;

        _playerController.Initialize();

        _bounceableObjectsController = _objectFinder.FindBouncableObjectsController();
        _bounceableObjectsController.InitializeBouncableObjects();
        _interactableObjectsController = _objectFinder.FindInteractableObjectsController();
        _interactableObjectsController.InitializeInteractableObjects();

        _raycastController.Initialize();
        _canvasController.SetCanvas(true,_canvasController.HUDCanvas);
        _weaponController.Initialize();
        _weaponController.Unlock(WeaponType.MAKARONI);
    }
    public void Tick()
    {
        _playerController.UpdatePlayer();
        _raycastController.UpdateRaycastController();
    }

    public void FixedTick()
    {
        _playerController.FixedUpdatePlayer();
    }

    public void OnStateExit()
    {
        _canvasController.SetCanvas(false, _canvasController.HUDCanvas);
        _interactableObjectsController.DeinitializeInteractablesObjects();
        _playerController.Deinitialize();
    }
}
