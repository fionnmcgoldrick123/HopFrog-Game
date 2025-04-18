using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonScript : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;

    [SerializeField] private Image _pauseBackGroundImage;

    private bool _pauseButtonPressed = false;

    void Awake()
    {
        DeathManager.OnDeath += HideButton;
    }

    void Start()
    {
        _pauseButton.gameObject.SetActive(true);
        _pauseButton.onClick.AddListener(PauseScene);

        _pauseBackGroundImage.gameObject.SetActive(false);
    }

    void PauseScene(){

        if(_pauseButtonPressed == false){
            Time.timeScale = 0f;
            _pauseButtonPressed = true;
            _pauseBackGroundImage.gameObject.SetActive(true);
        }
        else{
            Time.timeScale = 1f;
            _pauseButtonPressed = false;
            _pauseBackGroundImage.gameObject.SetActive(false);
        }
        
    }

    void HideButton()
{
    if (_pauseButton != null) // First, check if the button exists
    {
        _pauseButton.gameObject.SetActive(false);
    }
}



}
