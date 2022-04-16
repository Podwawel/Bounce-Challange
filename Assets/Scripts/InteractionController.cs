using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public enum InteractionKey
{
    GRABBING_INTERACTION,
    COMMON_INTERACTION,
    KICKING_INTERACTION,
}

[Serializable]
public struct InteractionData
{
    public Canvas MessageTypeCanvas;
    public InteractionKey Key;
}

public class InteractionController : MonoBehaviour
{
    private Canvas _currentMessageType;

    [SerializeField]
    private InteractionData[] _interactionData;

    public void ActivateInteractCanvasByType(InteractionKey key)
    {
        for (int i=0;i<_interactionData.Length;i++)
        {
            if(_interactionData[i].Key == key)
            {
                _interactionData[i].MessageTypeCanvas.enabled = true;
                if (_currentMessageType != null && _currentMessageType != _interactionData[i].MessageTypeCanvas)
                {
                    _currentMessageType.enabled = false;
                    _currentMessageType = _interactionData[i].MessageTypeCanvas;
                }

                return;
            }
        }

        Debug.LogError("CUSTOM ERROR: no interaztion key found.");
    }

    public void DesactivateInteractionCanvases()
    {
        for (int i = 0; i < _interactionData.Length; i++)
        {
            _interactionData[i].MessageTypeCanvas.enabled = false;
        }
    }
}
