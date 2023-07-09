using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {
    public static event Action OnDashBegin;
    public static event Action OnDashEnd;
    public static event Action OnPlayerStartMoving;
    
    static bool s_startMovingHasFired;

    [SerializeField] float movementSpeed;
    [SerializeField] [Range(0f, 1f)] float dashDropoffMultiplier;
    [SerializeField] Vector2 dashDeadzone;
    [SerializeField] float dashSpeed;

    Rigidbody2D _rb;
    Vector2 _moveVectorRaw;
    Vector2 _dashVector;
    Vector2 _inputVector;
    bool _inputEnabled = true;
    bool _dashing;

    void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }
    void Start() {
        SceneManager.activeSceneChanged += ResetEvents;
        GameManager.Instance.OnPlayerDeath += () => _inputEnabled = false;
    }
    void Update() {
        _dashVector *= dashDropoffMultiplier;
        if (_inputEnabled) {
            _inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        } else {
            _inputVector = Vector2.zero;
        }
        if (_inputVector != Vector2.zero) {
            if (!s_startMovingHasFired) {
                OnPlayerStartMoving?.Invoke();
                s_startMovingHasFired = true;
            }
        }
        _moveVectorRaw = _inputVector * movementSpeed;
        if (Input.GetKeyDown(KeyCode.Space)) {
            Dash();
        }
        float absoluteDashVectorX = Mathf.Abs(_dashVector.x);
        float absoluteDashVectorY = Mathf.Abs(_dashVector.y);
        if (absoluteDashVectorX < dashDeadzone.x && absoluteDashVectorY < dashDeadzone.y) {
            if (!_dashing) return;
            OnDashEnd?.Invoke();
            _dashing = false;
            _inputEnabled = true;
        }
    }
    void FixedUpdate() {
        _rb.MovePosition(_rb.position + _moveVectorRaw + _dashVector);
    }
    void OnDestroy() {
        OnDashBegin = null;
        OnDashEnd = null;
        OnPlayerStartMoving = null;
        s_startMovingHasFired = false;
        SceneManager.activeSceneChanged -= ResetEvents;
    }
    void Dash() {
        if (_dashing) return;
        Vector2 directionToDashIn = _inputVector.normalized * dashSpeed;
        OnDashBegin?.Invoke();
        _dashing = true;
        _inputEnabled = false;
        _dashVector = directionToDashIn;
    }
    void ResetEvents(Scene a, Scene b) {
        OnDashBegin = null;
        OnDashEnd = null;
        OnPlayerStartMoving = null;
        s_startMovingHasFired = false;
    }
}