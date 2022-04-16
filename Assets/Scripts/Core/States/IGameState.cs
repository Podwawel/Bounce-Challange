using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameState
{
    void OnStateEnter();
    void Tick();
    void FixedTick();
    void OnStateExit();
}
