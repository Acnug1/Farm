using System.Collections;
using UnityEngine;

public class Culture : MonoBehaviour
{
    private const string PlantErrorMessage = "Plant is null";
    private const string PlantContainerErrorMessage = "PlantContainer is null";
    private const string CultureConfigErrorMessage = "CultureConfig is null";

    [Tooltip("Ссылка на префаб растения, которое должно расти")]
    [SerializeField] private Plant _plantPrefab;
    [Tooltip("Контейнер, в котором будет посажено растение")]
    [SerializeField] private Transform _plantContainer;
    [Tooltip("Ссылка на ScriptableObject: CultureConfig")]
    [SerializeField] private CultureConfig _cultureConfig;

    private float _targetScaleY;
    private float _growthTime;
    private Plant _plant;
    private Coroutine _grow;

    public bool IsExists { get; private set; } = false;

    private void Awake()
    {
        Debug.Assert(_plantPrefab != null, PlantErrorMessage);
        Debug.Assert(_plantContainer != null, PlantContainerErrorMessage);
        Debug.Assert(_cultureConfig != null, CultureConfigErrorMessage);

        _targetScaleY = _cultureConfig.TargetScaleY;
        _growthTime = _cultureConfig.GrowthTime;
    }

    public void OnPlantDestroy()
    {
        _plant.PlantDestroy -= OnPlantDestroy;

        if (_grow != null)
            StopCoroutine(_grow);

        Destroy(_plant.gameObject);
        IsExists = false;
    }

    public void StartGrowth()
    {
        if (!IsExists)
            IsExists = true;
        else
            new System.InvalidOperationException();

        _plant = Instantiate(_plantPrefab, _plantContainer);
        _plant.PlantDestroy += OnPlantDestroy;
        _grow = StartCoroutine(Grow(_plant, _targetScaleY, _growthTime));
    }

    private IEnumerator Grow(Plant plant, float targetScaleY, float growthTime)
    {
        var waitForEndOfFrame = new WaitForEndOfFrame();
        float currentScaleY = 0f;
        float runningTime = 0f;
        float normalizedRunningTime;

        while (currentScaleY < targetScaleY)
        {
            runningTime += Time.deltaTime;
            normalizedRunningTime = runningTime / growthTime;

            currentScaleY = Mathf.Lerp(0f, targetScaleY, normalizedRunningTime);
            plant.SetScaleY(currentScaleY);

            yield return waitForEndOfFrame;
        }
    }
}
