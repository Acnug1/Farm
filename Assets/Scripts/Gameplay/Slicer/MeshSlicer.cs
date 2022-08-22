using EzySlice;
using UnityEngine;

public class MeshSlicer : MonoBehaviour
{
    public void TryToSliceObject(GameObject objectToSlice, Material sliceMaterial, 
        SliceableObjectConfig sliceableObjectConfig)
    {
        if (objectToSlice && objectToSlice.activeSelf)
        {
            GameObject[] slicedObjects = Slice(objectToSlice,
                transform.position, transform.up, sliceMaterial);

            if (slicedObjects != null)
            {
                Vector3 spawnPosition = objectToSlice.transform.position;

                TryDestroyObjectToSlice(objectToSlice);

                foreach (GameObject slicedObject in slicedObjects)
                {
                    SetPosition(slicedObject, spawnPosition);
                    AddMeshCollider(slicedObject);
                    AddRigidbody(slicedObject);
                    AddFragmentComponent(slicedObject, sliceableObjectConfig);
                }
            }
        }
    }

    private GameObject[] Slice(GameObject objectToSlice, Vector3 planeWorldPosition,
        Vector3 planeWorldDirection, Material sliceMaterial)
    {
        return objectToSlice.SliceInstantiate(planeWorldPosition, planeWorldDirection, sliceMaterial);
    }

    private void TryDestroyObjectToSlice(GameObject objectToSlice)
    {
        if (objectToSlice.TryGetComponent(out Plant plant))
            plant.Destroy();
    }

    private void SetPosition(GameObject slicedObject, Vector3 position)
    {
        slicedObject.transform.position = new Vector3(position.x, slicedObject.transform.position.y, position.z);
    }

    private void AddMeshCollider(GameObject slicedObject)
    {
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
    }

    private void AddRigidbody(GameObject slicedObject)
    {
        slicedObject.AddComponent<Rigidbody>();
    }

    private void AddFragmentComponent(GameObject slicedObject, SliceableObjectConfig sliceableObjectConfig)
    {
        Fragment fragment = slicedObject.AddComponent<Fragment>();
        fragment.Init(sliceableObjectConfig);
    }
}
