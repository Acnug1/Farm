using UnityEngine;

public class Player : MonoBehaviour
{
    private const string PlayerConfigErrorMessage = "PlayerConfig is null";

    [Tooltip("—сылка на ScriptableObject: PlayerConfig")]
    [SerializeField] private PlayerConfig _playerConfig;

    public int CropCount { get; private set; }
    public int MaxCropCount { get; private set; }

    private void Awake()
    {
        Debug.Assert(_playerConfig != null, PlayerConfigErrorMessage);

        MaxCropCount = _playerConfig.MaxCropCount;
    }

    public void IncreaseCropCount()
    {
        CropCount++;
    }
}
