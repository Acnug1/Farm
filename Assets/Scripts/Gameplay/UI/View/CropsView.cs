using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CropsView : MonoBehaviour
{
    private const string CropsCounterErrorMessage = "CropsCounter is null";
    private const string CropsMaxErrorMessage = "CropsMax is null";

    [Tooltip("—сылка на счетчик стаков урожа€")]
    [SerializeField] private TMP_Text _cropsCounter;
    [Tooltip("—сылка на счетчик, в котором указано максимальное количество стаков урожа€, которое может подобрать игрок")]
    [SerializeField] private TMP_Text _cropsMax;
    [SerializeField] private UnityEvent _onCropsCountChanged;

    private Player _player;

    private void Awake()
    {
        Debug.Assert(_cropsCounter != null, CropsCounterErrorMessage);
        Debug.Assert(_cropsMax != null, CropsMaxErrorMessage);
    }

    private void Start()
    {
        _player = PlayerInstance.Instance.GetComponent<Player>();

        if (!_player)
            throw new MissingComponentException();

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
        _cropsCounter.text = cropsCount.ToString();
        _onCropsCountChanged?.Invoke();
    }
}
