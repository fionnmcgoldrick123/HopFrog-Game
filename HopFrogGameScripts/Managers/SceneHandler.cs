using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{

    public static SceneHandler Instance;

    public static event Action OnResetScene;

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

    public void RestartScene(){
        DeathManager.Instance.SetIsDead(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        OnResetScene?.Invoke();
        PlayerPrefs.SetInt("LimitAds", 0);
    }

    public void RestartSceneWithRevive(){
        DeathManager.Instance.SetIsDead(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        OnResetScene?.Invoke();
    }

    public void CloseGame(){
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

}
