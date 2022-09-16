using UnityEngine;

[CreateAssetMenu(fileName = "New VictoryConditionsConfig", menuName = "VictoryConditions/Create new VictoryConditionsConfig", order = 51)]

public class VictoryConditionsConfig : ScriptableObject
{
    [Tooltip("���������� �����, ������� ����� ���������� ��� ����������� ������")]
    [Min(1)]
    [SerializeField] private int _targetAmountOfMoney;

    public int TargetAmountOfMoney => _targetAmountOfMoney;
}
