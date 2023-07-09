using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class BurstTurret : Turret {
    [Header("Burst Settings")]
    [SerializeField] int shotsPerBurst;
    [SerializeField] [Tooltip("Time between shots during a burst shot in seconds.")] float delayBetweenBurstShotsSeconds;
    [SerializeField] [Tooltip("Run every single shot in the burst, instead of every burst.")] UnityEvent OnTurretProjectileShot;

    WaitForSeconds burstShotDelayCachedWFS;

    protected override void Awake() {
        base.Awake();
        burstShotDelayCachedWFS = new WaitForSeconds(delayBetweenBurstShotsSeconds);
    }
    protected override void FireTurret() {
        StartCoroutine(C_TurretBurst());
    }
    IEnumerator C_TurretBurst() {
        int shotsFired = 0;
        OnTurretFire?.Invoke();
        while (shotsFired < shotsPerBurst) {
            FireProjectileOnce();
            shotsFired++;
            OnTurretProjectileShot?.Invoke();
            yield return burstShotDelayCachedWFS;
        }
    }
}
