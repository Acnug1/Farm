using UnityEngine;

public class VictoryMenu : Menu
{
    private LevelLoader _levelLoader;

    protected override void Start()
    {
        base.Start();

        _levelLoader = LevelLoaderInstance.Instance.GetComponent<LevelLoader>();

        if (!_levelLoader)
            throw new MissingComponentException();

        _levelLoader.VictoryMenuOpening += OnMenuOpening;
    }

    private void OnDestroy()
    {
        _levelLoader.VictoryMenuOpening -= OnMenuOpening;
    }
}
