using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Crop))]

public class CropMove : MonoBehaviour
{
    private Crop _crop;
    private float _moveSpeedToContainer;
    private float _rotateSpeedToContainer;
    private float _offsetYInContainer;
    private Coroutine _moveToContainer;
    private Coroutine _rotateToContainer;
    private Coroutine _moveForSale;

    private void Awake()
    {
        _crop = GetComponent<Crop>();

        _moveSpeedToContainer = _crop.CropConfig.MoveSpeedToContainer;
        _rotateSpeedToContainer = _crop.CropConfig.RotateSpeedToContainer;
        _offsetYInContainer = _crop.CropConfig.OffsetYInContainer;
    }

    private void OnEnable()
    {
        _crop.AttachToContainer += OnAttachToContainer;
        _crop.Selling += OnSelling;
    }

    private void OnDisable()
    {
        _crop.AttachToContainer -= OnAttachToContainer;
        _crop.Selling -= OnSelling;
    }

    private void OnAttachToContainer(Transform cropContainer, int cropCount)
    {
        if (_moveToContainer != null || _rotateToContainer != null)
            return;

        _moveToContainer = StartCoroutine(MoveToContainer(cropContainer, cropCount));
        _rotateToContainer = StartCoroutine(RotateToContainer(cropContainer));
    }

    private IEnumerator MoveToContainer(Transform cropContainer, int cropCount)
    {
        var waitForEndOfFrame = new WaitForEndOfFrame();
        Vector3 positionInContainer;
        float cropOffsetY = GetCropOffsetY(cropCount, _offsetYInContainer);

        do
        {
            yield return waitForEndOfFrame;

            positionInContainer = new Vector3(cropContainer.position.x,
                cropContainer.position.y + cropOffsetY, cropContainer.position.z);

            transform.position = Vector3.MoveTowards(transform.position, positionInContainer,
                _moveSpeedToContainer * Time.deltaTime);
        }
        while (Vector3.Distance(transform.position, positionInContainer) != 0);

        ResetLocalPosition(cropCount, _offsetYInContainer);
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

    private void OnSelling(Transform containerForSale)
    {
        if (_moveForSale != null)
            return;

        _moveForSale = StartCoroutine(MoveForSale(containerForSale));
    }

    private IEnumerator MoveForSale(Transform containerForSale)
    {
        var waitForEndFrame = new WaitForEndOfFrame();

        do
        {
            yield return waitForEndFrame;

            transform.position = Vector3.MoveTowards(transform.position, containerForSale.position,
                _moveSpeedToContainer * Time.deltaTime);
        }
        while (Vector3.Distance(transform.position, containerForSale.position) != 0);

        _crop.Destroy();
    }

    private float GetCropOffsetY(int cropCount, float offsetYInContainer)
    {
        return (cropCount - 1) * offsetYInContainer;
    }

    private void ResetLocalPosition(int cropCount, float offsetYInContainer)
    {
        Vector3 localPosition = Vector3.zero;
        localPosition.y = GetCropOffsetY(cropCount, offsetYInContainer);
        transform.localPosition = localPosition;
    }

    private void ResetLocalRotation()
    {
        transform.localRotation = Quaternion.identity;
    }
}
