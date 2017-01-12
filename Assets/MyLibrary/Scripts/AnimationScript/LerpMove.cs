using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using OranUnityUtils;

public class LerpMove : AnimationScript {


    public LerpMove(GameObject goToMove, Vector3 targetPosition, float duration) : base() {
        Init(goToMove, LerpCoro(goToMove, targetPosition, duration));
    }

    private IEnumerator LerpCoro(GameObject coroHost, Vector3 targetPosition, float duration) {
        yield return coroHost.LerpMove(targetPosition, duration);
        yield return null;
    }

}

