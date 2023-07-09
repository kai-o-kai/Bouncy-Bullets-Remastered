using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;
        AudioManager.Instance.PlaySoundByName("Powerup");
        FindObjectOfType<EndlessLevel>().CoinsCollected += 1;
        Destroy(gameObject);
    }
}