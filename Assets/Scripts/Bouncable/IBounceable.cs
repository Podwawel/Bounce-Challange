using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBounceable
{
    float BounceMultiplier { get; set; }
    bool IsInBounceArea { get; set; }
    float BounceMultiplierAfterFirstBounce { get; set; }
    Vector3 NextBouncePower { get; set; }
    float Bounciness { get; }

    void Bounce();
    void CalculateNextBouncePower(BounceType bounceType, Transform trampolineTransform);
    void BounceWithCertainForce(Vector3 force);
    float SetTrampolineBounceEffect();
}
