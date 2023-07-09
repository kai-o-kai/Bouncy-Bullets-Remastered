using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour {
    [SerializeField] GameObject[] heartImages;

    void Start() {
        PlayerHealth.Instance.OnPlayerGainHealth += RecalculateHealthbar;
        PlayerHealth.Instance.OnPlayerHurt += RecalculateHealthbar;
        RecalculateHealthbar();
    }
    void RecalculateHealthbar() {
        int playerNewHP = PlayerHealth.Instance.CurrentHealth;
        for (int i = 0; i < heartImages.Length; i++) {
            if (i < playerNewHP) {
                heartImages[i].SetActive(true);
            } else {
                heartImages[i].SetActive(false);
            }
        }
    }
}