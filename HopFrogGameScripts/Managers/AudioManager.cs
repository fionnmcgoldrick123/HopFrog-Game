using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;

    [SerializeField] private float _minPitch;
    [SerializeField] private float _maxPitch;
    
    private AudioSource _scoreSound;

    [SerializeField] private AudioSource _clickSound;

    private float _finalPitch;

    void Awake()
    {
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

    public float GetRandomPitch(){
        
        _finalPitch = Random.Range(_minPitch, _maxPitch);

        return _finalPitch;
    }

    public void PlayScoreSound(){
        _scoreSound.PlayOneShot(_scoreSound.clip);
    }

    public void SetScoreSound(AudioSource scoreSoundInstance){
        _scoreSound = scoreSoundInstance;
    }

    public void SetClickSound(AudioSource clickSoundInstance){
        _clickSound = clickSoundInstance;
    }

    public void PlayClickSound(float pitch){
        _clickSound.pitch = pitch;
        _clickSound.PlayOneShot(_clickSound.clip);
    }

}
