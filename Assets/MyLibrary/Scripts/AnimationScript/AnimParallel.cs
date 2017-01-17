using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using OranUnityUtils;

public class AnimParallel : IAnim {

    public override event UnityAction OnAnimationFinish;

    private List<IAnim> animationsInParallel;

    private bool hasAnimBegun = false;
    private int isAnimDone = 0;
    

    public AnimParallel(params IAnim[] animations) {
        animationsInParallel = new List<IAnim>();
        animationsInParallel.AddRange(animations);
    }

    public override bool IsDone() {
        return hasAnimBegun && isAnimDone==0;
    }

    public override bool IsPlaying() {
        return hasAnimBegun && isAnimDone>0;
    }

    public override Coroutine StartAnimation() {
        GameObject temp = new GameObject("Temp Obj");
        OnAnimationFinish += () => UnityEngine.Object.DestroyImmediate(temp);
        hasAnimBegun = true;
        isAnimDone = animationsInParallel.Count;
        Coroutine myCoro = temp.StartCoroutine(StartAnimationSequence_Coro());
        //temp.StartCoroutine(SetDoneWhenFinished_Coro(myCoro));
        return myCoro;
    }

    private IEnumerator StartAnimationSequence_Coro() {
        foreach (IAnim animation in animationsInParallel) {
            animation.OnAnimationFinish += AnimationFinished;
            animation.StartAnimation();
        }
        yield return null;
    }

    private void AnimationFinished() {
        isAnimDone--;
        if (isAnimDone == 0) {
            if (OnAnimationFinish != null) {
                OnAnimationFinish();
            }
        }
    }
    
}


