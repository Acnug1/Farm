using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]

public class ControlTutorial : MonoBehaviour
{
    private const string ControlTutorialConfigErrorMessage = "ControlTutorialConfig is null";

    [Tooltip("—сылка на ScriptableObject: ControlTutorialConfig")]
    [SerializeField] private ControlTutorialConfig _controlTutorialConfig;

    private CanvasGroup _canvasGroup;
    private float _timeOfFade;

    private void Awake()
    {
        Debug.Assert(_controlTutorialConfig != null, ControlTutorialConfigErrorMessage);

        _canvasGroup = GetComponent<CanvasGroup>();

        _canvasGroup.alpha = 1f;
        _timeOfFade = _controlTutorialConfig.TimeOfFade;

        StartCoroutine(WaitForControlStart());
    }

    private IEnumerator WaitForControlStart()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        StartCoroutine(FadeControlTutorialPanel(_timeOfFade));
    }

    private IEnumerator FadeControlTutorialPanel(float timeOfFade)
    {
        var waitForEndOfFrame = new WaitForEndOfFrame();

        for (float remainingTime = timeOfFade; remainingTime > 0; remainingTime -= Time.deltaTime)
        {
            remainingTime = Mathf.Clamp(remainingTime, 0, timeOfFade);

            _canvasGroup.alpha = GetNormalizeValue(remainingTime, timeOfFade);

            yield return waitForEndOfFrame;
        }
    }

    private float GetNormalizeValue(float remainingTime, float totalTime)
    {
        if (remainingTime < 0 || totalTime <= 0)
            throw new InvalidOperationException();

        return remainingTime / totalTime;
    }
}
