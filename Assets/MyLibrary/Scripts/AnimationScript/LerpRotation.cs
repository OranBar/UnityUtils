using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OranUnityUtils 
{
    public class LerpRotation : AnimationScript {

        public LerpRotation(GameObject objToRotate, Quaternion targetRotation, float duration) : base() {
            Init(objToRotate, LerpRotation_Coro(objToRotate, targetRotation, duration));
        }

        private IEnumerator LerpRotation_Coro(GameObject objToRotate, Quaternion targetRotation, float duration) {
            yield return objToRotate.LerpRotate(targetRotation, duration);
            yield return null;
        }
    }
}