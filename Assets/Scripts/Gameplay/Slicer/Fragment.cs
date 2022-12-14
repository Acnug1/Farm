using System;
using System.Collections;
using UnityEngine;

public class Fragment : MonoBehaviour
{
    private float _lifeTimeFragment;
    private float _timeOfDisappearance;
    private Coroutine _dissapear;

    public void Init(SliceableObjectConfig sliceableObjectConfig)
    {
        _lifeTimeFragment = sliceableObjectConfig.LifeTimeFragment;
        _timeOfDisappearance = sliceableObjectConfig.TimeOfDissapearance;
    }

    private void Start()
    {
        Invoke(nameof(StartDisappearing), _lifeTimeFragment);
    }

    private void StartDisappearing()
    {
        if (_dissapear != null)
            StopCoroutine(_dissapear);

        _dissapear = StartCoroutine(Dissapear(_timeOfDisappearance));
    }

    private IEnumerator Dissapear(float timeOfDisappearance)
    {
        Vector3 fragmentScale = gameObject.transform.localScale;
        var waitForEndOfFrame = new WaitForEndOfFrame();

        for (float remainingTime = timeOfDisappearance; remainingTime > 0; remainingTime -= Time.deltaTime)
        {
            remainingTime = Mathf.Clamp(remainingTime, 0, timeOfDisappearance);

            gameObject.transform.localScale = fragmentScale * GetNormalizeValue(remainingTime, timeOfDisappearance);

            yield return waitForEndOfFrame;
        }

        Destroy(gameObject);
    }

    private float GetNormalizeValue(float remainingTime, float totalTime)
    {
        if (remainingTime < 0 || totalTime <= 0)
            throw new InvalidOperationException();

        return remainingTime / totalTime;
    }
}
