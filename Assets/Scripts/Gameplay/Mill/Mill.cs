using UnityEngine;

public class Mill : MonoBehaviour
{
    private const string MillConfigErrorMessage = "MillConfig is null";
    private const string ContainerForSaleErrorMessage = "MillContainer is null";

    [Tooltip("������ �� ScriptableObject: MillConfig")]
    [SerializeField] private MillConfig _millConfig;
    [Tooltip("������ �� ��������� ��������, � ������� �������� ��������� ������")]
    [SerializeField] private Transform _containerForSale;

    public MillConfig MillConfig => _millConfig;
    public Transform ContainerForSale => _containerForSale;

    private void Awake()
    {
        Debug.Assert(_millConfig != null, MillConfigErrorMessage);
        Debug.Assert(_containerForSale != null, ContainerForSaleErrorMessage);
    }
}
