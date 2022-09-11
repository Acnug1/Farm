using UnityEngine;

[RequireComponent(typeof(Player))]

public class PlayerCollisionHandler : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Crop crop))
            _player.TryAdd(crop);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Mill mill))
            _player.TrySellCrops(mill.ContainerForSale);
    }
}
