using UnityEngine;

[CreateAssetMenu(fileName = "New CropConfig", menuName = "Crop/Create new CropConfig", order = 51)]

public class CropConfig : ScriptableObject
{
    [Tooltip("Скорость движения стака урожая, при сборе в контейнер")]
    [Min(1f)]
    [SerializeField] private float _moveSpeedToContainer = 10f;
    [Tooltip("Скорость вращения стака урожая, при сборе в контейнер")]
    [Min(1f)]
    [SerializeField] private float _rotateSpeedToContainer = 720f;
    [Tooltip("Смещение стака урожая по оси Y, при размещении в контейнере")]
    [Min(0.1f)]
    [SerializeField] private float _offsetYInContainer = 0.1f;

    public float MoveSpeedToContainer => _moveSpeedToContainer;
    public float RotateSpeedToContainer => _rotateSpeedToContainer;
    public float OffsetYInContainer => _offsetYInContainer;
}
