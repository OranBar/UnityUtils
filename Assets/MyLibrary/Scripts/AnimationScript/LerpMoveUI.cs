using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace OranUnityUtils
{
    public class LerpMoveUI : AnimationScript {

        public LerpMoveUI(GameObject goToMove, Vector3 targetPosition, float duration) : base() {
            Init(goToMove, LerpCoro(goToMove, targetPosition, duration));
        }

        /**<summary>This version delays the evaluation </summary>
         */
        public LerpMoveUI(GameObject goToMove, Func<Vector3> targetPosition, float duration) : base() {
            Init(goToMove, LerpCoro(goToMove, targetPosition, duration));
        }

        private IEnumerator LerpCoro(GameObject coroHost, Vector3 targetPosition, float duration) {
            yield return coroHost.LerpMoveUI(targetPosition, duration);
            yield return null;
        }

        private IEnumerator LerpCoro(GameObject coroHost, Func<Vector3> targetPosition, float duration) {
            yield return coroHost.LerpMoveUI(targetPosition(), duration);
            yield return null;
        }
    }
}