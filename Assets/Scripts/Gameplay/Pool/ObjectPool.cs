using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private readonly List<GameObject> _pool = new List<GameObject>();

    protected void InitializePool(GameObject prefab, Transform container = null, int capacity = 1)
    {
        if (!prefab || capacity < 1)
            throw new InvalidOperationException();

        for (int i = 0; i < capacity; i++)
        {
            GameObject spawned = Instantiate(prefab, container);
            spawned.SetActive(false);

            _pool.Add(spawned);
        }
    }

    protected bool TryGetObjectFromPool(out GameObject result)
    {
        result = _pool.FirstOrDefault(p => p.activeSelf == false);

        return result != null;
    }
}
