using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class FlashEffectScript : MonoBehaviour
{

    private CanvasGroup _canvasGroup;
    [SerializeField] private float _flashInDuration = 0.1f;
    [SerializeField] private float _flashOutDuration = 0.1f;
    private float _elapsedTime = 0f;

    void Awake()
    {
        DeathManager.OnDeath += FlashEffect;
    }

    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        if(_canvasGroup == null) return;

        _canvasGroup.alpha = 0f;
        _canvasGroup.gameObject.SetActive(false);
    }

    void FlashEffect()
    {
        _canvasGroup.gameObject.SetActive(true);
        StartCoroutine(StartFlashEffect());
    }

    IEnumerator StartFlashEffect()
    {
        _elapsedTime = 0f;

        while (_elapsedTime < _flashInDuration)
        {
            _canvasGroup.alpha = Mathf.Lerp(0f, 1f, _elapsedTime / _flashInDuration);
            _elapsedTime += Time.deltaTime;
            yield return null;
        }

         _elapsedTime = 0f;

         while(_elapsedTime < _flashOutDuration){
            _canvasGroup.alpha = Mathf.Lerp(1f, 0f, _elapsedTime / _flashInDuration);
            _elapsedTime += Time.deltaTime;
            yield return null;
         }

         _canvasGroup.gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        DeathManager.OnDeath -= FlashEffect;
    }



}
