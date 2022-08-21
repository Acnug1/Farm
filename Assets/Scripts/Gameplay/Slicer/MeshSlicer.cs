using EzySlice;
using UnityEngine;

public class MeshSlicer : MonoBehaviour
{
    public void TryToSliceObject(GameObject objectToSlice, Material sliceMaterial)
    {
        if (objectToSlice && objectToSlice.activeSelf)
        {
            GameObject[] slicedObjects = Slice(objectToSlice,
                transform.position, transform.up, sliceMaterial);

            SetParentForSlicedObjects(slicedObjects, objectToSlice.transform.parent);

            DestroyObjectToSlice(objectToSlice);
            AddMeshCollider(slicedObjects);
            AddRigidbody(slicedObjects);
        }
    }

    private GameObject[] Slice(GameObject objectToSlice, Vector3 planeWorldPosition,
        Vector3 planeWorldDirection, Material sliceMaterial)
    {
        return objectToSlice.SliceInstantiate(planeWorldPosition, planeWorldDirection, sliceMaterial);
    }

    private void SetParentForSlicedObjects(GameObject[] slicedObjects, Transform parent)
    {
        foreach (GameObject slicedObject in slicedObjects)
        {
            slicedObject.transform.parent = parent;
            slicedObject.transform.position = default;
            // не ставит в нужную позицию
        }
    }

    private void DestroyObjectToSlice(GameObject objectToSlice)
    {
        Transform parent = objectToSlice.transform.parent;

        if (parent != null && parent.TryGetComponent(out Culture culture))
        {
            // не удаляет
            culture.Destroy(objectToSlice.GetComponent<Plant>());
        }
    }

    private void AddMeshCollider(GameObject[] slicedObjects)
    {
        foreach (GameObject sliceObject in slicedObjects)
        {
            MeshCollider collider = sliceObject.AddComponent<MeshCollider>();
            collider.convex = true;
        }
    }

    private void AddRigidbody(GameObject[] slicedObjects)
    {
        foreach (GameObject sliceObject in slicedObjects)
        {
            sliceObject.AddComponent<Rigidbody>();
        }
    }
}
