using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public abstract class HiearchyOp : MonoBehaviour {
	
	public string ignoreIfNameContains;
	public string onlyIfNameContains;
	public int recursionDepth = 999;
	public bool execute = false, usaEGetta = true;
	
	
	void Update () {
		if(execute){
			execute = false;
			Single(this.transform);
			Recursive(this.transform, 0);
			if(usaEGetta){
				DestroyImmediate(GetComponent<HiearchyOp>());
			}
		}
	}
	
	private void Recursive(Transform transf, int depth){
		if(depth > recursionDepth){
			return;
		}
		Single(transf);
		foreach(Transform child in transf){
			Recursive(child, ++depth);
		}
		
	}
	
	private void Single(Transform transf){
		if(onlyIfNameContains.Length == 0 || (onlyIfNameContains.Length > 0 && transf.name.Contains(onlyIfNameContains) )){
			if(ignoreIfNameContains.Length == 0 || !(ignoreIfNameContains.Length > 0 && transf.name.Contains(ignoreIfNameContains) )){
				SingleHook(transf);
			}
		} 
	}
	
	protected abstract void SingleElementHook(Transform transf);
}


