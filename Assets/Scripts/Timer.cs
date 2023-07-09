using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer {
    static List<Timer> s_activeTimers = new List<Timer>();

    public float TimeLeft { get => _durationLeft; }
    public bool IsDone => _durationLeft == 0f;
    public bool IsEnabled { get { return _isEnabled; } set {
        _isEnabled = value;
        if (value == false) {
            s_activeTimers.Remove(this);
        }
    } }
    public Action OnTimerComplete { get => _onTimerComplete; }

    Action _onTimerComplete;
    float _durationLeft;
    MonoBehaviour _referenceObject;
    bool _isEnabled = true;

    public Timer(float duration, Action onTimerComplete, MonoBehaviour referenceObject) {
        _durationLeft = duration;
        _onTimerComplete = onTimerComplete;
        _referenceObject = referenceObject;
        _referenceObject.StartCoroutine(ProcessTimer());
        s_activeTimers.Add(this);
    }
    IEnumerator ProcessTimer() {
        while (_durationLeft > 0f) {
            if (!_isEnabled) yield return null;
            _durationLeft -= Time.deltaTime;
            yield return null;
        }
        if (!_isEnabled) yield return null;
        _durationLeft = 0f;
        _onTimerComplete.Invoke();
    }

    public static void DisableAllTimers() {
        foreach (Timer toDisable in s_activeTimers) {
            toDisable.IsEnabled = false;
        }
    }
    public static void DisableTimersWithCallback(Action check) {
        Timer[] matchingTimers = Array.FindAll<Timer>(s_activeTimers.ToArray(), c => c.OnTimerComplete == check);
        foreach (Timer toDisable in matchingTimers) {
            toDisable.IsEnabled = false;
        }
    }
}