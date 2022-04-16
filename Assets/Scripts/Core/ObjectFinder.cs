using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFinder : MonoBehaviour
{
    public BounceableObjectsController FindBouncableObjectsController()
    {
        return FindObjectOfType<BounceableObjectsController>();
    }
    public InteractableObjectsController FindInteractableObjectsController()
    {
        return FindObjectOfType<InteractableObjectsController>();
    }
}
