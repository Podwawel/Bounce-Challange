using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private WeaponDataStorage _weaponDataStorage;

    private List<GameObject> _weaponToChoose = new List<GameObject>();

    private Weapon _currentlyHoldWeapon;

    public WeaponType CurrentWeaponType;
    public WeaponStyle CurrentWeaponStyle;
    public float CurrentWeaponRange;

    private int _currentlyHoldWeaponIndex;
    
    public void Initialize()
    {
        Unlock(WeaponType.NONE);
        Unlock(WeaponType.TENNIS_BALL_CANNON);
        _currentlyHoldWeaponIndex = 0;
        _currentlyHoldWeapon = _weaponToChoose[0].GetComponent<Weapon>();
        _currentlyHoldWeapon.Initialize();
    }

    public void UpdateCurrentWeapon()
    {
        _currentlyHoldWeapon.UpdateWapon();
    }
    
    public void Unlock(WeaponType weaponType)
    {
        Weapon weapon = _weaponDataStorage.GetWeaponByType(weaponType);
        GameObject obj;
        obj = Instantiate(weapon.gameObject, Camera.main.transform);
       _weaponToChoose.Add(obj);
        obj.SetActive(false);
    }

    public void Action()
    {
        _currentlyHoldWeapon.Action();
    }

    public void ChangeWeapon()
    {
        if (GameManager.Instance.PlayerController.PlayerActions.IsHoldingGrabbable) return;

        if (_currentlyHoldWeapon != null)
        {
            _currentlyHoldWeapon.Hide();
            _currentlyHoldWeapon.ScaleDown();
        }

        CalculateNextWeaponIndex();

        _currentlyHoldWeapon = _weaponToChoose[_currentlyHoldWeaponIndex].GetComponent<Weapon>();

        CurrentWeaponType = _currentlyHoldWeapon.WeaponType;
        CurrentWeaponStyle = _currentlyHoldWeapon.WeaponStyle;
        CurrentWeaponRange = _currentlyHoldWeapon.WeaponRange;

        _currentlyHoldWeapon.Initialize();
        _currentlyHoldWeapon.ScaleUp();
        _currentlyHoldWeapon.SetHandCameraPosition();

        if (CurrentWeaponType != WeaponType.NONE)
        GameManager.Instance.PlayerController.PlayerActions.SnapObjectToGrabbingPoint(_currentlyHoldWeapon.gameObject, 1);
    }

    private void CalculateNextWeaponIndex()
    {
        int maxIndex = _weaponToChoose.Count-1;

        if (_currentlyHoldWeaponIndex == maxIndex) _currentlyHoldWeaponIndex = 0;
        else _currentlyHoldWeaponIndex++;
    }

    public void ReloadWeapon()
    {
        _currentlyHoldWeapon.Reload();
    }

    public void Deinitialize()
    {

    }
}
