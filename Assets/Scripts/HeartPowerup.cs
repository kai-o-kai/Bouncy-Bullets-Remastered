using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPowerup : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;
        PlayerHealth pHealth = other.GetComponent<PlayerHealth>();
        pHealth.IncreaseHealth();
        AudioManager.Instance.PlaySoundByName("Powerup");
        Destroy(gameObject);
    }
}