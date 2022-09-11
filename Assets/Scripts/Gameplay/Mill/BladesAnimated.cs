using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Mill))]

public class BladesAnimated : MonoBehaviour
{
    private const string BladesErrorMessage = "Blades is null";
    private const int FullRotation = 360;

    [Tooltip("—сылка на Transform лопастей мельницы")]
    [SerializeField] private Transform _blades;

    private Mill _mill;
    private float _timeToFullRotation;

    private void Awake()
    {
        Debug.Assert(_blades != null, BladesErrorMessage);

        _mill = GetComponent<Mill>();

        _timeToFullRotation = _mill.MillConfig.TimeToFullRotation;

        RotateBlades(_blades, _timeToFullRotation);
    }

    private void RotateBlades(Transform blades, float timeToFullRotation)
    {
        Tween tween = blades.DOLocalRotate(Vector3.forward * FullRotation, timeToFullRotation, RotateMode.FastBeyond360);
        tween.SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }
}
