using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private int _damageBase;
    [SerializeField]
    private int _damageRngRatio;

    private int _calculatedDamage;

    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private WeaponStyle _weaponStyle;
    [SerializeField]
    private WeaponType _weaponType;
    [SerializeField]
    private float _weaponRange;
    [SerializeField]
    private float _weaponInHandScale;
    [SerializeField]
    private Vector3 _handCameraPosition;

    [SerializeField]
    private Transform _shotPoint;
    [SerializeField]
    private Bullet[] _bullets;
    [SerializeField]
    private float _bulletsDespawnTime;
    [SerializeField]
    private int _maxAmmo;

    private Queue<Bullet> _bulletQueue = new Queue<Bullet>();

    public WeaponType WeaponType { get { return _weaponType; } }
    public WeaponStyle WeaponStyle { get { return _weaponStyle; } }
    public float WeaponRange { get { return _weaponRange; } }
    public void Initialize()
    {
        _bulletQueue.Clear();

        for (int i = 0; i < _bullets.Length; i++)
        {
            _bullets[i].Initialize(this);
        }
            EnqueueBullets();

        PullOut();
    }

    public void UpdateWapon()
    {
        if (_weaponType == WeaponType.NONE) return;
    }

    public void Action()
    {
        if (_weaponType == WeaponType.NONE) return;

        if(_weaponStyle == WeaponStyle.MELEE)
        {
            Swing();
        }
        else if (_weaponStyle == WeaponStyle.RANGED)
        {
            Shoot();
        }
    }

    public void ScaleUp()
    {
        transform.localScale *= _weaponInHandScale;
    }

    public void ScaleDown()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    public void SetHandCameraPosition()
    {
        GameManager.Instance.PlayerController.HandCamera.transform.localPosition = _handCameraPosition;
    }

    private void Swing()
    {
        _animator.SetTrigger("Swing");
    }

    private void Shoot()
    {
        if (_bulletQueue.Count == 0) return;

        Bullet newBullet = _bulletQueue.Dequeue();

        CalculateDamage();
        newBullet.Damage = _calculatedDamage; 
        newBullet.Shot(_shotPoint);

        newBullet.CountdownToDespawn(_bulletsDespawnTime);
    }

    public void CalculateDamage()
    {
        int positiveRatio, negativeRatio;

        positiveRatio = Random.Range(0, _damageRngRatio);
        negativeRatio = Random.Range(0, _damageRngRatio);

        if (positiveRatio > negativeRatio) _calculatedDamage = _damageBase + positiveRatio;
        else _calculatedDamage = _damageBase - negativeRatio;

        IDamageable damageable = GameManager.Instance.RaycastController.CombatRaycastCheck();

        if (damageable != null)
        {
            damageable.Damage(_calculatedDamage);
        }
        else Debug.Log("NO ENEMY IN RANGE");
    }

    public void PullOut()
    {
        gameObject.SetActive(true);
        Debug.Log("PULL OUT");
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        Debug.Log("HIDE");
    }

    public void Reload()
    {
        if (_weaponType == WeaponType.NONE) return;
        else if (_weaponStyle == WeaponStyle.MELEE) return;

        EnqueueBullets();
    }

    private void EnqueueBullets()
    {
        if (_weaponStyle == WeaponStyle.RANGED)
        {
            for (int i = 0; i < _maxAmmo; i++)
            {
                _bullets[i].Spawn();
                _bulletQueue.Enqueue(_bullets[i]);
            }
        }
    }

    public void Deinitialize()
    {

    }
}
