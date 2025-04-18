using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class MainCameraScript : MonoBehaviour
{
    [SerializeField] private Transform _playerTarget;
    [SerializeField] private int _offsetY;

    [SerializeField] private float _smoothFactor;

    private float _highestPoint;

    private float _fixedX = 0;
    private float _fixedZ = -10;

    private Vector3 _velocity = Vector3.zero;

    void Start()
    {
        _fixedX = transform.position.x;
        _fixedZ = transform.position.z;

        _highestPoint = _playerTarget.position.y;
        
    }

    void Update()
    {

        if(_playerTarget == null) return;

        CheckForPlayersHighestPoint();
        Vector3 targetPosition = new Vector3(_fixedX, _highestPoint + _offsetY, _fixedZ);

         transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothFactor);

    } 

    void CheckForPlayersHighestPoint(){
        if(_playerTarget.position.y > _highestPoint){
            _highestPoint = _highestPoint = _playerTarget.position.y;
        }
    }

}
