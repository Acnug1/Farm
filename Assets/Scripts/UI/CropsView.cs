using TMPro;
using UnityEngine;

public class CropsView : MonoBehaviour
{
    [SerializeField] private TMP_Text _cropsCounter;
    [SerializeField] private TMP_Text _cropsMax;

    private Player _player;

    private void Start()
    {
        _player = PlayerInstance.Instance.GetComponent<Player>();

        _cropsCounter.text = _player.CropsCount.ToString();
        _cropsMax.text = _player.MaxCropsCount.ToString();

        _player.CropsCountChanged += OnCropsCountChanged;
    }

    private void OnDestroy()
    {
        _player.CropsCountChanged -= OnCropsCountChanged;
    }

    private void OnCropsCountChanged(int cropsCount)
    {
        _cropsCounter.text = _player.CropsCount.ToString();
    }
}
