using System.Collections;
using System;
using UnityEngine;


namespace OranUnityUtils 
{ 
    public class LerpMoveLocal : AnimationScript {

        public LerpMoveLocal(GameObject goToMove, Vector3 targetLocalPosition, float duration) : base() {
            Init(goToMove, LerpMoveLocal_Coro(goToMove, targetLocalPosition, duration));
        }

        /**<summary>This version delays the evaluation </summary>
            */
        public LerpMoveLocal(GameObject goToMove, Func<Vector3> targetLocalPositionFunc, float duration) : base() {
            Init(goToMove, LerpMoveLocal_Coro(goToMove, targetLocalPositionFunc, duration));
        }

        private IEnumerator LerpMoveLocal_Coro(GameObject coroHost, Vector3 targetLocalPosition, float duration) {
            yield return coroHost.LerpMoveLocal(targetLocalPosition, duration);
            yield return null;
        }

        private IEnumerator LerpMoveLocal_Coro(GameObject coroHost, Func<Vector3> targetLocalPositionFunc, float duration) {
            yield return coroHost.LerpMoveLocal(targetLocalPositionFunc(), duration);
            yield return null;
        }

    }
}