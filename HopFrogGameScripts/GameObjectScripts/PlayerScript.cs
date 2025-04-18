using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerScript : MonoBehaviour
{

    #region VARIABLES

    private Rigidbody2D _rigidBody;
    [SerializeField] private float _customGravityScale = 3f;
    [SerializeField] private float _floatFactor = 0.5f;
    [SerializeField] private float _fallFactor = 1f;
    [SerializeField] private float _maxJumpSpeed = 5f;
    [SerializeField] private float _minJumpSpeed = 2f;
    [SerializeField] private float _jumpPowerScale = 0.5f;
    [SerializeField] private float _jumpHeightScale = 0.5f;
    [SerializeField] private float _maxJumpHeight = 10f;
    [SerializeField] private float _minJumpHeigh = 1f;
    private float _jumpPower = 0f;
    private float _jumpHeight = 0f;
    private bool _canJump = false;
    private bool _isCharging = false;

    public int _jumpCount = 0;

    private int _startFloorTouchCounter;

    private Vector3 _startPos = new Vector3(-1.6f, -3.5f, 0);

    [SerializeField] public int _jumpDirection = 1;

    private Vector2 _localScale;

    [SerializeField] private Animator _animator;

    #endregion

    #region METHODS

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.position = _startPos;

    }

    void Start()
    {
        _startFloorTouchCounter = 0;
        _jumpCount = 0;
    }

    void Update()
    {
        HandleJump();
        CheckIfAirborn();


    }

    void CheckIfAirborn()
    {
        if (!_canJump)
        {

            ApplyConstantGravity();

        }
        else { return; }
    }

    void ApplyConstantGravity()
    {
        if (_rigidBody.linearVelocityY > 0.1f)
        {
            _rigidBody.linearVelocity += Vector2.down * (_customGravityScale * _floatFactor) * Time.deltaTime;
        }
        else
        {
            _rigidBody.linearVelocity += Vector2.down * (_customGravityScale * _fallFactor) * Time.deltaTime;
        }
    }


    void HandleJump()
    {

        if (Input.GetMouseButtonDown(0) && _canJump)
        {



            bool isOverUIGameObject = CheckForInvalidTap();

            if(isOverUIGameObject) return;

            _isCharging = true;
            StartCoroutine(ChargeJump());
        }

        if (Input.GetMouseButtonUp(0) && _isCharging)
        {

            _isCharging = false;
            PerformJump();
        }
    }

    bool CheckForInvalidTap(){

        if(EventSystem.current.IsPointerOverGameObject()) return true;

        if(Input.touchCount > 0){

            Touch touch = Input.GetTouch(0);
            if(EventSystem.current.IsPointerOverGameObject(touch.fingerId)) return true;
        }

        return false;
    }

    IEnumerator ChargeJump()
    {
        while (_isCharging)
        {
            _jumpPower += _jumpPowerScale * Time.deltaTime;
            _jumpHeight += _jumpHeightScale * Time.deltaTime;

            _jumpPower = Mathf.Clamp(_jumpPower, _minJumpSpeed, _maxJumpSpeed);
            _jumpHeight = Mathf.Clamp(_jumpHeight, _minJumpHeigh, _maxJumpHeight);

            _animator.SetBool("IsCharging", true);

            CheckIfMaxJump();

            yield return null;
        }
    }

    void CheckIfMaxJump()
    {
        if (_jumpHeight >= _maxJumpHeight && _jumpPower >= _maxJumpSpeed)
        {
            _isCharging = false;
            PerformJump();
        }
    }
    void PerformJump()
    {
        _jumpCount++;
        _canJump = false;

        _animator.SetBool("IsCharging", false);
        _animator.SetBool("IsJumping", true);


        _rigidBody.linearVelocity = new Vector2(_jumpDirection * _jumpPower, _jumpHeight);

        SwitchDirection();

        StartCoroutine(StartTimerForIdleAnimation());
    }

    IEnumerator StartTimerForIdleAnimation()
    {
        yield return new WaitForSeconds(2f);

        _animator.SetBool("IsIdle", true);

    }

    void SwitchDirection()
    {
        _jumpDirection *= -1;



    }

    public void FlipCharacterDirection()
    {
        _localScale = transform.localScale;
        _localScale.x *= -1;
        transform.localScale = _localScale;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GroundFloor") || collision.gameObject.CompareTag("Platform"))
        {
            _canJump = true;
            _rigidBody.linearVelocity = Vector2.zero;
            _jumpHeight = 0f;
            _jumpPower = 0f;
        }

        if (collision.gameObject.CompareTag("GroundFloor"))
        {
            _startFloorTouchCounter += 1;

            if (_startFloorTouchCounter >= 2)
            {
                DeathManager.Instance.HandleDeath();
            }
        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            _animator.SetBool("IsJumping", false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            DeathManager.Instance.HandleDeath();
        }
    }


    #endregion


}

