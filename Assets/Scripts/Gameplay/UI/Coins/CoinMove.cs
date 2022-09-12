using System.Collections;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Coin))]

public class CoinMove : MonoBehaviour
{
    private Coin _coin;
    private Coroutine _moveToDestinationPoint;
    private float _timeToDestinationPoint;

    private void Awake()
    {
        _coin = GetComponent<Coin>();

        _timeToDestinationPoint = _coin.CoinConfig.TimeToDestinationPoint;
    }

    private void OnEnable()
    {
        _coin.CoinSpawned += OnCoinSpawned;
    }

    private void OnDisable()
    {
        _coin.CoinSpawned -= OnCoinSpawned;
    }

    private void OnCoinSpawned(Transform destinationPoint)
    {
        if (_moveToDestinationPoint != null)
            StopCoroutine(_moveToDestinationPoint);

        _moveToDestinationPoint = StartCoroutine(MoveToDestinationPoint(destinationPoint));
    }

    private IEnumerator MoveToDestinationPoint(Transform destinationPoint)
    {
        transform.DOMove(destinationPoint.position, _timeToDestinationPoint);

        while (Vector3.Distance(transform.position, destinationPoint.position) != 0)
            yield return null;

        _coin.Take();
        _coin.Disable();
    }
}
