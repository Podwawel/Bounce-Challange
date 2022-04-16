using UnityEngine;
public interface IPlayerInputFacade
{
    event System.Action OnJumpPerformedEvent;
    event System.Action OnInteractionPerformedEvent;
    event System.Action OnFirePerformedEvent;
    event System.Action OnSquatPerformedEvent;
    event System.Action OnUnsquatPerformedEvent;
    event System.Action OnReloadPerformedEvent;

    Vector2 MoveVector { get; }
    Vector2 LookVector { get; }

    void FireOnJumpEvent();
    void FireOnInteractionEvent();
    void FireOnFireEvent();
    void SetMoveVector(Vector2 moveVector);
    void SetLookVector(Vector2 lookVector);
    void FireOnSquatEvent();
    void FireOnUnSquatEvent();
    void FireOnReloadEvent();
}
