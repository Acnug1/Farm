using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Crop crop))
        {
            crop.Harvest();
            crop.Destroy();
        }
    }
}
