using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;


namespace OranUnityUtils 
{
    public class AnimationScript : IAnim {

        public override event UnityAction OnAnimationFinish;

        protected Func<Coroutine> animationFunction { get; set; }
        private Coroutine myAnimationCoroutine;
        private IEnumerator myAnimation_coro;

        private GameObject associatedGO;
        private CoroutineHelper coroutineHost;

        private bool hasAnimBegun = false;
        private bool isAnimDone = false;

        private bool wasInitialized = false;


        public AnimationScript() {

        }

        /**<param name="coroHost">The GameObject on which the coroutine will be executed. This object needs to stay active for the coroutine to keep working. So put some thought in your choice, will ya?</param>
        */
        public AnimationScript(GameObject coroHost, IEnumerator animCoro) {
            Init(coroHost, animCoro);
        }

        /**<param name="coroHost">The GameObject on which the coroutine will be executed. This object needs to stay active for the coroutine to keep working. So put some thought in your choice, will ya?</param>
        */
        protected virtual void Init(GameObject coroHost, IEnumerator animCoro) {
            this.associatedGO = coroHost;
            this.coroutineHost = associatedGO.AddOrGetComponent<CoroutineHelper>();
            this.myAnimation_coro = animCoro;
            wasInitialized = true;
        }


        public override Coroutine StartAnimation() {
            if (wasInitialized == false) { Debug.LogError("AnimationScript has not been initialized"); }

            //myAnimationCoroutine = coroutineHost.StartCoroutine_Tracked(this, myAnimation_coro);
            myAnimationCoroutine = coroutineHost.StartCoroutine(myAnimation_coro);
            coroutineHost.StartCoroutine(SetDoneWhenFinished_Coro(myAnimationCoroutine));
            hasAnimBegun = true;
            return myAnimationCoroutine;
        }

        protected IEnumerator SetDoneWhenFinished_Coro(Coroutine coro) {
            yield return coro;
            isAnimDone = true;
            if (OnAnimationFinish != null) {
                OnAnimationFinish();
            }
        }

        public Coroutine GetCoroutine() {
            if (wasInitialized == false) { Debug.LogError("AnimationScript has not been initialized"); }
            return myAnimationCoroutine;
        }

        public override bool IsPlaying() {
            if (wasInitialized == false) { Debug.LogError("AnimationScript has not been initialized"); }
            return hasAnimBegun && isAnimDone == false;
        }

        public override bool IsDone() {
            if (wasInitialized == false) { Debug.LogError("AnimationScript has not been initialized"); }
            return hasAnimBegun && isAnimDone;
        }
    }
}