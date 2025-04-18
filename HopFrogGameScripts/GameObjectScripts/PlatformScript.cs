using NUnit.Framework;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlatformScript : MonoBehaviour
{

    private PlatformSpawner _platformSpawner;
    private PlayerScript _playerScript;
    private bool _hasBeenUsed = false;

    void Start()
    {
        _platformSpawner = FindFirstObjectByType<PlatformSpawner>();
        _playerScript = FindFirstObjectByType<PlayerScript>();
    } 

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player") && !_hasBeenUsed)
        {

            if(DeathManager.Instance.GetIsDead()) return;
            
            ScoreManager.Instance.AddScore();
            AudioManager.Instance.PlayScoreSound();
            _hasBeenUsed = true;
            
            _playerScript.FlipCharacterDirection();
        }
        else{
            _playerScript._jumpDirection *= -1; 
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            DeathManager.Instance.HandleDeath();
        }

        if (collision.gameObject.CompareTag("EndBoundary"))
        {
            Destroy(gameObject);
            _platformSpawner.SpawnNextPlatform();
        }
    }

}



