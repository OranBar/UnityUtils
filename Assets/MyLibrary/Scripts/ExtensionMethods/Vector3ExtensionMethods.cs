using UnityEngine;
using System.Collections;

namespace OranUnityUtils
{
	public static class Vector3ExtensionMethods {

		public static void SetX(this Vector3 vector, float x){
			Vector3 newPosition = vector;
			newPosition.x = x;
			vector = newPosition;
		}

		public static void SetY(this Vector3 vector, float y){
			Vector3 newPosition = vector;
			newPosition.y = y;
			vector = newPosition;
		}

		public static void SetZ(this Vector3 vector, float z){
			Vector3 newPosition = vector;
			newPosition.z = z;
			vector = newPosition;
		}

		public static Vector3 ClampVectorComponents(this Vector3 vector, float min, float max){
			return new Vector3 (Mathf.Clamp(vector.x, min, max), Mathf.Clamp(vector.y, min, max), Mathf.Clamp(vector.z, min, max));
		}

	}
}