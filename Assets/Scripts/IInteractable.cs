using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Initialize();
    void OnRaycastHit();
    void OnRaycastUpdate();
    void OnRaycastEnd();
    void Action();
    void Deinitialize();
}
