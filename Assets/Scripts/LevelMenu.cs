using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenu : MonoBehaviour {
    [SerializeField] Animator gameOverAnim;
    [SerializeField] Animator gameWonAnim;

    void Start() {
        GameManager.Instance.OnPlayerDeath += ShowGameOver;
        GameManager.Instance.OnLevelWin += ShowLevelWon;
    }
    public void RetryLevel() {
        SceneTransitionManager.Instance.ReloadCurrentScene();
    }
    public void BackToMenu() {
        SceneTransitionManager.Instance.LoadSpecificScene(0);
    }
    void ShowGameOver() {
        gameOverAnim.Play("in");
    }
    void ShowLevelWon() {
        gameWonAnim.Play("in");
    }
    public void ProceedLevel() {
        SceneTransitionManager.Instance.NextScene();
    }
}