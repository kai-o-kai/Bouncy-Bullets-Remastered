using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public static PlayerHealth Instance { get; private set; }

    public bool Invincible { get => _isInvincible; set => _isInvincible = value; }

    public event Action OnPlayerHurt;
    public event Action OnPlayerGainHealth;

    public int CurrentHealth { get => _currentHealth; }

    [SerializeField] int startHealth;
    [SerializeField] int maxHealth;

    bool _isInvincible;
    int _currentHealth;

    void Awake() {
        Instance = this;    
        _currentHealth = startHealth;
    }

    void Start() {
    }

    public void Damage(int dmgToDeal) {
        _currentHealth -= dmgToDeal;
        OnPlayerHurt?.Invoke();
        if (_currentHealth <= 0f) {
            GameManager.Instance.PlayerDeath();
        }
    }
    public void IncreaseHealth() {
        if (_currentHealth >= maxHealth) return;
        _currentHealth++;
        OnPlayerGainHealth?.Invoke();
    }
}