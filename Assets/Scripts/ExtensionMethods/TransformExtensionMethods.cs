using UnityEngine;
using System.Collections;

public static class TransformExtensionMethods {

	#region Setters For Position - Change only ONE dimension
	#region Setters For World Position
	public static void SetXPosition(this Transform transform, float x){
		Vector3 newPosition = transform.position;
		newPosition.x = x;
		transform.position = newPosition;
	}

	public static void SetYPosition(this Transform transform, float y){
		Vector3 newPosition = transform.position;
		newPosition.y = y;
		transform.position = newPosition;
	}

	public static void SetZPosition(this Transform transform, float z){
		Vector3 newPosition = transform.position;
		newPosition.z = z;
		transform.position = newPosition;
	}
	#endregion

	#region Setters For Local Position
	public static void SetXLocalPosition(this Transform transform, float x){
		Vector3 newPosition = transform.localPosition;
		newPosition.x = x;
		transform.localPosition = newPosition;
	}

	public static void SetYLocalPosition(this Transform transform, float y){
		Vector3 newPosition = transform.localPosition;
		newPosition.y = y;
		transform.localPosition = newPosition;
	}

	public static void SetZLocalPosition(this Transform transform, float z){
		Vector3 newPosition = transform.localPosition;
		newPosition.z = z;
		transform.localPosition = newPosition;
	}
	#endregion
	#endregion

	#region Setters For Euler Angle - Change only ONE dimension
	#region Setters For World Rotation
	public static void SetXEulerAngle(this Transform transform, float x){
		Vector3 newEulerAngle = transform.eulerAngles;
		newEulerAngle.x = x;
		transform.eulerAngles = newEulerAngle;
	}

	public static void SetYEulerAngle(this Transform transform, float y){
		Vector3 newEulerAngle = transform.eulerAngles;
		newEulerAngle.y = y;
		transform.eulerAngles = newEulerAngle;
	}

	public static void SetZEulerAngle(this Transform transform, float z){
		Vector3 newEulerAngle = transform.eulerAngles;
		newEulerAngle.z = z;
		transform.eulerAngles = newEulerAngle;
	}
	#endregion

	#region Setters For Local Rotation
	public static void SetXLocalEulerAngle(this Transform transform, float x){
		Vector3 newEulerAngle = transform.localEulerAngles;
		newEulerAngle.x = x;
		transform.localEulerAngles = newEulerAngle;
	}

	public static void SetYLocalEulerAngle(this Transform transform, float y){
		Vector3 newEulerAngle = transform.localEulerAngles;
		newEulerAngle.y = y;
		transform.localEulerAngles = newEulerAngle;
	}

	public static void SetZLocalEulerAngle(this Transform transform, float z){
		Vector3 newEulerAngle = transform.localEulerAngles;
		newEulerAngle.z = z;
		transform.localEulerAngles = newEulerAngle;
	}
	#endregion
	#endregion

	public static void ResetPosAndRot(this Transform transf){
		transf.localPosition = Vector3.zero;
		transf.localRotation = Quaternion.identity;
	}

	public static Transform FindChildRecursive(this Transform transform, string childName){
		foreach(Transform t in transform){
			if(t.name == childName){
				return t;
			} else {
				Transform transf = FindChildRecursive (t, childName);
				if(transf!=null){
					return transf;
				}
			}
		}
		return null;
	}

}
