using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawning : MonoBehaviour {
    [SerializeField] float minTimeBetweenPowerups;
    [SerializeField] float maxTimeBetweenPowerups;
    [SerializeField] GameObject powerup;
    [SerializeField] Collider2D bounds;

    bool _powerupSpawningEnabled = true;

    void Start() {
        GameManager.Instance.OnLevelWin += () => _powerupSpawningEnabled = false;
        GameManager.Instance.OnPlayerDeath += () => _powerupSpawningEnabled = false;
        Invoke(nameof(SpawnPowerup), Random.Range(maxTimeBetweenPowerups, minTimeBetweenPowerups));
    }
    void SpawnPowerup() {
        if (!_powerupSpawningEnabled) { return; }
        Instantiate(powerup, GetRandomPosInBounds(), Quaternion.identity);
        Invoke(nameof(SpawnPowerup), Random.Range(maxTimeBetweenPowerups, minTimeBetweenPowerups));
    }
    Vector2 GetRandomPosInBounds() {
        float x = Random.Range(bounds.bounds.min.x, bounds.bounds.max.x);
        float y = Random.Range(bounds.bounds.max.y, bounds.bounds.max.y);
        return new Vector2(x, y);
    }
}