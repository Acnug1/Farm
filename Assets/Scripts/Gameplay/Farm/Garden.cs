using UnityEngine;

public class Garden : MonoBehaviour
{
    private Culture[] _cultures;

    private void Awake()
    {
        _cultures = GetComponentsInChildren<Culture>();
    }

    private void Update()
    {
        CheckCulturesState(_cultures);
    }

    private void CheckCulturesState(Culture[] cultures)
    {
        foreach (Culture culture in cultures)
        {
            if (!culture.IsExists)
                culture.StartGrowth();
        }
    }
}
