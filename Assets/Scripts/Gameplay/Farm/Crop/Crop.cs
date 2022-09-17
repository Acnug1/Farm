using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class Crop : MonoBehaviour
{
    private const string CropConfigErrorMessage = "CropConfig is null";

    [Tooltip("—сылка на ScriptableObject: CropConfig")]
    [SerializeField] private CropConfig _cropConfig;

    private Rigidbody _rigidbody;
    private Collider _collider;
    private Culture _culture;
    private int _cropPrice;

    public event UnityAction<Transform, int> AttachToContainer;
    public event UnityAction<Transform> Selling;
    public event UnityAction<Transform, Crop, int> Rewarding;

    public CropConfig CropConfig => _cropConfig;

    private void Awake()
    {
        Debug.Assert(_cropConfig != null, CropConfigErrorMessage);

        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();

        _cropPrice = _cropConfig.CropPrice;

        DisableKinematic(_rigidbody);
        EnableCollision(_collider);
    }

    public void Init(Culture culture)
    {
        _culture = culture;

        if (!_culture)
            throw new InvalidOperationException();
    }

    public void Reap(Transform cropContainer, int cropCount)
    {
        Harvest();

        EnableKinematic(_rigidbody);
        DisableCollision(_collider);
        SetParent(cropContainer);

        AttachToContainer?.Invoke(cropContainer, cropCount);
    }

    public void Harvest()
    {
        _culture.Harvest();
    }

    public void Sell(Transform containerForSale)
    {
        ClearParent();

        Selling?.Invoke(containerForSale);
    }

    public void GetRewardForSale(Transform containerForSale)
    {
        Rewarding?.Invoke(containerForSale, this, _cropPrice);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void SetParent(Transform cropContainer)
    {
        transform.parent = cropContainer;
    }

    private void ClearParent()
    {
        transform.parent = null;
    }

    private void EnableKinematic(Rigidbody rigidbody)
    {
        if (!rigidbody.isKinematic)
            rigidbody.isKinematic = true;
    }

    private void DisableKinematic(Rigidbody rigidbody)
    {
        if (rigidbody.isKinematic)
            rigidbody.isKinematic = false;
    }

    private void EnableCollision(Collider collider)
    {
        if (!collider.enabled)
            collider.enabled = true;
    }

    private void DisableCollision(Collider collider)
    {
        if (collider.enabled)
            collider.enabled = false;
    }
}
