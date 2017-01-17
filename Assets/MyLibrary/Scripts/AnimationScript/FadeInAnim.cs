using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OranUnityUtils;

public class FadeInAnim : AnimationScript {

    public FadeInAnim(GameObject objToFade, float duration) {
        FadeInUIElement fader = objToFade.AddOrGetComponent<FadeInUIElement>();
        Init(objToFade, fader.FadeIn_Coro(duration));
    }
}
