using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class OBMonoBehaviour : MonoBehaviour {

	public void Update(){
#if UNITY_EDITOR
		if(UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode == false){
			EditorUpdate ();
		} else {
			OnUpdate();
		}
#endif
	}

	protected virtual void OnUpdate(){
		
	}

	protected virtual void EditorUpdate(){

	}
}
