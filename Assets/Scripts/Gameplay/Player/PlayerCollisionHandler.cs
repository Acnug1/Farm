using UnityEngine;

[RequireComponent(typeof(Player))]

public class PlayerCollisionHandler : MonoBehaviour
{
    private const string CropContainerErrorMessage = "CropContainer is null";

    [Tooltip("—сылка на контейнер дл€ сбора урожа€")]
    [SerializeField] private Transform _cropContainer;

    private Player _player;

    private void Awake()
    {
        Debug.Assert(_cropContainer != null, CropContainerErrorMessage);

        _player = GetComponent<Player>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Crop crop) 
            && _player.CropCount < _player.MaxCropCount)
        {
            _player.IncreaseCropCount();
            crop.Reap(_cropContainer, _player.CropCount);
        }
    }
}
