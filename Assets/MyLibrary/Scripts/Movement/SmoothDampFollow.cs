using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SmoothDampFollow : MonoBehaviour {

	public Transform toFollow;
	private const float smoothTime = 0.05f;

	public void SmoothTransformFollow(Transform transformToFollow){
		toFollow = transformToFollow;

		this.transform.position = toFollow.position;
		this.transform.rotation = toFollow.rotation;

		StartCoroutine (SmoothMovement());
		StartCoroutine (SmoothRotation());
	}

	private IEnumerator SmoothMovement(){
		Vector3 velocity = Vector3.zero;
		while(true){
			this.transform.position = Vector3.SmoothDamp (this.transform.position, toFollow.position, ref velocity, smoothTime);
			yield return null;
		}
	}

	private IEnumerator SmoothRotation(){

		while(true){
			this.transform.rotation = Quaternion.Slerp (transform.rotation, toFollow.rotation, smoothTime);
			yield return null;
		}
	}
}
