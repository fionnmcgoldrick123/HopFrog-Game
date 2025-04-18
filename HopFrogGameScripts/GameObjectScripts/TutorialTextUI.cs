using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTextUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _tutorialTextCanvasGroup;

    [SerializeField] private PlayerScript _player;

    [SerializeField] private float _fadeDuration;

    private bool _lock = false;

    void Start()
    {
        _tutorialTextCanvasGroup.alpha = 1f;
    }

    void Update()
    {
        if (_player._jumpCount == 1 && !_lock) StartCoroutine(StartFadeAway());
    }

    IEnumerator StartFadeAway()
    {
        _lock = true;
        float elapsedTime = 0;

        while (elapsedTime < _fadeDuration)
        {
            _tutorialTextCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / _fadeDuration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        _tutorialTextCanvasGroup.alpha = 0f;
    }
}
