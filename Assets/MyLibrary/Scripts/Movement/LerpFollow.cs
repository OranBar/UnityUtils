using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LerpFollow : MonoBehaviour {

	public Transform toFollow;
//	private Vector3 prevPosition, targetPosition;
//	private Quaternion prevRotation, targetRotation;


	public void LerpTransformFollow(Transform transformToFollow){
		/*
		toFollow = transformToFollow;
		prevPosition = toFollow.position;
		targetPosition = toFollow.position;
		prevRotation = toFollow.rotation;
		targetRotation = toFollow.rotation;
		*/
		this.transform.position = toFollow.position;
		this.transform.rotation = toFollow.rotation;

		StartCoroutine (LerpMovement());
		StartCoroutine (LerpRotation());
	}

	private IEnumerator LerpMovement(){
		while(true){
			
			this.transform.position = Vector3.Lerp (transform.position, toFollow.position, 0.1f);
			yield return null;
		}
	}

	private IEnumerator LerpRotation(){
		while(true){
			
			this.transform.rotation = Quaternion.Lerp (transform.rotation, toFollow.rotation, 0.1f);

			yield return null;
		}
	}
}
