using UnityEngine;
using UnityEngine.Events;

public class Sliceable : MonoBehaviour
{
    [SerializeField] private Material _sliceMaterial;
    [SerializeField] private UnityEvent<Vector3> _onObjectSliced;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);

        if (collision.transform.TryGetComponent(out MeshSlicer meshSlicer))
        {
            // не находит коллайдер
            Debug.Log("Contact");
            Vector3 contactPointPosition = collision.GetContact(0).point;
            _onObjectSliced?.Invoke(contactPointPosition);

            meshSlicer.TryToSliceObject(gameObject, _sliceMaterial);
        }
    }
}
