using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissile : MonoBehaviour {
    [SerializeField] float moveSpeed;
    [SerializeField] float turnSpeed;
    [SerializeField] GameObject explosionFx;

    Rigidbody2D _rb;
    Transform _player;

    void Start () {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update () {
        Vector2 dirToPlayer = _player.position - transform.position;
        float angle = Mathf.Atan2(dirToPlayer.y, dirToPlayer.x) * Mathf.Rad2Deg - 90f;
        _rb.rotation = Mathf.LerpAngle(_rb.rotation, angle, turnSpeed * Time.deltaTime);
        _rb.velocity = transform.up * moveSpeed;
    }
    void OnTriggerEnter2D(Collider2D other) {
        PlayerHealth otherPlayerHealth = other.GetComponent<PlayerHealth>();
        if (otherPlayerHealth != null) {
            otherPlayerHealth.Damage(GameManager.Instance.HomingMissileDamage);
        }
        Instantiate(explosionFx, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}