using UnityEngine;

[RequireComponent(typeof(Plant))]

public class PlantHarvest : MonoBehaviour
{
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
        if (_culture != null)
            CreateCrop(_cropPrefab, _offsetOfSpawnCropPrefab, _culture);
    }

    private void CreateCrop(Crop cropPrefab, Vector3 offsetOfSpawnCropPrefab, Culture culture)
    {
        if (cropPrefab)
        {
            Crop crop = Instantiate(cropPrefab, transform.position + offsetOfSpawnCropPrefab, Quaternion.identity);

            crop.Init(culture);
        }
    }
}
