using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInputResponser : MonoBehaviour
{
    public void GetMoveVector(Vector2 inputVector)
    {
        GameManager.Instance.PlayerInputFacade.SetMoveVector(inputVector);
    }

    public void GetLookVector(Vector2 lookVector)
    {
        GameManager.Instance.PlayerInputFacade.SetLookVector(lookVector);
    }

    public void PerformInteraction()
    {
        GameManager.Instance.PlayerInputFacade.FireOnInteractionEvent();
    }

    public void PerformJump()
    {
        GameManager.Instance.PlayerInputFacade.FireOnJumpEvent();
    }

    public void PerformReload()
    {
        GameManager.Instance.PlayerInputFacade.FireOnReloadEvent();
    }
    public void PerformFire()
    {
        GameManager.Instance.PlayerInputFacade.FireOnFireEvent();
    }

    public void PerformSquat()
    {
        GameManager.Instance.PlayerInputFacade.FireOnSquatEvent();
        GameManager.Instance.WeaponController.ChangeWeapon();
    }

    public void PerformUnsquat()
    {
        GameManager.Instance.PlayerInputFacade.FireOnUnSquatEvent();
    }
}
