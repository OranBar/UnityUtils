using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoroutineHelper : MonoBehaviour {

    public List<Coroutine> routinesInExecution;

    private void Awake() {
        routinesInExecution = new List<Coroutine>();
    }
    
    //I save the routines being executed in a list. Might be useful later for pause/stop/abort, or with SuperCoroutines<>
	public new Coroutine StartCoroutine(IEnumerator routine) {
        Coroutine coroutine = base.StartCoroutine(routine);
        base.StartCoroutine(RemoveWhenDone(coroutine));
        routinesInExecution.Add(coroutine);
        return coroutine;
    }

    public IEnumerator RemoveWhenDone(Coroutine coro) {
        yield return coro;
        routinesInExecution.Remove(coro);
    }
}
