using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndlessLevel : MonoBehaviour {
    [SerializeField] TMP_Text coinsText;
    [SerializeField] Animator gameOverAnim;

    public int CoinsCollected { get => _coinsCollected;  set { _coinsCollected = value; RecalculateUI(); } }

    int _coinsCollected;

    void Start() {
        GameManager.Instance.OnPlayerDeath += ShowGameOver;
    }

    void RecalculateUI() {
        coinsText.text = _coinsCollected.ToString();
    }
    void ShowGameOver() {
        gameOverAnim.Play("in");
    }
}