using UnityEngine;
using UnityEngine.UI;

public class QuitButtonScript : MonoBehaviour
{

    Button _quitButton;

    void Awake()
    {
        _quitButton = GetComponent<Button>();
    }

    void Start()
    {


        _quitButton.onClick.AddListener(QuitGame);
    }

    void QuitGame()
    {
        SceneHandler.Instance.CloseGame();
    }


}
