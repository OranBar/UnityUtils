using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

public abstract class IAnim {

    public abstract event UnityAction OnAnimationFinish;

    public abstract Coroutine StartAnimation();
    public abstract bool IsPlaying();
    public abstract bool IsDone();

    public IAnim Then(params IAnim[] animations) {
        List<IAnim> animationSequence = new List<IAnim>();
        animationSequence.Add(this);
        animationSequence.AddRange(animations);

        return new AnimSequence( animationSequence.ToArray() );
    }

    public IAnim Parallel(params IAnim[] animations) {
        List<IAnim> animationsInParallel = new List<IAnim>();
        animationsInParallel.Add(this);
        animationsInParallel.AddRange(animations);
        return new AnimParallel( animationsInParallel.ToArray() );
    }
    

}
