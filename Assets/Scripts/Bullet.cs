using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public int Damage;

    private float _despawnTimeElapsed;
    private WaitForEndOfFrame _waitForEndOfFrame;

    [SerializeField]
    private Rigidbody _bulletRigidbody;
    private Weapon _myWeapon;

    [SerializeField]
    private MeshRenderer _renderer;

    [SerializeField]
    private GameObject _myFakeBall;

    [SerializeField]
    private float _forceApplied;

    [SerializeField]
    private Collider _collider;

    public void Initialize(Weapon myWeapon)
    {
        _myWeapon = myWeapon;
    }   
    
    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if(damageable != null)
        {
            damageable.Damage(Damage);
        }
    }

    public void Spawn()
    {
        _myFakeBall.SetActive(true);
    }

    public void CountdownToDespawn(float timeToDespawn)
    {
        StartCoroutine(DespawnBulletCoroutine(timeToDespawn));
    }

    private IEnumerator DespawnBulletCoroutine(float time)
    {
        _waitForEndOfFrame = new WaitForEndOfFrame();
        _despawnTimeElapsed = 0;
        while(_despawnTimeElapsed <= time)
        {
            _despawnTimeElapsed += Time.deltaTime;
            yield return _waitForEndOfFrame;
        }

        Despawn();
    }

    public void Shot(Transform shotPoint)
    {
        transform.position = shotPoint.position;
        transform.SetParent(null);
        _myFakeBall.SetActive(false);
        _collider.enabled = true;
        _renderer.enabled = true;
        _bulletRigidbody.velocity = _myWeapon.transform.forward * _forceApplied;
    }

    private void Despawn()
    {
        _despawnTimeElapsed = 0;
        _renderer.enabled = false;
        _collider.enabled = false;
    }
}
