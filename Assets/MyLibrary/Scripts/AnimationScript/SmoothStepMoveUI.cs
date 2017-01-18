using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OranUnityUtils 
{
    public class SmoothstepMoveUI : AnimationScript {

        public SmoothstepMoveUI(GameObject objToMove, Vector2 targetAnchorPosition, float duration) {
            Init(objToMove, SmoothstepMoveUI_Coro(objToMove, targetAnchorPosition, duration));
        }

        private IEnumerator SmoothstepMoveUI_Coro(GameObject objToMove, Vector2 targetAnchorPosition, float duration) {
            yield return objToMove.SmoothstepMoveUI(targetAnchorPosition, duration);
            yield return null;
        }
    }
}