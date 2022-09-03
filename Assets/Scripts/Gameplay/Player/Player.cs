using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const string PlayerConfigErrorMessage = "PlayerConfig is null";
    private const string CropContainerErrorMessage = "CropContainer is null";

    [Tooltip("—сылка на ScriptableObject: PlayerConfig")]
    [SerializeField] private PlayerConfig _playerConfig;
    [Tooltip("—сылка на контейнер дл€ сбора урожа€")]
    [SerializeField] private Transform _cropContainer;

    private readonly Stack<Crop> _crops = new Stack<Crop>();
    private int _maxCropCount;
    private float _sellCropDelay;
    private float _waitingTime;

    private void Awake()
    {
        Debug.Assert(_playerConfig != null, PlayerConfigErrorMessage);
        Debug.Assert(_cropContainer != null, CropContainerErrorMessage);

        _maxCropCount = _playerConfig.MaxCropCount;
        _sellCropDelay = _playerConfig.SellCropDelay;
    }

    public void TryAdd(Crop crop)
    {
        if (_crops.Count < _maxCropCount)
        {
            _crops.Push(crop);
            crop.Reap(_cropContainer, _crops.Count);
        }
    }

    public void SellCrops(Transform containerForSale)
    {
        while (_crops.Count > 0)
        {
            Crop crop = _crops.Pop();

            _waitingTime += _sellCropDelay;

            crop.Sell(containerForSale, _waitingTime);
        }

        _waitingTime = 0;
    }
}
