using System;
using UnityEngine;

[RequireComponent(typeof(Plant))]

public class PlantHarvest : MonoBehaviour
{
    private const string CropPrefabErrorMessage = "CropPrefab is null";
    private Plant _plant;
    private Crop _cropPrefab;
    private Vector3 _offsetOfSpawnCropPrefab;
    private Culture _culture;

    private void Awake()
    {
        _plant = GetComponent<Plant>();

        _cropPrefab = _plant.PlantConfig.CropPrefab;
        _offsetOfSpawnCropPrefab = _plant.PlantConfig.OffsetOfSpawnCropPrefab;
        _culture = GetComponentInParent<Culture>();

        Debug.Assert(_cropPrefab != null, CropPrefabErrorMessage);
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
        if (!_culture)
            throw new InvalidOperationException();

        CreateCrop(_cropPrefab, _offsetOfSpawnCropPrefab, _culture);
    }

    private void CreateCrop(Crop cropPrefab, Vector3 offsetOfSpawnCropPrefab, Culture culture)
    {
        Crop crop = Instantiate(cropPrefab, transform.position + offsetOfSpawnCropPrefab, Quaternion.identity);

        crop.Init(culture);
    }
}
