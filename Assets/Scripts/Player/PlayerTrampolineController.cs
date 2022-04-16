using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrampolineController : MonoBehaviour, IBounceable
{
    [SerializeField]
    private PlayerPhysics _objectPhysics;
    [SerializeField]
    private float _bounceMultiplierOnSquat;
    [SerializeField]
    private float _bounceMultiplierAfterFirstBounce;
    [SerializeField]
    private float _bounciness;
    [SerializeField]
    private float _maxVertcialVelocityValue;
    [SerializeField]
    private float _maxVerticalVelocityValueRange;

    #region BOUNCE
    private Vector3 _nextBouncePower;
    public Vector3 NextBouncePower { get { return _nextBouncePower; } set { _nextBouncePower = value; } }
    public float BounceMultiplierOnSquat { get { return _bounceMultiplierOnSquat; } set { _bounceMultiplierOnSquat = value; } }
    public float BounceMultiplierAfterFirstBounce { get { return _bounceMultiplierAfterFirstBounce; } set { _bounceMultiplierAfterFirstBounce = value; } }
    public float Bounciness { get { return _bounciness; } }

    private float _bounceMultiplier;
    public float BounceMultiplier { get { return _bounceMultiplier; } set { _bounceMultiplier = value; } }
    private bool _isInBounceArea;
    public bool IsInBounceArea { get { return _isInBounceArea; } set { _isInBounceArea = value; } }
    #endregion

    private float _maxVerticalVelocity3rdStageValue;
    private float _maxVerticalVelocity2ndStageValue;
    private float _forceUnit;


    public void Initialize()
    {
        _nextBouncePower = Vector3.zero;
        _bounceMultiplier = 0;

        _maxVerticalVelocity3rdStageValue = _maxVertcialVelocityValue - _maxVerticalVelocityValueRange;
        _maxVerticalVelocity2ndStageValue = _maxVertcialVelocityValue - _maxVerticalVelocityValueRange*2;
        _forceUnit = 1 / _maxVertcialVelocityValue;
    }

    public void BounceableUpdate()
    {
        if (_objectPhysics.CheckGround() && !_isInBounceArea)
        {
            _bounceMultiplier = 0;
            _nextBouncePower = Vector3.zero;
        }
    }

    public void Bounce()
    {
        BounceWithCertainForce(_nextBouncePower * _bounciness);
    }

    public void BounceWithCertainForce(Vector3 force)
    {
        _objectPhysics.SetVelocity(force);
    }

    public void CalculateNextBouncePower(BounceType bounceType, Transform trampolineTransform)
    {
        Vector3 currentVelocity = _objectPhysics.GetVelocity();

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

        if(_nextBouncePower.y < 1 && _bounceMultiplier == _bounceMultiplierAfterFirstBounce)
        {
            _nextBouncePower.y = _bounceMultiplierAfterFirstBounce * 2;
        }
        else if (_nextBouncePower.y < 1 && _bounceMultiplier == _bounceMultiplierOnSquat)
        {
            _nextBouncePower.y = 0;
        }
    }

    #region BOUNCE_CALCULATION
    private void CalculateHorizontalBounce(Vector3 velocityVector)
    {
        _nextBouncePower = new Vector3(0, 1, 0) * _nextBouncePower.y;

        if (_bounceMultiplier == 0)
        {
            _nextBouncePower = new Vector3(0f, Mathf.Abs(velocityVector.y), 0f);
            _bounceMultiplier = _bounceMultiplierAfterFirstBounce;
        }
        else if (_nextBouncePower.y <= _maxVerticalVelocity2ndStageValue)
        {
            _nextBouncePower *= _bounceMultiplier;
        }
        else if (_nextBouncePower.y > _maxVerticalVelocity2ndStageValue && _nextBouncePower.y <= _maxVerticalVelocity3rdStageValue)
        {
            _nextBouncePower.y *= _bounceMultiplier - (_bounceMultiplier * 0.10f);
        }
        else if (_nextBouncePower.y > _maxVerticalVelocity3rdStageValue)
        {
            if (_bounceMultiplier == _bounceMultiplierOnSquat) _nextBouncePower *= _bounceMultiplier;
            else _nextBouncePower.y = Random.Range(_maxVertcialVelocityValue - _maxVerticalVelocityValueRange, _maxVertcialVelocityValue);
        }
    }

    private void CalculateAnyAngleBounce(Vector3 velocityVector, Transform trampolineTransform)
    {
        _nextBouncePower = new Vector3(trampolineTransform.forward.x * 0.5f, 1, trampolineTransform.forward.z * 0.5f) * _nextBouncePower.y;

        if (_bounceMultiplier == 0)
        {
            _nextBouncePower = new Vector3(trampolineTransform.forward.x * 0.5f , velocityVector.y, trampolineTransform.forward.z * 0.5f) ;
            _bounceMultiplier = _bounceMultiplierAfterFirstBounce;
        }
        else if (_nextBouncePower.y <= _maxVerticalVelocity2ndStageValue)
        {
            _nextBouncePower *= _bounceMultiplier;
        }
        else if (_nextBouncePower.y > _maxVerticalVelocity2ndStageValue && _nextBouncePower.y <= _maxVerticalVelocity3rdStageValue)
        {       
            _nextBouncePower.y *= _bounceMultiplier - (_bounceMultiplier * 0.10f);
        }
        else if (_nextBouncePower.y > _maxVerticalVelocity3rdStageValue)
        {         
            if (_bounceMultiplier == _bounceMultiplierOnSquat) _nextBouncePower *= _bounceMultiplier;
            else _nextBouncePower.y = Random.Range(_maxVertcialVelocityValue - _maxVerticalVelocityValueRange, _maxVertcialVelocityValue);
        }
    }

    public float SetTrampolineBounceEffect()
    {
        return _forceUnit * _nextBouncePower.y;
    }

    #endregion
}
