using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OranUnityUtils;

public class FadeOutAnim : AnimationScript {

	public FadeOutAnim(GameObject objToFade, float duration) {
        FadeInUIElement fader = objToFade.AddOrGetComponent<FadeInUIElement>();
        Init(objToFade, fader.FadeOut_Coro(duration));
    }
    
}
