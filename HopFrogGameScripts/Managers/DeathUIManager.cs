using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using System;
using System.Threading;

public class DeathUIManager : MonoBehaviour
{

    [SerializeField] private GameObject _deathPanel;
    [SerializeField] private Animator _deathPanelAnimation;
    [SerializeField] private Button _respawnButton;

    [SerializeField] private RectTransform _reviveButtonTrans;
    private CanvasGroup _respawnButtonCanvasGroup;

    [SerializeField] private RectTransform _retryButtonTransform;
    [SerializeField] private RewardAd _rewardAdScript;
    [SerializeField] private int _scoreRequiredForRevive = 3;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;
    [SerializeField] private Image _deathBackgroundImage;
    private int _score;
    private int _highScore;
    [SerializeField] private float _elapsedTime = 0f;
    [SerializeField] private float _fadeDuration = 5f;
    [SerializeField] private float _countScoreTime = 0.2f;
    [SerializeField] private float _countHighScore = 0.1f;

    public bool _reviveButtonActivated = false;
    [SerializeField] private AudioSource _countUpScoreAudio;
    [SerializeField] private TextMeshProUGUI _mainScoreText;
    private float _receivedPitch;

    [SerializeField] private float _bottomOffset;

    private float _bottomDeathPanel;

    [SerializeField] private RectTransform _deathPanelRectTrans;



    void Awake()
    {

        _respawnButtonCanvasGroup = _respawnButton.gameObject.GetComponent<CanvasGroup>();
        _respawnButton.onClick.AddListener(CallRespawn);

        DeathManager.OnDeath += GetScoreAfterDeath;
        //DeathManager.OnDeath += CheckIfScoreWillEnableRevive;
        DeathManager.OnDeath += HideMainScore;

    }

    void Start()
    {
        _deathPanel.SetActive(false);

        _respawnButton.gameObject.SetActive(false);
        _respawnButtonCanvasGroup.alpha = 0f;

        _deathBackgroundImage.gameObject.SetActive(false);

        _mainScoreText.gameObject.SetActive(true);
    }

    void GetScoreAfterDeath()
    {
        _score = ScoreManager.Instance.GetScore();
        _scoreText.text = $"{_score}";
    }


    public void DisplayDeathUI()
    {

        _deathBackgroundImage.gameObject.SetActive(true);

        HandleDeathPanel();

        _respawnButton.gameObject.SetActive(true);
    }

    void HandleDeathPanel()
    {
        _deathPanel.SetActive(true);
        _deathPanelAnimation.SetBool("PlayerDied", true);

        StartCoroutine(WaitForDeathPanelAnimation());
    }

    IEnumerator WaitForDeathPanelAnimation()
    {

        AnimatorStateInfo stateOfDeathPanelAnimation = _deathPanelAnimation.GetCurrentAnimatorStateInfo(0);

        yield return new WaitForSeconds(stateOfDeathPanelAnimation.length);

        yield return new WaitForEndOfFrame();

        OnDeathPanelAnimationComplete();
    }

    void OnDeathPanelAnimationComplete()
    {
        _bottomDeathPanel = GetBottomOfDeathPanel();

        HandleScore();

        CheckIfScoreWillEnableRevive();

        StartCoroutine(HandleRespawnButton());
    }

    float GetBottomOfDeathPanel()
    {
        float bottomOfDeathPanel =  _deathPanelRectTrans.anchoredPosition.y - (_deathPanelRectTrans.rect.height / 2);
        return bottomOfDeathPanel;
    }

    IEnumerator HandleRespawnButton()
    {
        float buttonY = _bottomDeathPanel - _bottomOffset;

        if (_reviveButtonActivated == false)
        {
            _retryButtonTransform.anchoredPosition = new Vector3(0, buttonY, 0);
        }
        else
        {
            _retryButtonTransform.anchoredPosition = new Vector3(_retryButtonTransform.anchoredPosition.x, buttonY, 0);
        }

        while (_elapsedTime < _fadeDuration)
        {
            _respawnButtonCanvasGroup.alpha += Mathf.Lerp(0f, 1f, _elapsedTime / _fadeDuration);
            _elapsedTime += Time.deltaTime;
            yield return null;
        }

        _respawnButtonCanvasGroup.alpha = 1f;
    }

    void CallRespawn()
    {

        DeathManager.Instance.CallRestartGame();
    }

    void HandleScore()
    {
        _score = ScoreManager.Instance.GetScore();
        _highScore = ScoreManager.Instance.GetHighScore();

        StartCoroutine(CountScore());
        StartCoroutine(CountHighScore());

    }

    IEnumerator CountScore()
    {
        for (int i = 0; i <= _score; i++)
        {
            _scoreText.text = $"{i}";

            if (_score <= 0) yield break;

            if (AudioManager.Instance != null) _receivedPitch = AudioManager.Instance.GetRandomPitch();
            _countUpScoreAudio.pitch = _receivedPitch;
            _countUpScoreAudio.PlayOneShot(_countUpScoreAudio.clip);
            yield return new WaitForSeconds(_countScoreTime);
        }
    }

    IEnumerator CountHighScore()
    {
        for (int i = 0; i <= _highScore; i++)
        {
            _highScoreText.text = $"{i}";
            yield return new WaitForSeconds(_countHighScore);
        }
    }

    void CheckIfScoreWillEnableRevive()
    {

        float buttonY = _bottomDeathPanel - _bottomOffset;

        if (PlayerPrefs.GetInt("LimitAds", 0) < 1)
        {

            _score = ScoreManager.Instance.GetScore();
            if (_score >= _scoreRequiredForRevive)
            {
                _reviveButtonTrans.anchoredPosition = new Vector3(_reviveButtonTrans.anchoredPosition.x, buttonY, 0);
                _reviveButtonActivated = true;
                _rewardAdScript.StartCoroutineForReviveButton();

            }

        }
        else return;
    }

    void HideMainScore()
    {
        _mainScoreText.gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        //DeathManager.OnDeath -= CheckIfScoreWillEnableRevive;
        DeathManager.OnDeath -= HideMainScore;
        DeathManager.OnDeath -= GetScoreAfterDeath;
    }



}
