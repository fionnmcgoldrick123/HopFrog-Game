using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    [SerializeField] private DeathUIManager _deathUIInstance;

    [SerializeField] private PlayerScript _playerInstance;

    [SerializeField] private GameObject _platformPrefabInstance;

    [SerializeField] private AudioSource _deathSound;

    [SerializeField] private AudioSource _scoreSound;

    private int _currentScore;

    void Awake()
    {

        InitializeSingletons();

    }

    void InitializeSingletons()
    {
        InitializeScoreManager();
        InitializeAudioManager();
        InitializeDeathManager();
    }

    void InitializeScoreManager()
    {
        _currentScore = ScoreManager.Instance.GetScore();

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.SetScoreText(_scoreText, _currentScore);
        }
    }

    void InitializeDeathManager()
    {
        if (DeathManager.Instance != null)
        {
            DeathManager.Instance.SetDeathUserInerfaceInstance(_deathUIInstance);
            DeathManager.Instance.SetPlayerInstance(_playerInstance);
            DeathManager.Instance.SetDeathAudioSource(_deathSound);
        }
    }

    void InitializeAudioManager()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetScoreSound(_scoreSound);
        }
    }
}
