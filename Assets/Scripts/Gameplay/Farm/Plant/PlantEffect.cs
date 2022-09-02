using UnityEngine;

[RequireComponent(typeof(Plant))]

public class PlantEffect : MonoBehaviour
{
    private Plant _plant;
    private ParticleSystem _cutEffect;
    private Vector3 _offsetOfSpawnEffect;

    private void Awake()
    {
        _plant = GetComponent<Plant>();

        _cutEffect = _plant.PlantConfig.CutEffect;
        _offsetOfSpawnEffect = _plant.PlantConfig.OffsetOfSpawnEffect;
    }

    private void OnEnable()
    {
        _plant.PlantDestroy += OnPlantDestroy;
    }

    private void OnDisable()
    {
        _plant.PlantDestroy -= OnPlantDestroy;
    }

    private void OnPlantDestroy()
    {
        PlayCutEffect(_cutEffect, _offsetOfSpawnEffect);
    }

    private void PlayCutEffect(ParticleSystem cutEffect, Vector3 offsetOfSpawnEffect)
    {
        if (cutEffect)
            Instantiate(cutEffect, transform.position + offsetOfSpawnEffect, Quaternion.identity);
    }
}
