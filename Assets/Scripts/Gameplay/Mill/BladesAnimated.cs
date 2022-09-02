using UnityEngine;

public class BladesAnimated : MonoBehaviour
{
    private const string MillConfigErrorMessage = "MillConfig is null";
    private const string BladesErrorMessage = "Blades is null";

    [Tooltip("������ �� ScriptableObject: MillConfig")]
    [SerializeField] private MillConfig _millConfig;
    [Tooltip("������ �� Transform �������� ��������")]
    [SerializeField] private Transform _blades;

    private float _speedRotationBlades;

    private void Awake()
    {
        Debug.Assert(_millConfig != null, MillConfigErrorMessage);
        Debug.Assert(_blades != null, BladesErrorMessage);

        _speedRotationBlades = _millConfig.SpeedRotationBlades;
    }

    private void Update()
    {
        RotateBlades(_blades, _speedRotationBlades);
    }

    private void RotateBlades(Transform blades, float speed)
    {
        blades.Rotate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }
}
