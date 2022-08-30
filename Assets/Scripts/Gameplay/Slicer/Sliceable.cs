using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]

public class Sliceable : MonoBehaviour
{
    private const string SliceableObjectConfigErrorMessage = "SliceableObjectConfig is null";

    [Tooltip("—сылка на ScriptableObject: SliceableObjectConfig")]
    [SerializeField] private SliceableObjectConfig _sliceableObjectConfig;

    private Material _sliceMaterial;

    private void Awake()
    {
        Debug.Assert(_sliceableObjectConfig != null, SliceableObjectConfigErrorMessage);

        _sliceMaterial = _sliceableObjectConfig.SliceMaterial;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out MeshSlicer meshSlicer))
            meshSlicer.TryToSliceObject(gameObject, _sliceMaterial, _sliceableObjectConfig);
    }
}
