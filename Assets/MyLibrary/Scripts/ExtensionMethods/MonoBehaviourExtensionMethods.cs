using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace OranUnityUtils
{
	public static class MonoBehaviourExtensionMethods
	{
		#region Coroutines: Coroutine Timeline and SuperCoroutine
		/// <summary>
		/// Starts a coroutine timeline, executing each routine in order.
		/// Each routine waits for the previous one to finish before executing
		/// </summary>
		/// <returns>The coroutine timeline.</returns>
		/// <param name="routines">Routines.</param>
		public static Coroutine StartCoroutineTimeline(this MonoBehaviour monoBehaviour, params IEnumerator[] routines){
			return monoBehaviour.StartCoroutine( monoBehaviour.StartCoroutineTimeline_Coro(routines) );
		}

		private static IEnumerator StartCoroutineTimeline_Coro(this MonoBehaviour monoBehaviour, params IEnumerator[] routines){
			foreach(IEnumerator currentRoutine in routines){
				yield return monoBehaviour.StartCoroutine( currentRoutine );
			}
		}


		public static SuperCoroutine<T> StartCoroutine<T>(this MonoBehaviour obj, IEnumerator coroutine){
			SuperCoroutine<T> coroutineObject = new SuperCoroutine<T>();
			coroutineObject.coroutine = obj.StartCoroutine(coroutineObject.RoutineWithReturn(coroutine));
			return coroutineObject;
		}
		#endregion

		public static void ExecuteDelayed(this MonoBehaviour monoBehaviour, Action action, float delay){
			monoBehaviour.StartCoroutineTimeline(
				monoBehaviour.WaitForSeconds_Coro(delay),
				monoBehaviour.ToIEnum( action )
			);
		}

		#region MonoBehaviour's GameObject Movement and Rotation Advanced Lerps
		#region Movement
		#region Functions
		public static void MoveWithCurve(this MonoBehaviour monoBehaviour, Vector3 endPosition, float duration, AnimationCurve curve, Action onMoveEnd=null){
			Func<float, float> tSmoothStepFunc = (t) => { return curve.Evaluate(t);};

			Move(monoBehaviour, endPosition, duration, tSmoothStepFunc, onMoveEnd);
		}

		public static void LerpMove(this MonoBehaviour monoBehaviour, Vector3 endPosition, float duration, Action onMoveEnd=null){
			Func<float, float> tLinearFunc = (t) => { return t;};

			Move(monoBehaviour, endPosition, duration, tLinearFunc, onMoveEnd);
		}

		public static void LerpMoveLocal(this MonoBehaviour monoBehaviour, Vector3 localEndPosition, float duration, Action onMoveEnd=null){
			Func<float, float> tLinearFunc = (t) => { return t;};

			MoveLocal(monoBehaviour, localEndPosition, duration, tLinearFunc, onMoveEnd);
		}

		public static void SmoothstepMove(this MonoBehaviour monoBehaviour, Vector3 endPosition, float duration, Action onMoveEnd=null){
			Func<float, float> tSmoothStepFunc = (t) => { return t*t*t * (t * (6f*t - 15f) + 10f);};

			Move(monoBehaviour, endPosition, duration, tSmoothStepFunc, onMoveEnd);
		}

		public static void SmoothstepMoveLocal(this MonoBehaviour monoBehaviour, Vector3 localEndPosition, float duration, Action onMoveEnd=null){
			Func<float, float> tSmoothStepFunc = (t) => { return t*t*t * (t * (6f*t - 15f) + 10f);};

			MoveLocal(monoBehaviour, localEndPosition, duration, tSmoothStepFunc, onMoveEnd);
		}
		#endregion


		private static void Move(this MonoBehaviour monoBehaviour, Vector3 endPosition, float duration, Func<float, float> tFunc, Action onMoveEnd){
			Vector3 startPosition = monoBehaviour.transform.position;
			Action<Vector3> positionSetterFunc = (p)=> monoBehaviour.transform.position = p;
			monoBehaviour.StartCoroutine(MoveCoroutine(positionSetterFunc, startPosition, endPosition, duration, tFunc, onMoveEnd));
		}

		private static void MoveLocal(this MonoBehaviour monoBehaviour, Vector3 endPosition, float duration, Func<float, float> tFunc, Action onMoveEnd){
			Vector3 startPosition = monoBehaviour.transform.localPosition;
			Action<Vector3> positionSetterFunc = (p)=> monoBehaviour.transform.localPosition = p;
			monoBehaviour.StartCoroutine(MoveCoroutine(positionSetterFunc, startPosition, endPosition, duration, tFunc, onMoveEnd));
		}



		private static IEnumerator MoveCoroutine(Action<Vector3> valueSetterFunc, Vector3 start, Vector3 end, float duration, 
			Func<float, float> tCurve, Action onMoveEnd){

			float currentLerpTime=0f;

			while(currentLerpTime < duration){
				yield return null;
				currentLerpTime += Time.deltaTime;
			
				if(currentLerpTime > duration){
					currentLerpTime = duration;
				}
				float t = currentLerpTime / duration;
				valueSetterFunc(Vector3.Lerp(start, end, tCurve(t)));
			}
			onMoveEnd.Do (x => x ());
		}


		#endregion

		#region Rotations
		#region Functions
		public static void LerpRotate(this MonoBehaviour monoBehaviour, Quaternion endRotation, float duration){
			Func<float, float> tLinearFunc = (t) => { return t;};

			Rotate(monoBehaviour, endRotation, duration, tLinearFunc);
		}

		public static void LerpRotateLocal(this MonoBehaviour monoBehaviour, Quaternion localEndRotation, float duration){
			Func<float, float> tLinearFunc = (t) => { return t;};

			RotateLocal(monoBehaviour, localEndRotation, duration, tLinearFunc);
		}

		public static void SmoothstepRotate(this MonoBehaviour monoBehaviour, Quaternion endPosition, float duration){
			Func<float, float> tSmoothStepFunc = (t) => { return t*t*t * (t * (6f*t - 15f) + 10f);};

			Rotate(monoBehaviour, endPosition, duration, tSmoothStepFunc);
		}

		public static void SmoothstepRotateLocal(this MonoBehaviour monoBehaviour, Quaternion localEndPosition, float duration){
			Func<float, float> tSmoothStepFunc = (t) => { return t*t*t * (t * (6f*t - 15f) + 10f);};

			RotateLocal(monoBehaviour, localEndPosition, duration, tSmoothStepFunc);
		}
		#endregion


		private static void Rotate(this MonoBehaviour monoBehaviour, Quaternion endRotation, float duration, Func<float, float> tFunc ){
			Quaternion startRotation = monoBehaviour.transform.rotation;
			Action<Quaternion> positionSetterFunc = (p)=> monoBehaviour.transform.rotation = p;
			monoBehaviour.StartCoroutine(RotateCoroutine(positionSetterFunc, startRotation, endRotation, duration, tFunc));
		}

		private static void RotateLocal(this MonoBehaviour monoBehaviour, Quaternion endRotation, float duration, Func<float, float> tFunc){
			Quaternion startRotation = monoBehaviour.transform.rotation;
			Action<Quaternion> positionSetterFunc = (p)=> monoBehaviour.transform.localRotation = p;
			monoBehaviour.StartCoroutine(RotateCoroutine(positionSetterFunc, startRotation, endRotation, duration, tFunc));
		}



		private static IEnumerator RotateCoroutine(Action<Quaternion> valueSetterFunc, Quaternion start, Quaternion end, float duration, 
			Func<float, float> tCurve){

			float currentLerpTime=0f;

			while(currentLerpTime < duration){
				yield return null;
				currentLerpTime += Time.deltaTime;

				if(currentLerpTime > duration){
					currentLerpTime = duration;
				}
				float t = currentLerpTime / duration;
				valueSetterFunc(Quaternion.Lerp(start, end, tCurve(t)));
			}
		}
		#endregion 

		#endregion


	}
}


