using System.Collections;
using UnityEngine;

public class Culture : MonoBehaviour
{
    private const string PlantContainerErrorMessage = "PlantContainer is null";
    private const string CultureConfigErrorMessage = "CultureConfig is null";
    private const string PlantPrefabErrorMessage = "PlantPrefab is null";

    [Tooltip("Контейнер, в котором будет посажено растение")]
    [SerializeField] private Transform _plantContainer;
    [Tooltip("Ссылка на ScriptableObject: CultureConfig")]
    [SerializeField] private CultureConfig _cultureConfig;

    private float _timeBetweenSpawn;
    private float _targetScaleY;
    private float _growthTime;
    private Plant _plantPrefab;
    private Color _targetColor;
    private Plant _plant;
    private Coroutine _grow;

    public bool IsExists { get; private set; } = false;

    private void Awake()
    {
        Debug.Assert(_plantContainer != null, PlantContainerErrorMessage);
        Debug.Assert(_cultureConfig != null, CultureConfigErrorMessage);

        _timeBetweenSpawn = _cultureConfig.TimeBetweenSpawn;
        _targetScaleY = _cultureConfig.TargetScaleY;
        _growthTime = _cultureConfig.GrowthTime;
        _plantPrefab = _cultureConfig.PlantPrefab;
        _targetColor = _cultureConfig.TargetColor;

        Debug.Assert(_plantPrefab != null, PlantPrefabErrorMessage);
    }

    public void OnPlantDestroy()
    {
        _plant.PlantDestroy -= OnPlantDestroy;

        if (_grow != null)
            StopCoroutine(_grow);

        Destroy(_plant.gameObject);
        IsExists = false;
    }

    public void Sow()
    {
        if (!IsExists)
            IsExists = true;
        else
            new System.InvalidOperationException();

        Invoke(nameof(StartGrowth), _timeBetweenSpawn);
    }

    private void StartGrowth()
    {
        _plant = Instantiate(_plantPrefab, _plantContainer);
        _plant.PlantDestroy += OnPlantDestroy;
        _grow = StartCoroutine(Grow(_plant, _growthTime));
    }

    private IEnumerator Grow(Plant plant, float growthTime)
    {
        var waitForEndOfFrame = new WaitForEndOfFrame();
        float currentScaleY;
        Color currentColor;
        float runningTime = 0f;
        float normalizedRunningTime;

        while (runningTime < growthTime)
        {
            runningTime += Time.deltaTime;
            normalizedRunningTime = runningTime / growthTime;

            currentScaleY = Mathf.Lerp(0f, _targetScaleY, normalizedRunningTime);
            plant.SetScaleY(currentScaleY);
            currentColor = Color.Lerp(plant.StartColor, _targetColor, normalizedRunningTime);
            plant.SetColor(currentColor);

            yield return waitForEndOfFrame;
        }

        plant.FinishGrowth();
    }
}
