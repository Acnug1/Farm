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
    private Plant _plant;
    private PlantRenderer _plantRenderer;
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

        Debug.Assert(_plantPrefab != null, PlantPrefabErrorMessage);
    }

    public void OnPlantDestroy()
    {
        _plant.PlantDestroy -= OnPlantDestroy;

        if (_grow != null)
            StopCoroutine(_grow);

        Destroy(_plant.gameObject);
    }

    public void Reap()
    { 
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

        _plantRenderer = TryTakePlantRenderer(_plant);

        _grow = StartCoroutine(Grow(_plant, _growthTime, _plantRenderer));
    }

    private PlantRenderer TryTakePlantRenderer(Plant plant) =>
        plant.TryGetComponent(out PlantRenderer plantRenderer) ? plantRenderer : null;

    private IEnumerator Grow(Plant plant, float growthTime, PlantRenderer plantRenderer)
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

            if (plantRenderer)
            {
                currentColor = Color.Lerp(plantRenderer.StartColor, plantRenderer.TargetColor, normalizedRunningTime);
                plantRenderer.SetColor(currentColor);
            }

            yield return waitForEndOfFrame;
        }

        plant.FinishGrowth();
    }
}
