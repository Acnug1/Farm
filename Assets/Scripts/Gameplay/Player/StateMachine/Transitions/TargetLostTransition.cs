using System.Collections.Generic;
using UnityEngine;

public class TargetLostTransition : Transition
{
    private const string PlayerRadarConfigErrorMessage = "PlayerRadarConfig is null";

    [Tooltip("—сылка на ScriptableObject: PlayerRadarConfig")]
    [SerializeField] private PlayerRadarConfig _playerRadarConfig;

    private TargetsRadar _targetsRadar;
    private float _lostTargetAngle;

    protected override void OnTransitionAwake()
    {
        Debug.Assert(_playerRadarConfig != null, PlayerRadarConfigErrorMessage);

        _targetsRadar = GetComponentInChildren<TargetsRadar>();

        _lostTargetAngle = _playerRadarConfig.LostTargetAngle;
    }

    protected override void Update()
    {
        CheckNonAvailableTargets(_targetsRadar.TargetsInRadius);
    }

    private void CheckNonAvailableTargets(IReadOnlyList<GameObject> targets)
    {
        foreach (GameObject target in targets)
        {
            if (target == null)
                continue;

            if (_targetsRadar.IsAvailableTarget(target.transform.position, _lostTargetAngle))
                return;
        }

        NeedTransit = true;
    }
}
