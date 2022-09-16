using UnityEngine;

public class DefeatMenu : Menu
{
    private LevelLoader _levelLoader;

    protected override void Start()
    {
        base.Start();

        _levelLoader = LevelLoaderInstance.Instance.GetComponent<LevelLoader>();

        if (!_levelLoader)
            throw new MissingComponentException();

        _levelLoader.DefeatMenuOpening += OnMenuOpening;
    }

    private void OnDestroy()
    {
        _levelLoader.DefeatMenuOpening -= OnMenuOpening;
    }
}
