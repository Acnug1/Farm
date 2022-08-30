using UnityEngine;

[CreateAssetMenu(fileName = "New PlantConfig", menuName = "Plant/Create new PlantConfig", order = 51)]

public class PlantConfig : ScriptableObject
{
    [Tooltip("Анимационная кривая изменения значения 'scale.y' у растения, после завершения роста")]
    [SerializeField] private AnimationCurve _plantScaleYCurve;
    [Tooltip("Нужно ли зациклить анимацию? (Если установить false, то анимация воспроизведется один раз)")]
    [SerializeField] private bool _isLoopAnimation = true;
    [Tooltip("Стартовый цвет растения при начале роста")]
    [SerializeField] private Color _startColor = Color.yellow;
    [Tooltip("Целевой цвет, который должно достичь растение при полном росте")]
    [SerializeField] private Color _targetColor = Color.green;
    [Tooltip("Эффект среза растения")]
    [SerializeField] private ParticleSystem _cutEffect;
    [Tooltip("Смещение спавна эффекта, относительно пивота растения в глобальной системе координат")]
    [SerializeField] private Vector3 _offsetOfSpawnEffect = new Vector3(0, 1, 0);

    public AnimationCurve PlantScaleYCurve => _plantScaleYCurve;
    public bool IsLoopAnimation => _isLoopAnimation;
    public Color StartColor => _startColor;
    public Color TargetColor => _targetColor;
    public ParticleSystem CutEffect => _cutEffect;
    public Vector3 OffsetOfSpawnEffect => _offsetOfSpawnEffect;
}
