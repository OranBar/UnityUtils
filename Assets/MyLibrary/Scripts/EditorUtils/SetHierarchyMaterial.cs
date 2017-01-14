using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class SetHierarchyMaterial : HiearchyOp {

	public Material materialToSet;

	protected override void SingleElementHook(Transform transf) {
		try{
			transf.GetComponent<Renderer>().material = materialToSet;
		}catch (MissingComponentException e){
			Debug.LogError(e.Message);
		}
	}
}
