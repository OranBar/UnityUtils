using UnityEngine;
using System.Collections;
using OranUnityUtils;

public class TestSmoothstep : MonoBehaviour {

	private Vector3 startPosition;

	void Start(){
		startPosition = this.transform.localPosition;
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.A)){
			print("Global moveTo");
			this.Smoothstep(new Vector3(5,0,0), 2f);
		}
		if(Input.GetKeyDown(KeyCode.S)){
			print("Local moveTo");
			this.SmoothstepLocal(new Vector3(0,0,0), 2f);
		}
		if(Input.GetKeyDown(KeyCode.Q)){
			print("Global moveTo");
			this.LerpMove(new Vector3(5,0,0), 2f);
		}
		if(Input.GetKeyDown(KeyCode.W)){
			print("Local moveTo");
			this.LerpMoveLocal(new Vector3(0,0,0), 2f);
		}
		if(Input.GetKeyDown(KeyCode.R)){
			print("Reset");
			this.transform.localPosition = startPosition;
		}
	}
}
