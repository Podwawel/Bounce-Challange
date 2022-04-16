using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceableObjectsController : MonoBehaviour
{
    [SerializeField]
    public BounceableObject[] _bouncableObject;

    public void InitializeBouncableObjects()
    {
        for (int i = 0; i < _bouncableObject.Length; i++)
        {
            _bouncableObject[i].Initialize();
        }
    }
}

