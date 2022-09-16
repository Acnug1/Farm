using UnityEngine;

[DefaultExecutionOrder(-100)]

public class VictoryConditions : Singleton<VictoryConditions>
{
    private const string VictoryConditionsConfigErrorMessage = "VictoryConditionsConfig is null";

    [Tooltip("—сылка на ScriptableObject: VictoryConditionsConfig")]
    [SerializeField] private VictoryConditionsConfig _victoryConditionsConfig;

    private int _targetAmountOfMoney;

    public int TargetAmountOfMoney => _targetAmountOfMoney;

    public override void Initialize()
    {
        Debug.Assert(_victoryConditionsConfig != null, VictoryConditionsConfigErrorMessage);

        _targetAmountOfMoney = _victoryConditionsConfig.TargetAmountOfMoney;
    }
}
