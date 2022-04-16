using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private float _slopeForceRayLength;
    [SerializeField]
    private float _slopeForce;

    [SerializeField]
    private Transform _groundCheck;
    [SerializeField]
    private float _distanceToGround;
    [SerializeField]
    private LayerMask _groundMask;

    public void MoveRigidbody(Vector3 vectorValue)
    {
        _rigidbody.MovePosition(_rigidbody.position + vectorValue);
    }

    public bool CheckGround()
    {
        return Physics.CheckSphere(_groundCheck.position, _distanceToGround, _groundMask);
    }

    private bool SlopeCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, _slopeForceRayLength))
        {
            if (hit.normal != Vector3.up) return true;
        }

        return false;
    }

    public void OnSlope()
    {
        if (!SlopeCheck()) return;

        _rigidbody.MovePosition(_rigidbody.position + (Vector3.down * _slopeForce * 0.05f));
    }

    public void SetVelocity(Vector3 newVelocity)
    {
        _rigidbody.velocity = newVelocity;
    }

    public Vector3 GetVelocity()
    {
        return _rigidbody.velocity;
    }

    public void AddForceImpulse(Vector3 forceVector)
    {
        _rigidbody.AddForce(forceVector,ForceMode.Impulse);
    }
}
