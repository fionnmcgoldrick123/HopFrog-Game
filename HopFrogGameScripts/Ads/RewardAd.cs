using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections;
using Unity.VisualScripting;
using System.Runtime.InteropServices;
using UnityEngine.SocialPlatforms.Impl;
using System;

public class RewardAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] Button _showAdButton;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    string _adUnitId = null;

    [SerializeField] private BannerAds _bannerAd;

    [SerializeField] private Animator _deathPanelAnimation;

    [SerializeField] private DeathUIManager _deathUIManager;

    void Awake()
    {
        // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif

        _showAdButton.interactable = false;
        _showAdButton.gameObject.SetActive(false);

        LoadAd();

    }

    public void LoadAd()
    {
        if (string.IsNullOrEmpty(_adUnitId))
        {
            Debug.LogError("Ad Unit ID is not set.");
            return;
        }

        Advertisement.Load(_adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
    }

    public void ShowRewardAdButton()
    {

  
            _showAdButton.onClick.AddListener(ShowAd);
            _showAdButton.gameObject.SetActive(true);
            _showAdButton.interactable = true;
            PlayerPrefs.SetInt("LimitAds", 1);
            PlayerPrefs.Save();


    }

    public void StartCoroutineForReviveButton()
    {
        StartCoroutine(WaitForDeathPanel());
    }

    IEnumerator WaitForDeathPanel()
    {

        AnimatorStateInfo _deathPanel = _deathPanelAnimation.GetCurrentAnimatorStateInfo(0);

        yield return new WaitUntil(() => _deathPanelAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime > 0);
        yield return new WaitForSeconds(_deathPanelAnimation.GetCurrentAnimatorStateInfo(0).length);


        ShowRewardAdButton();

    }

    private void ShowAd()
    {
        _showAdButton.gameObject.SetActive(false);
        _showAdButton.interactable = false;

        _bannerAd.HideBannerAd();

        Advertisement.Show(_adUnitId, this);

    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            GiftReviveReward();
        }
    }
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");

        Invoke("LoadAd", 5f);
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        LoadAd();
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }


    void GiftReviveReward()
    {
        _deathUIManager._reviveButtonActivated = false;
        ScoreManager.Instance.SaveScoreAndRestart();
        
    }

    void OnDestroy()
    {
        _showAdButton.onClick.RemoveAllListeners();
    }


}