using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OranUnityUtils 
{
    public class LerpRotationLocal : AnimationScript {

        public LerpRotationLocal(GameObject objToRotate, Quaternion targetLocalRotation, float duration) : base() {
            Init(objToRotate, LerpRotationLocal_Coro(objToRotate, targetLocalRotation, duration));
        }

        private IEnumerator LerpRotationLocal_Coro(GameObject objToRotate, Quaternion targetLocalRotation, float duration) {
            yield return objToRotate.LerpRotateLocal(targetLocalRotation, duration);
            yield return null;
        }
    }
}