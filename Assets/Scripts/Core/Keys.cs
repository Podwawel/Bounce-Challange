using UnityEngine;
using System;
public enum GameStateName
{
    MAIN_MENU,
    GAMEPLAY,
}

[Serializable]
public enum BounceType
{
    HORIZONTAL_BOUNCE,
    VERTICAL_BOUNCE,
    ANGLE_BOUNCE,
}
