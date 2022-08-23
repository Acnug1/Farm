using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]

public class Plant : MonoBehaviour
{
    [SerializeField] private UnityEvent _onHarvestReady;

    private Collider _collider;

    public event UnityAction PlantDestroy;

    public bool HarvestIsReady { get; private set; } = false;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        DisableCollision(_collider);
    }

    public void Destroy()
    {
        PlantDestroy?.Invoke();
    }

    public void SetScaleY(float currentScaleY)
    {
        transform.localScale = new Vector3(transform.localScale.x, currentScaleY, transform.localScale.z);
    }

    public void FinishGrowth()
    {
        EnableCollision(_collider);
        HarvestIsReady = true;
        _onHarvestReady?.Invoke();
    }

    private void DisableCollision(Collider collider)
    {
        if (collider.enabled)
            collider.enabled = false;
    }

    private void EnableCollision(Collider collider)
    {
        if (!collider.enabled)
            collider.enabled = true;
    }
}
