using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetsRadar))]

public class TargetDetectedTransition : Transition
{
    [Tooltip("Угол зоны видимости игрока, при котором он начнет срезать растение")]
    [Range(0, 180)]
    [SerializeField] private float _detectionAngle;

    private TargetsRadar _targetsRadar;

    protected override void OnTransitionAwake()
    {
        _targetsRadar = GetComponent<TargetsRadar>();
    }

    protected override void Update()
    {
        TryFindAvailableTarget(_targetsRadar.TargetsInRadius);
    }

    private void TryFindAvailableTarget(IReadOnlyList<GameObject> targetsInRadius)
    {
        foreach (GameObject target in targetsInRadius)
        {
            if (IsAvailableTarget(target.transform.position, _detectionAngle))
                NeedTransit = true;
        }
    }

    private bool IsAvailableTarget(Vector3 target, float angle)
    {
        Vector3 direction = (target - transform.position);
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
}
