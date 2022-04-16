using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int _healthPoints;

    public void Damage(int damage)
    {
        if (_healthPoints <= 0) return;

        _healthPoints -= damage;

        Debug.Log(gameObject.name + " GOT HIT WITH " + damage + " DAMAGE.");
        if (_healthPoints <= 0) gameObject.SetActive(false);
    }
}
