using System.Collections;
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
    private Coroutine _waitingBeforeSale;

    public event UnityAction<Transform, int> AttachToContainer;
    public event UnityAction<Transform> Selling;

    public CropConfig CropConfig => _cropConfig;

    private void Awake()
    {
        Debug.Assert(_cropConfig != null, CropConfigErrorMessage);

        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();

        DisableKinematic(_rigidbody);
        EnableCollision(_collider);
    }

    public void Init(Culture culture)
    {
        _culture = culture;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void Reap(Transform cropContainer, int cropCount)
    {
        if (_culture != null)
        {
            _culture.Reap();

            EnableKinematic(_rigidbody);
            DisableCollision(_collider);
            SetParent(cropContainer);

            AttachToContainer?.Invoke(cropContainer, cropCount);
        }
    }

    public void Sell(Transform containerForSale, float waitingTime)
    {
        if (_waitingBeforeSale != null)
            return;

        _waitingBeforeSale = StartCoroutine(WaitingBeforeSale(containerForSale, waitingTime));
    }

    private IEnumerator WaitingBeforeSale(Transform containerForSale, float waitingTime)
    {
        var waitForSeconds = new WaitForSeconds(waitingTime);

        yield return waitForSeconds;

        ClearParent();

        Selling?.Invoke(containerForSale);
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
