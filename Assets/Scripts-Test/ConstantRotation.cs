using UnityEngine;
using System.Collections;

public class ConstantRotation : MonoBehaviour {

	public Vector3 anglePerSecond = new Vector3(0,0,20f);
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(anglePerSecond * Time.deltaTime);
	}
}
