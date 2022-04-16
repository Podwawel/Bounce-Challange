using UnityEngine;

public class BounceArea : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        IBounceable bounceable = other.GetComponentInParent<IBounceable>();

        if (bounceable == null) return;

        bounceable.IsInBounceArea = true;
    }

    private void OnTriggerExit(Collider other)
    {
        IBounceable bounceable = other.GetComponentInParent<IBounceable>();

        if (bounceable == null) return;

        bounceable.IsInBounceArea = false;
    }
}
