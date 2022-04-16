using System;
using UnityEngine;
public class PlayerInputAdapter : IPlayerInputFacade
{
    public event Action OnJumpPerformedEvent;
    public event Action OnInteractionPerformedEvent;
    public event Action OnFirePerformedEvent;
    public event Action OnSquatPerformedEvent;
    public event Action OnUnsquatPerformedEvent;
    public event Action OnReloadPerformedEvent;

    public Vector2 MoveVector { get; private set; }
    public Vector2 LookVector { get; private set; }

    public void FireOnFireEvent()
    {
        OnFirePerformedEvent?.Invoke();
    }

    public void FireOnInteractionEvent()
    {
        OnInteractionPerformedEvent?.Invoke();
    }

    public void FireOnJumpEvent()
    {
        OnJumpPerformedEvent?.Invoke();
    }

    public void SetMoveVector(Vector2 moveVector)
    {
        MoveVector = moveVector;
    }

    public void FireOnReloadEvent()
    {
        OnReloadPerformedEvent?.Invoke();
    }
    public void SetLookVector(Vector2 lookVector)
    {
        LookVector = lookVector;
    }

    public void FireOnSquatEvent()
    {
        OnSquatPerformedEvent?.Invoke();
    }

    public void FireOnUnSquatEvent()
    {
        OnUnsquatPerformedEvent?.Invoke();
    }
}
