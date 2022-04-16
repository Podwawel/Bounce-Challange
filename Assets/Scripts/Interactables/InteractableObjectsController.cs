using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectsController : MonoBehaviour
{
    [SerializeField]
    public GameObject[] _interactableObject;

    public void InitializeInteractableObjects()
    {
        for (int i = 0; i < _interactableObject.Length; i++)
        {
            _interactableObject[i].GetComponent<IInteractable>().Initialize();
        }
    }

    public void DeinitializeInteractablesObjects()
    {
        for (int i = 0; i < _interactableObject.Length; i++)
        {
            _interactableObject[i].GetComponent<IInteractable>().Deinitialize();
        }
    }
}
