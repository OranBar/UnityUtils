using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OranUnityUtils 
{
    public class FadeOutAnim : AnimationScript {

        public FadeOutAnim(GameObject objToFade, float duration) {
            FaderUI fader = objToFade.AddOrGetComponent<FaderUI>();
            Init(objToFade, fader.FadeOut_Coro(duration));
        }

    }
}