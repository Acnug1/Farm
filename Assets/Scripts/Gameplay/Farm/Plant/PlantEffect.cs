using UnityEngine;

[RequireComponent(typeof(Plant))]

public class PlantEffect : ObjectPool
{
    private const string CutEffectErrorMessage = "CutEffect is null";
    private Plant _plant;
    private ParticleSystem _cutEffect;
    private Vector3 _offsetOfSpawnEffect;

    private void Awake()
    {
        _plant = GetComponent<Plant>();

        _cutEffect = _plant.PlantConfig.CutEffect;
        _offsetOfSpawnEffect = _plant.PlantConfig.OffsetOfSpawnEffect;

        Debug.Assert(_cutEffect != null, CutEffectErrorMessage);

        InitializePool(_cutEffect.gameObject, transform.parent);
    }

    private void OnEnable()
    {
        _plant.PlantDisable += OnPlantDisable;
    }

    private void OnDisable()
    {
        _plant.PlantDisable -= OnPlantDisable;
    }

    private void OnPlantDisable()
    {
        PlayCutEffect(_offsetOfSpawnEffect);
    }

    private void PlayCutEffect(Vector3 offsetOfSpawnEffect)
    {
        if (TryGetObjectFromPool(out GameObject cutEffectObject))
        {
            cutEffectObject.transform.position = transform.position + offsetOfSpawnEffect;
            cutEffectObject.SetActive(true);
        }
    }
}
