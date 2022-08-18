using System.Collections.Generic;
using UnityEngine;

public class Garden : MonoBehaviour
{
    [SerializeField] private List<Culture> _cultures;

    private void Update()
    {
        CheckCulturesState(_cultures);
    }

    private void CheckCulturesState(List<Culture> cultures)
    {
        foreach (Culture culture in cultures)
        {
            if (!culture.IsExists)
                culture.StartGrowth();
        }
    }
}
