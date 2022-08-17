using UnityEngine;

public class SurfaceSlider : MonoBehaviour
{
    private readonly float _pivotOffset = 0.5f;
    private readonly float _raycastMaxDistance = 1f;

    public Vector3 GetDirectionAlongSurface(Vector3 direction, LayerMask layerMask)
    {
        return Vector3.ProjectOnPlane(direction, GetPlaneNormal(layerMask));
    }

    private Vector3 GetPlaneNormal(LayerMask layerMask)
    {
        Physics.Raycast(transform.position + new Vector3(0, _pivotOffset, 0),
            -transform.up, out RaycastHit hit, _raycastMaxDistance, layerMask);

        return hit.normal;
    }
}
