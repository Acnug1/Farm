using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    private bool _isActiveSelling;

    public event UnityAction<Crop> CropAdded;
    public event UnityAction<int> CropsCountChanged;

    public int CropsCount => _crops.Count;
    public int MaxCropsCount => _maxCropCount;

    private void Awake()
    {
        Debug.Assert(_playerConfig != null, PlayerConfigErrorMessage);
        Debug.Assert(_cropContainer != null, CropContainerErrorMessage);

        _maxCropCount = _playerConfig.MaxCropCount;
        _sellCropDelay = _playerConfig.SellCropDelay;
    }

    public void TryAdd(Crop crop)
    {
        if (_crops.Count < _maxCropCount && !_isActiveSelling)
        {
            _crops.Push(crop);
            crop.Reap(_cropContainer, _crops.Count);
            CropAdded?.Invoke(crop);
            CropsCountChanged?.Invoke(_crops.Count);
        }
    }

    public void TrySellCrops(Transform containerForSale)
    {
        if (_crops.Count == 0 || _isActiveSelling)
            return;

        _isActiveSelling = true;

        for (int i = 0; i < _crops.Count; i++)
        {
            _waitingTime += _sellCropDelay;

            StartCoroutine(WaitingBeforeSell(containerForSale, _waitingTime));
        }

        _waitingTime = 0;
    }

    private IEnumerator WaitingBeforeSell(Transform containerForSale, float waitingTime)
    {
        var waitForSeconds = new WaitForSeconds(waitingTime);

        yield return waitForSeconds;

        Crop crop = _crops.Pop();
        crop.Sell(containerForSale);
        CropsCountChanged?.Invoke(_crops.Count);

        if (_crops.Count == 0)
            _isActiveSelling = false;
    }
}
