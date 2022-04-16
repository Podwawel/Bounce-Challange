using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField]
    private BounceType _bounceType;
    [SerializeField]
    protected Animator _trampolineAnimator;
    [SerializeField]
    protected float _backForceMultiplier;

    protected List<IBounceable> objectsOnTrampoline = new List<IBounceable>();

    public virtual void OnTriggerEnter(Collider other)
    {
        IBounceable bounceable = other.GetComponent<IBounceable>();

       if (bounceable == null) return;

        bounceable.CalculateNextBouncePower(_bounceType , transform);
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        IBounceable bounceable = collision.gameObject.GetComponent<IBounceable>();

        if (bounceable == null) return;

        bounceable.Bounce();

        ApplyBackForce(bounceable.NextBouncePower);

        objectsOnTrampoline.Add(bounceable);

        SetAccurateTrampolineBounceEffect(bounceable.SetTrampolineBounceEffect());
    }

    public virtual void ApplyBackForce(Vector3 power)
    {
        foreach (IBounceable bounceable in objectsOnTrampoline)
        {
            bounceable.BounceWithCertainForce(power * 0.1f * _backForceMultiplier);
        }
    }

    public virtual void SetAccurateTrampolineBounceEffect(float power)
    {
        if (power > 1) power = 1;

        _trampolineAnimator.SetFloat("Jump Power", power);

        _trampolineAnimator.SetTrigger("Jump");
    }

    public virtual void OnCollisionExit(Collision collision)
    {
        IBounceable bounceable = collision.gameObject.GetComponent<IBounceable>();

        if (bounceable == null) return;

        objectsOnTrampoline.Remove(bounceable);
       _trampolineAnimator.SetTrigger("No Jump");
    }
}
