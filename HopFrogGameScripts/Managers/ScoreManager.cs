using TMPro;
using UnityEngine;
using System;
using UnityEngine.SocialPlatforms.Impl;
using Unity.VisualScripting;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager Instance;

    private TextMeshProUGUI _scoreText;
    public int _score = 0;

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

    void Start()
    {
        _score = GetScore();
        PlayerPrefs.SetInt("LimitAds", 0);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            ResetHighScore();
        }
    }

    public void AddScore()
    {
        _score += 1;
        _scoreText.text = $"{_score}";
    }

    public void ResetScore()
    {
             _score = 0;
    }

    public void SetScoreText(TextMeshProUGUI scoreText, int _currentScore)
    {
        _scoreText = scoreText;
        _score = _currentScore;
        _scoreText.text = $"{_score}";
    }

    public void SetHighScore()
    {
        if (_score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", _score);
            PlayerPrefs.Save();
        }

    }

    void ResetHighScore(){
        PlayerPrefs.DeleteAll();
    }

    public int GetScore(){
        return _score;
    }

    public void SetScore(int savedScoreFromRevive){
        _score = savedScoreFromRevive;
    }

    public int GetHighScore(){
        return PlayerPrefs.GetInt("HighScore", 0);
    }

    public void SaveScoreAndRestart(){
        _score = GetScore();
        _scoreText.text = $"{_score}";
        PlayerPrefs.SetInt("LimitAds", 1);
        SceneHandler.Instance.RestartSceneWithRevive();
    }



}
