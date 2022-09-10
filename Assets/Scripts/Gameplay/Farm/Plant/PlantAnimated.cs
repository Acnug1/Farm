using UnityEngine;

[RequireComponent(typeof(Plant))]

public class PlantAnimated : MonoBehaviour
{
    private bool _isEnabledAnimation;
    private Plant _plant;
    private AnimationCurve _plantScaleYCurve;
    private bool _isLoop;
    private float _defaultScaleY;
    private float _currentTime;
    private float _totalTime;

    private void Awake()
    {
        _plant = GetComponent<Plant>();

        _plantScaleYCurve = _plant.PlantConfig.PlantScaleYCurve;
        _isLoop = _plant.PlantConfig.IsLoopAnimation;

        _totalTime = _plantScaleYCurve.keys[_plantScaleYCurve.keys.Length - 1].time;
    }

    private void OnEnable()
    {
        _plant.HarvestReady += OnHarvestReady;
        _currentTime = 0;
        DisableAnimation();
    }

    private void OnDisable()
    {
        _plant.HarvestReady -= OnHarvestReady;
    }

    private void OnHarvestReady()
    {
        SetDefaultScaleY();
        EnableAnimation();
    }

    private void SetDefaultScaleY()
    {
        _defaultScaleY = transform.localScale.y;
    }

    private void EnableAnimation()
    {
        if (!_isEnabledAnimation)
            _isEnabledAnimation = true;
    }

    private void DisableAnimation()
    {
        if (_isEnabledAnimation)
            _isEnabledAnimation = false;
    }

    private void Update()
    {
        if (_isEnabledAnimation)
            AnimateScaleY(_plantScaleYCurve, _isLoop);
    }

    private void AnimateScaleY(AnimationCurve plantScaleYCurve, bool isLoop)
    {
        if (_currentTime > _totalTime)
        {
            if (isLoop)
                _currentTime = 0;
            else
                return;
        }

        Vector3 scale = transform.localScale;
        scale.y = _defaultScaleY * plantScaleYCurve.Evaluate(_currentTime);
        transform.localScale = scale;

        _currentTime += Time.deltaTime;
    }
}
