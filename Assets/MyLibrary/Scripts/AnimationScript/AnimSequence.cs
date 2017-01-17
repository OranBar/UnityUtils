using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using OranUnityUtils;
using UnityEngine.Events;

public class AnimSequence : IAnim {

    public override event UnityAction OnAnimationFinish;

    private List<IAnim> animationsSequence;

    private bool hasAnimBegun = false;
    private bool isAnimDone = false;


    public AnimSequence(params IAnim[] animations) {
        animationsSequence = new List<IAnim>();
        animationsSequence.AddRange(animations);
    }

    public override bool IsDone() {
        return hasAnimBegun && isAnimDone;
    }

    public override bool IsPlaying() {
        return hasAnimBegun && isAnimDone==false;
    }

    public override Coroutine StartAnimation() {
        GameObject temp = new GameObject("Temp Obj");
        OnAnimationFinish += () => UnityEngine.Object.DestroyImmediate(temp);
        Coroutine myCoro = temp.StartCoroutine(StartAnimationSequence_Coro());
        hasAnimBegun = true;
        temp.StartCoroutine(SetDoneWhenFinished_Coro(myCoro));
        return myCoro;
    }

    private IEnumerator StartAnimationSequence_Coro() {
        foreach(IAnim animation in animationsSequence) {
            animation.StartAnimation();
            yield return new WaitForAnim(animation);
        }
        yield return null;
    }

    protected IEnumerator SetDoneWhenFinished_Coro(Coroutine coro) {
        yield return coro;
        isAnimDone = true;
        OnAnimationFinish.Invoke();
    }
}
