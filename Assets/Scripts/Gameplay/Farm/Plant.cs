using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Renderer))]

public class Plant : MonoBehaviour
{
    public event UnityAction HarvestReady;

    private Collider _collider;
    private Renderer _renderer;

    public event UnityAction PlantDestroy;

    public bool HarvestIsReady { get; private set; } = false;
    public Color StartColor { get; private set; }

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _renderer = GetComponent<Renderer>();
        StartColor = _renderer.material.color;

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

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
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
