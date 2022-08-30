using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]

public class Plant : MonoBehaviour
{
    private const string PlantConfigErrorMessage = "PlantConfig is null";

    [Tooltip("—сылка на ScriptableObject: PlantConfig")]
    [SerializeField] private PlantConfig _plantConfig;

    private Collider _collider;

    public event UnityAction HarvestReady;
    public event UnityAction PlantDestroy;

    public PlantConfig PlantConfig => _plantConfig;
    public bool HarvestIsReady { get; private set; } = false;

    private void Awake()
    {
        Debug.Assert(_plantConfig != null, PlantConfigErrorMessage);

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
        HarvestReady?.Invoke();
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
