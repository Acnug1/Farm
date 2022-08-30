using UnityEngine;

[RequireComponent(typeof(Plant))]
[RequireComponent(typeof(Renderer))]

public class PlantRenderer : MonoBehaviour
{
    private Plant _plant;
    private Renderer _renderer;

    public Color StartColor { get; private set; }
    public Color TargetColor { get; private set; }

    private void Awake()
    {
        _plant = GetComponent<Plant>();
        _renderer = GetComponent<Renderer>();

        StartColor = _plant.PlantConfig.StartColor;
        TargetColor = _plant.PlantConfig.TargetColor;
    }

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }
}
