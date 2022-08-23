using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]

public class Sliceable : MonoBehaviour
{
    private const string SliceableObjectConfigErrorMessage = "SliceableObjectConfig is null";

    [Tooltip("—сылка на ScriptableObject: SliceableObjectConfig")]
    [SerializeField] private SliceableObjectConfig _sliceableObjectConfig;
    [SerializeField] private UnityEvent<Vector3> _onObjectSliced;

    private Material _sliceMaterial;

    private void Awake()
    {
        Debug.Assert(_sliceableObjectConfig != null, SliceableObjectConfigErrorMessage);

        _sliceMaterial = _sliceableObjectConfig.SliceMaterial;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out MeshSlicer meshSlicer))
        {
            Vector3 contactPointPosition = collision.GetContact(0).point;
            _onObjectSliced?.Invoke(contactPointPosition);

            meshSlicer.TryToSliceObject(gameObject, _sliceMaterial, _sliceableObjectConfig);
        }
    }
}
