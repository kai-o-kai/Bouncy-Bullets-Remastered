using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Turret : MonoBehaviour {
    protected bool _turretIsEnabled;
    protected Transform _player;
    protected Transform _firePoint;

    [SerializeField] [Tooltip("How often (in seconds) this turret should fire. ")] protected float fireRateSeconds;
    [SerializeField] [Tooltip("Prefab to spawn in every fire cycle.")] protected GameObject projectile;
    [SerializeField] protected UnityEvent OnTurretFire;
    [Header("Turn To Player")]
    [SerializeField] protected bool pointAtPlayer;
    [SerializeField] protected float turnSpeed;

    protected virtual void Awake() {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _firePoint = transform.Find("FirePoint");
    }

    protected virtual void Start() {
        if (projectile == null) {
            Debug.LogWarning("Turret has no projectile to fire! Disabling Turret.", gameObject);
            return;
        }
        PlayerMovement.OnPlayerStartMoving += EnableTurret;
        GameManager.Instance.OnPlayerDeath += DisableTurret;
    }
    protected virtual void Update() {
        if (!_turretIsEnabled) return;
        if (!pointAtPlayer) return;
        if (!pointAtPlayer) return;
        Vector2 dirToPlayer = (_player.position - transform.position);
        RaycastHit2D rayToPlayer = Physics2D.Raycast(transform.position, dirToPlayer);
        Debug.DrawRay(transform.position, dirToPlayer);
        if (!rayToPlayer.collider) return;
        if (!rayToPlayer.collider.gameObject.CompareTag("Player")) return;
        PointToPlayer(dirToPlayer);
    }
    protected void PointToPlayer(Vector2 dirToPlayer) {
        float angle = Mathf.Atan2(dirToPlayer.y, dirToPlayer.x) * Mathf.Rad2Deg - 90f;
        float lerpedAngle = Mathf.LerpAngle(transform.eulerAngles.z, angle, Time.deltaTime * turnSpeed);
        transform.rotation = Quaternion.Euler(0f, 0f, lerpedAngle);
    }
    protected virtual void FireTurret() {
        FireProjectileOnce();
        OnTurretFire?.Invoke();
    }

    protected void FireProjectileOnce() {
        if (!_turretIsEnabled) return;
        GameObject launchedProjectile = Instantiate(projectile, _firePoint.position, _firePoint.rotation);
    }
    void OnDestroy() {
        PlayerMovement.OnPlayerStartMoving -= EnableTurret;
        GameManager.Instance.OnPlayerDeath -= DisableTurret;
    }
    protected virtual void EnableTurret() {
        _turretIsEnabled = true;
        InvokeRepeating(nameof(FireTurret), 2f, fireRateSeconds);
    }
    protected virtual void DisableTurret() {
        _turretIsEnabled = false;
        CancelInvoke(nameof(FireTurret));
    }
}