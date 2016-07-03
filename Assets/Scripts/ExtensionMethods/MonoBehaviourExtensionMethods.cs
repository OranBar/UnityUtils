using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using System;

namespace OranUnityUtils
{
	public static class MonoBehaviourExtensionMethods {
		
		public static void LerpMove(this MonoBehaviour monoBehaviour, Vector3 endPosition, float duration){
			Func<float, float> tLinearFunc = (t) => { return t;};
			
			Move(monoBehaviour, endPosition, duration, tLinearFunc);
		}
		
		public static void LerpMoveLocal(this MonoBehaviour monoBehaviour, Vector3 localEndPosition, float duration){
			Func<float, float> tLinearFunc = (t) => { return t;};
			
			MoveLocal(monoBehaviour, localEndPosition, duration, tLinearFunc);
		}
		
		
		public static void Smoothstep(this MonoBehaviour monoBehaviour, Vector3 endPosition, float duration){
			Func<float, float> tSmoothStepFunc = (t) => { return t*t*t * (t * (6f*t - 15f) + 10f);};
			
			Move(monoBehaviour, endPosition, duration, tSmoothStepFunc);
		}
		
		public static void SmoothstepLocal(this MonoBehaviour monoBehaviour, Vector3 localEndPosition, float duration){
			Func<float, float> tSmoothStepFunc = (t) => { return t*t*t * (t * (6f*t - 15f) + 10f);};
			
			MoveLocal(monoBehaviour, localEndPosition, duration, tSmoothStepFunc);
		}
		
		private static void Move(this MonoBehaviour monoBehaviour, Vector3 endPosition, float duration, Func<float, float> tFunc){
			Vector3 startPosition = monoBehaviour.transform.position;
			Action<Vector3> positionSetterFunc = (p)=> monoBehaviour.transform.position = p;
			monoBehaviour.StartCoroutine(Move_Coro(positionSetterFunc, startPosition, endPosition, duration, tFunc));
		}
		
		private static void MoveLocal(this MonoBehaviour monoBehaviour, Vector3 endPosition, float duration, Func<float, float> tFunc){
			Vector3 startPosition = monoBehaviour.transform.localPosition;
			Action<Vector3> positionSetterFunc = (p)=> monoBehaviour.transform.localPosition = p;
			monoBehaviour.StartCoroutine(Move_Coro(positionSetterFunc, startPosition, endPosition, duration, tFunc));
		}
		
		private static IEnumerator Move_Coro(Action<Vector3> positionSetterFunc, Vector3 startPosition, Vector3 endPosition, float duration, 
			Func<float, float> tCurve){
			
			float currentLerpTime=0f;
			
			yield return null;
			while(currentLerpTime < duration){
				currentLerpTime += Time.deltaTime;
				if(currentLerpTime > duration){
					currentLerpTime = duration;
				}
				float t = currentLerpTime / duration;
				positionSetterFunc(Vector3.Lerp(startPosition, endPosition, tCurve(t)));
				yield return null;
			}
		}
		
	}
}

	