using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField]
    protected InteractionKey _interactionKey;

    protected Renderer _renderer;
    protected Material _material;
    protected PlayerController _playerController;
    protected Color _color;
    protected CanvasController _canvasController;
    protected InteractionController _interactionController;
    protected Color _highlightColor;

    public virtual void Initialize()
    {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;
        _playerController = GameManager.Instance.PlayerController;
        _canvasController = GameManager.Instance.CanvasController;
        _interactionController = GameManager.Instance.InteractionController;
        _highlightColor = new Vector4(0.2f, 0.2f, 0.2f, 1);
    }

    public virtual void OnRaycastHit()
    {
        _interactionController.ActivateInteractCanvasByType(_interactionKey);

        _color = _material.color;
        _material.color = _highlightColor;
        _playerController.PlayerActions.OnInteractEvent += Action;
    }

    public virtual void OnRaycastUpdate()
    {

    }

    public virtual void OnRaycastEnd()
    {
        _material.color = _color;
        _interactionController.DesactivateInteractionCanvases();
        _playerController.PlayerActions.OnInteractEvent -= Action;
    }

    public virtual void Action()
    {
        _interactionController.DesactivateInteractionCanvases();
    }

    public virtual void Deinitialize()
    {

    }
}
