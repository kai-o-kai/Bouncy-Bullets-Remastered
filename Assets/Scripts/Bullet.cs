using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {
    [SerializeField] float bulletSpeed;
    [SerializeField] float stayTime;

    Rigidbody2D _rb;

    void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }
    void Start() {
        _rb.AddForce(transform.up * bulletSpeed);
        Destroy(gameObject, stayTime);
    }
    void OnCollisionEnter2D(Collision2D other) {
        if (!other.gameObject.CompareTag("Player")) return;
        PlayerHealth pHealth = other.gameObject.GetComponent<PlayerHealth>();
        pHealth.Damage(GameManager.Instance.BulletDamage);

        Destroy(gameObject);
    }
}