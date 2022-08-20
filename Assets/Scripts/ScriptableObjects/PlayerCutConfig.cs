using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerCutConfig", menuName = "Player/Create new PlayerCutConfig", order = 51)]

public class PlayerCutConfig : ScriptableObject
{
    [Tooltip("�������� ����� �������")]
    [Min(0.5f)]
    [SerializeField] private float _cutDelay = 1f;

    public float CutDelay => _cutDelay;
}
