using UnityEngine;

public class Plant : MonoBehaviour
{
    public void SetScaleY(float currentScaleY)
    {
        transform.localScale = new Vector3(transform.localScale.x, currentScaleY, transform.localScale.z);
    }
}
