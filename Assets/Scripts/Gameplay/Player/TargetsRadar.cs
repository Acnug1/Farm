using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetsRadar : MonoBehaviour
{
    private readonly List<GameObject> _targetsInRadius = new List<GameObject>();

    public IReadOnlyList<GameObject> TargetsInRadius => _targetsInRadius;

    private void Update()
    {
        CheckNullElementsInList(_targetsInRadius);
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

    private void CheckNullElementsInList(List<GameObject> targetsInRadius)
    {
        GameObject[] nullElements = targetsInRadius.Where(targetInRadius => targetInRadius == null).ToArray();

        if (nullElements.Length != 0)
            RemoveNullElements(targetsInRadius, nullElements);
    }

    private void RemoveNullElements(List<GameObject> targetsInRadius, GameObject[] nullElements)
    {
        foreach (GameObject nullElement in nullElements)
            targetsInRadius.Remove(nullElement);
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
