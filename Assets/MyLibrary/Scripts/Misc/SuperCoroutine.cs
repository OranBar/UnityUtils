using UnityEngine;
using System.Collections;
using System;

public class SuperCoroutine<T> : CustomYieldInstruction {
	
	private T _returnVal;
	public T ReturnVal {
		get{
			if(e != null){
				throw e;
			}
			return _returnVal;
		}
	}

	public Coroutine coroutine;
	private Exception e;
	private bool isCanceled = false;
	private bool isFinished = false;

	public override bool keepWaiting {
		get{ return !isFinished; }
	}

	public IEnumerator RoutineWithReturn(IEnumerator coroutine){
		while(true){
			if(isCanceled){
				e = new CoroutineStoppedException();
				break;
			}
			try{
				if(!coroutine.MoveNext()){
					break;
				}
			} catch(Exception e){
				this.e = e;
				break;
			}
			object yielded = coroutine.Current;

			if(yielded != null && yielded is T){
				_returnVal = (T) yielded;
				break;
			} else {
				yield return coroutine.Current;
			}
		}
		isFinished = true;
		yield break;
	}

	public void Stop(){
		isCanceled = true;
	}

	public bool WasExceptionThrown(){
		return (e != null);
	}

	public Exception GetException(){
		return e;
	}
}

public class CoroutineStoppedException: System.Exception{
	public CoroutineStoppedException() : base("Coroutine was aborted"){
		
	}
}

