using System.Collections.Generic;
using UnityEngine;

public class TargetsRadar : MonoBehaviour
{
    private readonly List<GameObject> _targetsInRadius = new List<GameObject>();
    private readonly List<GameObject> _emptyTargets = new List<GameObject>();

    public IReadOnlyList<GameObject> TargetsInRadius => _targetsInRadius;

    private void Update()
    {
        TryFindEmptyTargets(_targetsInRadius, _emptyTargets);
    }

    public bool IsAvailableTarget(GameObject target, float angle)
    {
        if (TargetIsGrownPlant(target) || TargetIsNotPlant(target))
        {
            Vector3 direction = target.transform.position - transform.position;
            float dot = Vector3.Dot(transform.forward, direction.normalized);

            if (dot < 1)
            {
                float angleRadians = Mathf.Acos(dot);
                float angleDegrees = angleRadians * Mathf.Rad2Deg;
                return angleDegrees <= angle;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    private void TryFindEmptyTargets(List<GameObject> targetsInRadius, List<GameObject> emptyTargets)
    {
        foreach (GameObject targetInRadius in targetsInRadius)
        {
            if (!targetInRadius || !targetInRadius.activeSelf)
                emptyTargets.Add(targetInRadius);
        }

        if (emptyTargets.Count != 0)
            RemoveEmptyTargets(targetsInRadius, emptyTargets);
    }

    private void RemoveEmptyTargets(List<GameObject> targetsInRadius, List<GameObject> emptyTargets)
    {
        foreach (GameObject emptyTarget in emptyTargets)
            targetsInRadius.Remove(emptyTarget);

        emptyTargets.Clear();
    }

    private bool TargetIsGrownPlant(GameObject target) => target.TryGetComponent(out Plant plant) && plant.HarvestIsReady;

    private bool TargetIsNotPlant(GameObject target) => !target.TryGetComponent(out Plant plant);

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
