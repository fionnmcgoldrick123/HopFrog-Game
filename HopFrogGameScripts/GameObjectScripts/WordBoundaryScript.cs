using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;

public class WordBoundaryScript : MonoBehaviour
{

    private float _screenEdgeLeft;
    private float _screenEdgeRight;

    private float _screenBottom;
    [SerializeField] private Transform _playerTransform;   

    private Vector3 _playerPos;

    [SerializeField] private float _bottomDeathMargin = 0;
    void Awake()
    {
        GetScreenEdges();
    }

    void Update()
    {

        if(_playerTransform == null) return;
        _playerPos = _playerTransform.position;
        CheckForSideCollisions();
        CheckForBottomCollision();
    } 

    void GetScreenEdges()
    {
        _screenEdgeLeft = Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)).x;
        _screenEdgeRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0,0)).x;
        _screenBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
    }

    void CheckForSideCollisions(){

        if(_playerTransform == null) return;
        if(_playerPos.x <= _screenEdgeLeft || _playerPos.x >= _screenEdgeRight || _playerPos.y <= _screenBottom + _bottomDeathMargin){
            DeathManager.Instance.HandleDeath();
        } 
    }

    void CheckForBottomCollision(){
        _screenBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
        if(_playerPos.y <= _screenBottom + _bottomDeathMargin ) DeathManager.Instance.HandleDeath();
    }

    
}
