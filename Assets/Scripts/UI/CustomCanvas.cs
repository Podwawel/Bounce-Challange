using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomCanvas : MonoBehaviour
{
    public Canvas myCanvas { get; private set; }
    public virtual void Initialize()
    {
        GameManager.Instance.CanvasController.AddCanvasToList(this);
        myCanvas = GetComponent<Canvas>();
    }

    public virtual void Deinitialize()
    {

    }
}
