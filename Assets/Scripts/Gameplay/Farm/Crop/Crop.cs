using System.Collections;
using UnityEngine;

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
    private float _moveSpeedToContainer;
    private float _rotateSpeedToContainer;
    private float _offsetYInContainer;
    private Coroutine _moveToContainer;
    private Coroutine _rotateToContainer;

    private void Awake()
    {
        Debug.Assert(_cropConfig != null, CropConfigErrorMessage);

        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();

        DisableKinematic(_rigidbody);
        EnableCollision(_collider);

        _moveSpeedToContainer = _cropConfig.MoveSpeedToContainer;
        _rotateSpeedToContainer = _cropConfig.RotateSpeedToContainer;
        _offsetYInContainer = _cropConfig.OffsetYInContainer;
    }

    public void Init(Culture culture)
    {
        _culture = culture;
    }

    public void Reap(Transform cropContainer, int cropCount)
    {
        if (_culture != null)
        {
            _culture.Reap();

            EnableKinematic(_rigidbody);
            DisableCollision(_collider);
            SetParent(cropContainer);

            if (_moveToContainer != null || _rotateToContainer != null)
                return;

            _moveToContainer = StartCoroutine(MoveToContainer(cropContainer, cropCount));
            _rotateToContainer = StartCoroutine(RotateToContainer(cropContainer));
        }
    }

    private IEnumerator MoveToContainer(Transform cropContainer, int cropCount)
    {
        var waitForEndOfFrame = new WaitForEndOfFrame();
        Vector3 positionInContainer;
        float cropOffsetY = GetCropOffsetY(cropCount);

        do
        {
            yield return waitForEndOfFrame;

            positionInContainer = new Vector3(cropContainer.position.x,
                cropContainer.position.y + cropOffsetY, cropContainer.position.z);

            transform.position = Vector3.MoveTowards(transform.position, positionInContainer,
                _moveSpeedToContainer * Time.deltaTime);
        }
        while (Vector3.Distance(transform.position, positionInContainer) != 0);

        ResetLocalPosition(cropCount);
    }

    private IEnumerator RotateToContainer(Transform cropContainer)
    {
        var waitForEndOfFrame = new WaitForEndOfFrame();

        do
        {
            yield return waitForEndOfFrame;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, cropContainer.rotation,
                _rotateSpeedToContainer * Time.deltaTime);
        }
        while (Quaternion.Angle(transform.rotation, cropContainer.rotation) != 0);

        ResetLocalRotation();
    }

    private void SetParent(Transform cropContainer)
    {
        transform.parent = cropContainer;
    }

    private float GetCropOffsetY(int cropCount)
    {
        return (cropCount - 1) * _offsetYInContainer;
    }

    private void ResetLocalPosition(int cropCount)
    {
        Vector3 localPosition = Vector3.zero;
        localPosition.y = GetCropOffsetY(cropCount);
        transform.localPosition = localPosition;
    }

    private void ResetLocalRotation()
    {
        transform.localRotation = Quaternion.identity;
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
