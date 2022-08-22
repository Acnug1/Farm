using UnityEngine;
using UnityEngine.Events;

public class Plant : MonoBehaviour
{
    public event UnityAction PlantDestroy;

    public void Destroy()
    {
        PlantDestroy?.Invoke();
    }

    public void SetScaleY(float currentScaleY)
    {
        transform.localScale = new Vector3(transform.localScale.x, currentScaleY, transform.localScale.z);
    }
}
