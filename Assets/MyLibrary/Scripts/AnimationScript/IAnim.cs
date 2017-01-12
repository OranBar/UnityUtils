using UnityEngine;
using UnityEngine.Events;

public interface IAnim {

    event UnityAction OnAnimationFinish;

    Coroutine StartAnimation();
    bool IsPlaying();
    bool IsDone();
    
        
}
