using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerInputResponser _playerInputResponser;

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started)
             _playerInputResponser.PerformFire();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
       _playerInputResponser.GetMoveVector(context.ReadValue<Vector2>());
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _playerInputResponser.GetLookVector(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
            _playerInputResponser.PerformJump();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.started)
             _playerInputResponser.PerformInteraction();
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.started)
            _playerInputResponser.PerformReload();
    }

    public void OnSquat(InputAction.CallbackContext context)
    {
        if (context.performed)
            _playerInputResponser.PerformSquat();
    }

    public void OnSquatRelease(InputAction.CallbackContext context)
    {
        if (context.performed)
            _playerInputResponser.PerformUnsquat();
    }
}
