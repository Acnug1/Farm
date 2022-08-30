using UnityEngine;

[RequireComponent(typeof(Plant))]

public class PlantAnimated : MonoBehaviour
{
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
        _plant.HarvestReady += OnHarvestReady;
        DisableAnimation();
    }

    private void OnHarvestReady()
    {
        _plant.HarvestReady -= OnHarvestReady;
        SetDefaultScaleY();
        EnableAnimation();
    }

    private void SetDefaultScaleY()
    {
        _defaultScaleY = transform.localScale.y;
    }

    private void EnableAnimation()
    {
        if (!enabled)
            enabled = true;
    }

    private void DisableAnimation()
    {
        if (enabled)
            enabled = false;
    }

    private void Update()
    {
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
