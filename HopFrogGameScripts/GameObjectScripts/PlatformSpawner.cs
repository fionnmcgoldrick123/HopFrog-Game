using System;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{

    public static PlatformSpawner Instance;

    [SerializeField] private GameObject _platformPrefab;
    private Vector3 _firstPlatform = new Vector3(1.5f, -2.8f, 0f);

    private Vector2 _nextPlatformPosition;

    [SerializeField] private float _maxX = 5f;
    [SerializeField] private float _minX = 1f;
    [SerializeField] private float _maxY = 5f;
    [SerializeField] private float _minY = 1f;

    private int _directionForNextPlatform = -1;

    [SerializeField] private int _amountOfPlatformsAllowed = 30;

    void Start()
    {

        _nextPlatformPosition = _firstPlatform;
        Instantiate(_platformPrefab, _firstPlatform, Quaternion.identity);
        HandleAmountOfPlatforms();

    }

    void HandleAmountOfPlatforms()
    {
        for (int i = 0; i < _amountOfPlatformsAllowed; i++)
        {
            SpawnNextPlatform();
        }
    }

    public void SpawnNextPlatform()
    {

        float nextPlatformX = GetRandomX();
        float nextPlatformY = GetRandomY();

        _nextPlatformPosition = new Vector2(nextPlatformX, nextPlatformY);

        Instantiate(_platformPrefab, _nextPlatformPosition, Quaternion.identity);

        SwitchDirection();
    }

    float GetRandomX()
    {
        float randomX = UnityEngine.Random.Range(_minX * _directionForNextPlatform, _maxX * _directionForNextPlatform);
        return randomX;
    }

    float GetRandomY()
    {
        float randomX = UnityEngine.Random.Range(_minY + _nextPlatformPosition.y, _maxY + _nextPlatformPosition.y);
        return randomX;
    }

    void SwitchDirection()
    {
        _directionForNextPlatform *= -1;
    }


}
