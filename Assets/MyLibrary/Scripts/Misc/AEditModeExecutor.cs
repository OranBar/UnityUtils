using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public abstract class AEditModeExecutor : MonoBehaviour {

	public void Update(){
#if UNITY_EDITOR
		if(UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode == false){
			EditorUpdate ();
		} else {
			OnUpdate();
		}
#endif
	}

	protected abstract void OnUpdate(){
		
	}

    protected abstract void EditorUpdate();
}
