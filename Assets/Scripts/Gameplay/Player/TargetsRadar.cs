using System.Collections.Generic;
using UnityEngine;

public class TargetsRadar : MonoBehaviour
{
    private readonly List<GameObject> _targetsInRadius = new List<GameObject>();

    public IReadOnlyList<GameObject> TargetsInRadius => _targetsInRadius;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Plant plant))
            TryAddTarget(plant.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Plant plant))
            TryRemoveTarget(plant.gameObject);
    }

    private void TryAddTarget(GameObject target)
    {
        if (target != null && !_targetsInRadius.Contains(target))
            _targetsInRadius.Add(target);
    }

    private void TryRemoveTarget(GameObject target)
    {
        if (target != null && _targetsInRadius.Contains(target))
            _targetsInRadius.Remove(target);
    }
}
