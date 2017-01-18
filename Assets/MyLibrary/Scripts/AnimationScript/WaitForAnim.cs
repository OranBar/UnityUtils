using UnityEngine;
using System;

/** <summary>Implementation of WaitWhile yield instruction. This can be later used as:
 *   yield return new WaitWhile(() => Princess.isInCastle);</summary>
 */
namespace OranUnityUtils 
{
    class WaitForAnim : CustomYieldInstruction {

        private IAnim animScript;

        public override bool keepWaiting {
            get { return animScript.IsPlaying(); }
        }


        public WaitForAnim(IAnim anim) {
            animScript = anim;
        }
    }
}