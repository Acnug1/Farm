using UnityEngine;

[RequireComponent(typeof(Plant))]

public class PlantAnimated : MonoBehaviour
{
    [SerializeField] private AnimationCurve _plantScaleCurve;

    private Plant _plant;
    private float _currentTime;
    private float _totalTime;

    private void Awake()
    {
        _plant = GetComponent<Plant>();
        _totalTime = _plantScaleCurve.keys[_plantScaleCurve.keys.Length - 1].time;
        DisableAnimation();
    }

    private void OnEnable()
    {
        _plant.HarvestReady += OnHarvestReady;
    }

    private void OnDisable()
    {
        _plant.HarvestReady -= OnHarvestReady;
    }

    private void OnHarvestReady()
    {
        EnableAnimation();
    }

    private void EnableAnimation()
    {
        enabled = true;
    }

    private void DisableAnimation()
    {
        enabled = false;
    }

    private void Update()
    {
        Vector3 scale = transform.localScale;

        scale.y = _plantScaleCurve.Evaluate(_currentTime);

        transform.localScale = scale;

        _currentTime += Time.deltaTime;

        if (_currentTime >= _totalTime)
            _currentTime = 0;
    }
}
