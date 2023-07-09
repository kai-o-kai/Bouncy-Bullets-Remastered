using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerLevel : MonoBehaviour {
    [SerializeField] float levelDuration;
    [SerializeField] TMP_Text timerText;

    Timer levelTimer;

    void Start() {
        GameManager.Instance.OnLevelWin += () => Timer.DisableTimersWithCallback(WinLevel);
        PlayerMovement.OnPlayerStartMoving += StartTimer;
        timerText.text = Mathf.RoundToInt(levelDuration).ToString();
    }
    void StartTimer() {
        levelTimer = new Timer(levelDuration, WinLevel, this);
    }
    void Update() {
        if (levelTimer != null) {
            timerText.text = Mathf.RoundToInt(levelTimer.TimeLeft).ToString();    
        }
    }
    void WinLevel() {
        GameManager.Instance.LevelWin();
    }
}