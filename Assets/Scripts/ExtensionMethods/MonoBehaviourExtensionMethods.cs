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
		public static void MoveWithCurve(this MonoBehaviour monoBehaviour, Vector3 endPosition, float duration, AnimationCurve curve, int framesBetweenIterations=1){
			Func<float, float> tSmoothStepFunc = (t) => { return curve.Evaluate(t);};

			Move(monoBehaviour, endPosition, duration, tSmoothStepFunc, framesBetweenIterations);
		}

		public static void LerpMove(this MonoBehaviour monoBehaviour, Vector3 endPosition, float duration, int framesBetweenIterations=1){
			Func<float, float> tLinearFunc = (t) => { return t;};

			Move(monoBehaviour, endPosition, duration, tLinearFunc, framesBetweenIterations);
		}

		public static void LerpMoveLocal(this MonoBehaviour monoBehaviour, Vector3 localEndPosition, float duration, int framesBetweenIterations=1){
			Func<float, float> tLinearFunc = (t) => { return t;};

			MoveLocal(monoBehaviour, localEndPosition, duration, tLinearFunc, framesBetweenIterations);
		}

		public static void SmoothstepMove(this MonoBehaviour monoBehaviour, Vector3 endPosition, float duration, int framesBetweenIterations=1){
			Func<float, float> tSmoothStepFunc = (t) => { return t*t*t * (t * (6f*t - 15f) + 10f);};

			Move(monoBehaviour, endPosition, duration, tSmoothStepFunc, framesBetweenIterations);
		}

		public static void SmoothstepMoveLocal(this MonoBehaviour monoBehaviour, Vector3 localEndPosition, float duration, int framesBetweenIterations=1){
			Func<float, float> tSmoothStepFunc = (t) => { return t*t*t * (t * (6f*t - 15f) + 10f);};

			MoveLocal(monoBehaviour, localEndPosition, duration, tSmoothStepFunc, framesBetweenIterations);
		}

		private static void Move(this MonoBehaviour monoBehaviour, Vector3 endPosition, float duration, Func<float, float> tFunc, int framesBetweenIterations){
			Vector3 startPosition = monoBehaviour.transform.position;
			Action<Vector3> positionSetterFunc = (p)=> monoBehaviour.transform.position = p;
			monoBehaviour.StartCoroutine(ProgressiveChangeVector3_Coro(positionSetterFunc, startPosition, endPosition, duration, tFunc, framesBetweenIterations));
		}

		private static void MoveLocal(this MonoBehaviour monoBehaviour, Vector3 endPosition, float duration, Func<float, float> tFunc, int framesBetweenIterations){
			Vector3 startPosition = monoBehaviour.transform.localPosition;
			Action<Vector3> positionSetterFunc = (p)=> monoBehaviour.transform.localPosition = p;
			monoBehaviour.StartCoroutine(ProgressiveChangeVector3_Coro(positionSetterFunc, startPosition, endPosition, duration, tFunc, framesBetweenIterations));
		}
		#endregion

		#region Rotations
		public static void LerpRotate(this MonoBehaviour monoBehaviour, Vector3 endRotation, float duration, int framesBetweenIterations=1){
			Func<float, float> tLinearFunc = (t) => { return t;};

			Rotate(monoBehaviour, endRotation, duration, tLinearFunc, framesBetweenIterations);
		}

		public static void LerpRotateLocal(this MonoBehaviour monoBehaviour, Vector3 localEndRotation, float duration, int framesBetweenIterations=1){
			Func<float, float> tLinearFunc = (t) => { return t;};

			RotateLocal(monoBehaviour, localEndRotation, duration, tLinearFunc, framesBetweenIterations);
		}

		public static void SmoothstepRotate(this MonoBehaviour monoBehaviour, Vector3 endPosition, float duration, int framesBetweenIterations=1){
			Func<float, float> tSmoothStepFunc = (t) => { return t*t*t * (t * (6f*t - 15f) + 10f);};

			Rotate(monoBehaviour, endPosition, duration, tSmoothStepFunc, framesBetweenIterations);
		}

		public static void SmoothstepRotateLocal(this MonoBehaviour monoBehaviour, Vector3 localEndPosition, float duration, int framesBetweenIterations=1){
			Func<float, float> tSmoothStepFunc = (t) => { return t*t*t * (t * (6f*t - 15f) + 10f);};

			RotateLocal(monoBehaviour, localEndPosition, duration, tSmoothStepFunc, framesBetweenIterations);
		}

		private static void Rotate(this MonoBehaviour monoBehaviour, Vector3 endRotation, float duration, Func<float, float> tFunc, int framesBetweenIterations){
			Vector3 startRotation = monoBehaviour.transform.eulerAngles;
			Action<Vector3> positionSetterFunc = (p)=> monoBehaviour.transform.eulerAngles = p;
			monoBehaviour.StartCoroutine(ProgressiveChangeVector3_Coro(positionSetterFunc, startRotation, endRotation, duration, tFunc, framesBetweenIterations));
		}

		private static void RotateLocal(this MonoBehaviour monoBehaviour, Vector3 endRotation, float duration, Func<float, float> tFunc, int framesBetweenIterations){
			Vector3 startRotation = monoBehaviour.transform.localEulerAngles;
			Action<Vector3> positionSetterFunc = (p)=> monoBehaviour.transform.localEulerAngles = p;
			monoBehaviour.StartCoroutine(ProgressiveChangeVector3_Coro(positionSetterFunc, startRotation, endRotation, duration, tFunc, framesBetweenIterations));
		}
		#endregion 


		private static IEnumerator ProgressiveChangeVector3_Coro(Action<Vector3> valueSetterFunc, Vector3 start, Vector3 end, float duration, 
			                                                     Func<float, float> tCurve, int framesBetweenIterations){

			float currentLerpTime=0f;

			yield return null;
			currentLerpTime += Time.deltaTime;

			while(currentLerpTime < duration){
				if(currentLerpTime > duration){
					currentLerpTime = duration;
				}
				float t = currentLerpTime / duration;
				valueSetterFunc(Vector3.Lerp(start, end, tCurve(t)));

				for(int i=0; i<framesBetweenIterations; i++){
					yield return null;
					currentLerpTime += Time.deltaTime;
				}
			}
		}
		#endregion


	}
}


