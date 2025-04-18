using System;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.XR;

public class DeathManager : MonoBehaviour
{
    public static DeathManager Instance;

    private DeathUIManager _deathUIManager;

    private PlayerScript _player;

    public static event Action OnDeath;

    [SerializeField] private AudioSource _deathSound;

    private bool _isDead = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void HandleDeath()
    {

        SetIsDead(true);
        OnDeath?.Invoke();

        _deathSound.Play();

        Destroy(_player.gameObject);
        //_player.gameObject.SetActive(false);

        HandleHighScore();
        CallDeathUI();
    }

    public void SetIsDead(bool deadState){
        _isDead = deadState;
    }

    public bool GetIsDead(){
        return _isDead;
    }

    void CallDeathUI()
    {
        _deathUIManager.DisplayDeathUI();
    }

    void HandleHighScore()
    {
        ScoreManager.Instance.SetHighScore();
    }

    public void CallRestartGame()
    {
        ScoreManager.Instance.ResetScore();
        SceneHandler.Instance.RestartScene();
    }

    public void SetDeathUserInerfaceInstance(DeathUIManager _deathUIInstance)
    {
        _deathUIManager = _deathUIInstance;
    }

    public void SetPlayerInstance(PlayerScript _currentPlayerInstance)
    {
        _player = _currentPlayerInstance;
    }

    public void SetDeathAudioSource(AudioSource _deathAudioInstance){
        _deathSound = _deathAudioInstance;
    }
}
