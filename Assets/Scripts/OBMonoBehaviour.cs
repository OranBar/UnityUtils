using UnityEngine;
using System.Collections;

public class OBMonoBehaviour : MonoBehaviour {

	public virtual void Update(){
#if UNITY_EDITOR
		if(UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode == false){
			EditorUpdate ();
		}
#endif
	}

	protected virtual void EditorUpdate(){

	}
}
