using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class EndBoundaryScript : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private float _fixedX;
    [SerializeField] private float _offsetY = 30;


    void Start()
    {
        if(_player == null) return;

        _fixedX = transform.position.x;
    }
    void Update()
    {

        if(_player == null) return;

        UnityEngine.Vector3 _playerPos = _player.position;
        
        transform.position = new UnityEngine.Vector3(_fixedX, _playerPos.y - _offsetY, 30);
    }

}
