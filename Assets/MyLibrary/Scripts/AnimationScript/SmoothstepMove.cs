using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OranUnityUtils 
{
   public class SmoothstepMove : AnimationScript {

        public SmoothstepMove(GameObject objToMove, Vector3 targetPosition, float duration) {
            Init(objToMove, SmoothstepMove_Coro(objToMove, targetPosition, duration));
        }

        private IEnumerator SmoothstepMove_Coro(GameObject objToMove, Vector3 targetPosition, float duration) {
            yield return objToMove.SmoothstepMove(targetPosition, duration);
            yield return null;
        }
    }
}