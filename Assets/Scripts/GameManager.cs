using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public int BulletDamage { get => bulletDmg; }
    public int HomingMissileDamage { get => homingMissileDmg; }

    public event Action OnPlayerDeath;
    public event Action OnLevelWin;

    [SerializeField] int bulletDmg;
    [SerializeField] int homingMissileDmg;

    void Awake() {
        Instance = this;
    }
    public void PlayerDeath() => OnPlayerDeath?.Invoke();
    public void LevelWin() => OnLevelWin?.Invoke();
}