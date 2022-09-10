using System.Collections.Generic;
using UnityEngine;

public class TargetDetectedTransition : Transition
{
    private const string PlayerRadarConfigErrorMessage = "PlayerRadarConfig is null";

    [Tooltip("—сылка на ScriptableObject: PlayerRadarConfig")]
    [SerializeField] private PlayerRadarConfig _playerRadarConfig;

    private TargetsRadar _targetsRadar;
    private float _detectionTargetAngle;

    protected override void OnTransitionAwake()
    {
        Debug.Assert(_playerRadarConfig != null, PlayerRadarConfigErrorMessage);

        _targetsRadar = GetComponentInChildren<TargetsRadar>();

        _detectionTargetAngle = _playerRadarConfig.DetectionTargetAngle;
    }

    protected override void Update()
    {
        TryFindAvailableTarget(_targetsRadar.TargetsInRadius);
    }

    private void TryFindAvailableTarget(IReadOnlyList<GameObject> targets)
    {
        foreach (GameObject target in targets)
        {
            if (!target || !target.activeSelf)
                continue;

            if (_targetsRadar.IsAvailableTarget(target, _detectionTargetAngle))
            {
                NeedTransit = true;
                break;
            }
        }
    }
}
