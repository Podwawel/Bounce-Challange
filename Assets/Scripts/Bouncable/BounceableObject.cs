using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceableObject : MonoBehaviour, IBounceable
{
    private Rigidbody _rigidbody;

    [SerializeField]
    private float _bounceMultiplierAfterFirstBounce;
    [SerializeField]
    private float _bounciness;
    [SerializeField]
    private float _maxVertcialVelocityValue;

    private Vector3 _nextBouncePower;
    public Vector3 NextBouncePower { get { return _nextBouncePower; } set { _nextBouncePower = value; } }
    public float BounceMultiplierAfterFirstBounce { get { return _bounceMultiplierAfterFirstBounce; } set { _bounceMultiplierAfterFirstBounce = value; } }
    public float Bounciness { get { return _bounciness; } }

    private float _bounceMultiplier;
    public float BounceMultiplier { get { return _bounceMultiplier; } set { _bounceMultiplier = value; } }
    private bool _isInBounceArea;
    public bool IsInBounceArea { get { return _isInBounceArea; } set { _isInBounceArea = value; } }

    private float _maxVerticalVelocity2ndStageValue;
    private float _forceUnit;

    public void Initialize()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _nextBouncePower = Vector3.zero;
        _bounceMultiplier = 0;
        _maxVerticalVelocity2ndStageValue = 2f;

        _forceUnit = 1 / _maxVertcialVelocityValue;
    }

    public void BounceableUpdate()
    {
        if (!_isInBounceArea)
        {
            _bounceMultiplier = 0;
            _nextBouncePower = Vector3.zero;
        }
    }

    public void Bounce()
    {
        BounceWithCertainForce(_nextBouncePower);
    }

    public Vector3 GetVelocity()
    {
        return _rigidbody.velocity;
    }

    public void SetVelocity(Vector3 velocity)
    {
        _rigidbody.velocity = velocity;
    }

    public void BounceWithCertainForce(Vector3 force)
    {
        SetVelocity(force);
    }

    public void CalculateNextBouncePower(BounceType bounceType, Transform trampolineTransform)
    {
        Vector3 currentVelocity = GetVelocity();

        if (currentVelocity.y == 0)
        {
            return;
        }

        if (bounceType == BounceType.HORIZONTAL_BOUNCE)
        {
            CalculateHorizontalBounce(currentVelocity);
        }
        else if (bounceType == BounceType.ANGLE_BOUNCE)
        {
            CalculateAnyAngleBounce(currentVelocity, trampolineTransform);
        }

        if (_nextBouncePower.y < 1f)
        {
            _nextBouncePower.y = 0;
        }
        if (_nextBouncePower.y < 0.1f)
        {
            _bounceMultiplier = 0;
        }

        }

    #region BOUNCE_CALCULATION
    private void CalculateHorizontalBounce(Vector3 velocityVector)
    {
        _nextBouncePower = new Vector3(0, 1, 0) * _nextBouncePower.y;

        if (_bounceMultiplier == 0)
        {
            _nextBouncePower = new Vector3(0f, Mathf.Abs(velocityVector.y), 0f) * _bounciness;
            _bounceMultiplier = _bounceMultiplierAfterFirstBounce;
        }
        else if(_nextBouncePower.y >= _maxVertcialVelocityValue)
        {
            _nextBouncePower.y *= _bounceMultiplier - (_bounceMultiplier * 0.1f);
        }
        else if(_nextBouncePower.y >= _maxVerticalVelocity2ndStageValue && _bounceMultiplier < _maxVertcialVelocityValue)
        {
            _nextBouncePower.y *= _bounceMultiplier - (_bounceMultiplier * 0.2f);
        }
        else if(_nextBouncePower.y < _maxVerticalVelocity2ndStageValue)
        {
            _nextBouncePower.y *= _bounceMultiplier - (_bounceMultiplier * 0.3f);
        }
    }

    private void CalculateAnyAngleBounce(Vector3 velocityVector, Transform trampolineTransform)
    {
        _nextBouncePower = new Vector3(trampolineTransform.forward.x * 0.5f, 1, trampolineTransform.forward.z * 0.5f) * _nextBouncePower.y;

        if (_bounceMultiplier == 0)
        {
            _nextBouncePower = new Vector3(trampolineTransform.forward.x * 0.5f, 1, trampolineTransform.forward.z * 0.5f) * Mathf.Abs(velocityVector.y);
            _bounceMultiplier = _bounceMultiplierAfterFirstBounce;
        }
        else if (_nextBouncePower.y >= _maxVertcialVelocityValue)
        {
            _nextBouncePower.y *= _bounceMultiplier - (_bounceMultiplier * 0.1f);
        }
        else if (_nextBouncePower.y >= _maxVerticalVelocity2ndStageValue && _bounceMultiplier < _maxVertcialVelocityValue)
        {
            _nextBouncePower.y *= _bounceMultiplier - (_bounceMultiplier * 0.2f);
        }
        else if (_nextBouncePower.y < _maxVerticalVelocity2ndStageValue)
        {
            _nextBouncePower.y *= _bounceMultiplier - (_bounceMultiplier * 0.3f);
        }
    }

    public float SetTrampolineBounceEffect()
    {
        return _forceUnit * _nextBouncePower.y;
    }

    #endregion
}
