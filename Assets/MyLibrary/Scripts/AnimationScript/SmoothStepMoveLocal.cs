using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OranUnityUtils 
{
    public class SmoothstepMoveLocal : AnimationScript {

        public SmoothstepMoveLocal(GameObject objToMove, Vector3 localEndPosition, float duration) {
            Init(objToMove, SmoothstepMoveLocal_Coro(objToMove, localEndPosition, duration));
        }

        private IEnumerator SmoothstepMoveLocal_Coro(GameObject objToMove, Vector3 localEndPosition, float duration) {
            yield return objToMove.SmoothstepMove(localEndPosition, duration);
            yield return null;
        }
    }
}