using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{

    [SerializeField] private float _setPitch;

    void Start()
    {   
        Button currentButton = GetComponent<Button>();
        if(currentButton != null){

            currentButton.onClick.AddListener(() => AudioManager.Instance.PlayClickSound(_setPitch));

        }
    }

}
