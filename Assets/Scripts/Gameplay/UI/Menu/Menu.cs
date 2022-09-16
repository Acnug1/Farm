using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]

public abstract class Menu : MonoBehaviour
{
    private const string MenuConfigErrorMessage = "MenuConfig is null";
    private const string MenuButtonErrorMessage = "MenuButton is null";

    [Tooltip("—сылка на ScriptableObject: MenuConfig")]
    [SerializeField] private MenuConfig _menuConfig;
    [Tooltip("—сылка на кнопку, котора€ используетс€ дл€ действий в меню")]
    [SerializeField] private Button _menuButton;

    private CanvasGroup _canvasGroup;
    private float _timeOfAppearance;
    private Coroutine _menuAppearance;

    protected virtual void Start()
    {
        Debug.Assert(_menuConfig != null, MenuConfigErrorMessage);
        Debug.Assert(_menuButton != null, MenuButtonErrorMessage);

        _canvasGroup = GetComponent<CanvasGroup>();

        _timeOfAppearance = _menuConfig.TimeOfAppearance;

        Closed();
    }

    protected virtual void OnMenuOpening()
    {
        Open();
    }

    private void Closed()
    {
        _canvasGroup.alpha = 0;

        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }

    private void Open()
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        _menuButton.onClick.AddListener(OnMenuButtonClick);

        if (_menuAppearance != null)
            StopCoroutine(_menuAppearance);

        _menuAppearance = StartCoroutine(MenuAppearance(_timeOfAppearance));
    }

    private IEnumerator MenuAppearance(float timeOfAppearance)
    {
        var waitForEndOfFrame = new WaitForEndOfFrame();

        for (float elapsedTime = 0; elapsedTime < timeOfAppearance; elapsedTime += Time.deltaTime)
        {
            elapsedTime = Mathf.Clamp(elapsedTime, 0, timeOfAppearance);

            float normalizeValue = GetNormalizeValue(elapsedTime, timeOfAppearance);
            _canvasGroup.alpha = normalizeValue;
            Time.timeScale = 1 - normalizeValue;

            yield return waitForEndOfFrame;
        }
    }

    private float GetNormalizeValue(float remainingTime, float totalTime)
    {
        if (remainingTime < 0 || totalTime <= 0)
            throw new InvalidOperationException();

        return remainingTime / totalTime;
    }

    private void OnMenuButtonClick()
    {
        _menuButton.onClick.RemoveListener(OnMenuButtonClick);

        LevelsList.Instance.LoadActiveLevel();
    }
}