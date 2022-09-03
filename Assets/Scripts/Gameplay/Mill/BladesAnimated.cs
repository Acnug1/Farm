using UnityEngine;

[RequireComponent(typeof(Mill))]

public class BladesAnimated : MonoBehaviour
{
    private const string BladesErrorMessage = "Blades is null";

    [Tooltip("—сылка на Transform лопастей мельницы")]
    [SerializeField] private Transform _blades;

    private Mill _mill;
    private float _speedRotationBlades;

    private void Awake()
    {
        Debug.Assert(_blades != null, BladesErrorMessage);

        _mill = GetComponent<Mill>();

        _speedRotationBlades = _mill.MillConfig.SpeedRotationBlades;
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
