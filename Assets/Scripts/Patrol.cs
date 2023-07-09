using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour {
    [SerializeField] Transform[] points;
    [SerializeField] float moveSpeed;

    int _currentPointIndex;

    void Update() {
        if (points == null) return;
        if (points.Length == 0) return;
        transform.position = Vector2.MoveTowards(transform.position, points[_currentPointIndex].position, moveSpeed);
        if (Vector2.Distance(transform.position, points[_currentPointIndex].position) < 0.2f) {
            if (_currentPointIndex == points.Length - 1) {
                _currentPointIndex = 0;
            } else {
                _currentPointIndex++;
            }
        }
    }
}