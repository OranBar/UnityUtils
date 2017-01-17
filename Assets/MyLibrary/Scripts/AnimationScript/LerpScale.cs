using System.Collections;
using UnityEngine;
using OranUnityUtils;
using System;

public class LerpScale : AnimationScript { 

    public LerpScale(GameObject goToScale, Vector3 targetScale, float duration) : base() {
        Init(goToScale, LerpCoro(goToScale, targetScale, duration));
    }

    /**<summary>This version delays the evaluation </summary>
     */
    public LerpScale(GameObject goToScale, Func<Vector3> targetScaleFun, float duration) : base() {
        Init(goToScale, LerpCoro(goToScale, targetScaleFun, duration));
    }

    private IEnumerator LerpCoro(GameObject coroHost, Vector3 targetScale, float duration) {
        yield return coroHost.LerpLocalScale(targetScale, duration);
        yield return null;
    }

    private IEnumerator LerpCoro(GameObject coroHost, Func<Vector3> targetScaleFun, float duration) {
        yield return coroHost.LerpLocalScale(targetScaleFun(), duration);
        yield return null;
    }
}
