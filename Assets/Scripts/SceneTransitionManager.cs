using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class SceneTransitionManager : MonoBehaviour {
    public static SceneTransitionManager Instance { get; private set; }
    
    [SerializeField] float wipeTime;
    WaitForSeconds _wipeTimeDelay;

    Animator _anim;

    void Awake() {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        _anim = GetComponent<Animator>();
        _wipeTimeDelay = new WaitForSeconds(wipeTime);
    }
    public void NextScene() {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        Scene newScene = SceneManager.GetSceneByBuildIndex(nextSceneIndex);
        if (newScene == null) return;
        StartCoroutine(WipeIntoScene(nextSceneIndex));
    }
    public void ReloadCurrentScene() {
        StartCoroutine(WipeIntoScene(SceneManager.GetActiveScene().buildIndex));
    }
    public void LoadSpecificScene(int toLoad) {
        StartCoroutine(WipeIntoScene(toLoad));
    }
    IEnumerator WipeIntoScene(int newScene) {
        _anim.Play("wipeOut");
        yield return _wipeTimeDelay;
        SceneManager.LoadScene(newScene);
        _anim.Play("wipeIn");
    }
}