using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OranUnityUtils 
{
    public class FadeInAnim : AnimationScript {

        public FadeInAnim(GameObject objToFade, float duration) {
            FaderUI fader = objToFade.AddOrGetComponent<FaderUI>();
            Init(objToFade, fader.FadeIn_Coro(duration));
        }
    }
}