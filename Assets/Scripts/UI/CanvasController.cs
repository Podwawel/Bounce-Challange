using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CanvasController : MonoBehaviour
{
    #region CANVASES
    [SerializeField]
    private CustomCanvas _HUDCanvas;
    public CustomCanvas HUDCanvas { get { return _HUDCanvas; } }
    #endregion

    private List<CustomCanvas> _canvasList = new List<CustomCanvas>();

    [SerializeField]
    private Canvas[] _noncustomizableCanvases;

    public void Initialize()
    {
    }

    public void SetCanvas(bool visible, CustomCanvas canvas)
    {
        if (visible) canvas.Initialize();
        else if (!visible) canvas.Deinitialize();

        canvas.myCanvas.enabled = visible;
    }

    public void AddCanvasToList(CustomCanvas canvas)
    {
        _canvasList.Add(canvas);
    }

    public void DesactivateAllCanvases()
    {
        for(int i =0; i < _canvasList.Count; i++)
        {
            SetCanvas(false, _canvasList[i]);
        }
    }
}
