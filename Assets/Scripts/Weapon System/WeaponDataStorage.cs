using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum WeaponType
{
    NONE,
    MAKARONI,    
    TENNIS_BALL_CANNON,
}

[Serializable]
public enum WeaponStyle
{
    MELEE,
    RANGED,
}

[Serializable]
public struct WeaponData
{
    public GameObject WeaponPrefab;
    public WeaponType WeaponType;
}

[CreateAssetMenu(fileName = "WeaponDataStorage", menuName = "Data/WeaponDataStorage")]
public class WeaponDataStorage : ScriptableObject
{
    [SerializeField]
    private WeaponData[] _weaponData;

    public WeaponData[] GetWeaponsStorage()
    {
        return _weaponData;
    }

    public Weapon GetWeaponByType(WeaponType weaponType)
    {
        for(int i=0;i<_weaponData.Length; i++)
        {
            if (_weaponData[i].WeaponType == weaponType) return _weaponData[i].WeaponPrefab.GetComponent<Weapon>();

        }

        Debug.LogError("CUSTOM ERROR: Weapon type not found.");
        return null;
    }
}
