using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;



namespace IMX.ExtensionMethods
{
	public static class MonoBehaviourExtensionMethods
	{
		#region Coroutines: SuperCoroutine Timeline and SuperCoroutine
		/// <summary>
		/// Starts a SuperCoroutine timeline, executing each routine in order.
		/// Each routine waits for the previous one to finish before executing
		/// </summary>
		/// <returns>The SuperCoroutine timeline.</returns>
		/// <param name="routines">Routines.</param>
		public static Coroutine StartCoroutineTimeline(this MonoBehaviour monoBehaviour, params IEnumerator[] routines){
			return monoBehaviour.StartCoroutine( monoBehaviour.StartCoroutineTimeline_Coro(routines) );
		}

		private static IEnumerator StartCoroutineTimeline_Coro(this MonoBehaviour monoBehaviour, params IEnumerator[] routines){
			foreach(IEnumerator currentRoutine in routines){
				yield return monoBehaviour.StartCoroutine( currentRoutine );
			}
		}
        
		public static SuperCoroutine<T> StartCoroutine<T>(this MonoBehaviour monoBehaviour, IEnumerator coroutine){
			SuperCoroutine<T> coroutineObject = new SuperCoroutine<T>();
			coroutineObject.coroutine = monoBehaviour.StartCoroutine(coroutineObject.RoutineWithReturn(coroutine));
			return coroutineObject;
		}

        public static SuperCoroutine StartStoppableCoroutine(this MonoBehaviour monoBehaviour, IEnumerator coroutine) {
            SuperCoroutine coroutineObject = new SuperCoroutine();
            coroutineObject.coroutine = monoBehaviour.StartCoroutine(coroutineObject.RoutineWithReturn(coroutine));
            return coroutineObject;
        }
		#endregion

		public static void ExecuteDelayed(this MonoBehaviour monoBehaviour, Action action, float delay){
            monoBehaviour.gameObject.ExecuteDelayed(action, delay);
        }

        public static SuperCoroutine LerpLocalScale(this MonoBehaviour monoBehaviour, Vector3 endLocalScale, float duration, Action onScaleEnd = null) {
            return monoBehaviour.gameObject.LerpLocalScale(endLocalScale, duration, onScaleEnd);
        }
        
        #region GameObject Movement and Rotation Advanced Lerps
        #region Movement

        public static SuperCoroutine MoveWithCurve(this MonoBehaviour monoBehaviour, Vector3 endPosition, float duration, AnimationCurve curve, Action onMoveEnd = null) {
            return monoBehaviour.gameObject.MoveWithCurve(endPosition, duration, curve, onMoveEnd);
        }

        public static SuperCoroutine MoveWithCurveUI(this MonoBehaviour monoBehaviour, Vector2 localEndPosition, float duration, AnimationCurve curve, Action onMoveEnd = null) {
            return monoBehaviour.gameObject.MoveWithCurveUI(  localEndPosition,  duration,  curve,  onMoveEnd );
        }

        #region Functions

        public static SuperCoroutine LerpMove(this MonoBehaviour monoBehaviour, Vector3 endPosition, float duration, Action onMoveEnd=null){
            return monoBehaviour.gameObject.LerpMove(endPosition, duration, onMoveEnd );

            }

		public static SuperCoroutine LerpMoveLocal(this MonoBehaviour monoBehaviour, Vector3 localEndPosition, float duration, Action onMoveEnd=null){
            return monoBehaviour.gameObject.LerpMoveLocal(localEndPosition, duration, onMoveEnd);
		}

        public static SuperCoroutine LerpMoveUI(this MonoBehaviour monoBehaviour, Vector3 targetAnchoredPosition, float duration, Action onMoveEnd = null) {
            return monoBehaviour.gameObject.LerpMoveUI(targetAnchoredPosition, duration, onMoveEnd);
        }

        public static SuperCoroutine SmoothstepMove(this MonoBehaviour monoBehaviour, Vector3 endPosition, float duration, Action onMoveEnd=null){
            return monoBehaviour.gameObject.SmoothstepMove(endPosition, duration, onMoveEnd );
        }

		public static SuperCoroutine SmoothstepMoveLocal(this MonoBehaviour monoBehaviour, Vector3 localEndPosition, float duration, Action onMoveEnd=null){
            return monoBehaviour.gameObject.SmoothstepMoveLocal(localEndPosition, duration, onMoveEnd );
        }

        public static SuperCoroutine SmoothstepMoveUI(this MonoBehaviour monoBehaviour, Vector3 targetAnchoredPosition, float duration, Action onMoveEnd = null) {
            return monoBehaviour.gameObject.SmoothstepMoveUI(targetAnchoredPosition, duration, onMoveEnd);
        }

        public static SuperCoroutine SmoothDampMove(this MonoBehaviour monoBehaviour, Vector3 endPosition, float duration, Vector3 startVelocity, Action onMoveEnd=null){
            return monoBehaviour.gameObject.SmoothDampMove(endPosition, duration, startVelocity, onMoveEnd);
		}

        public static SuperCoroutine ExponentialMove(this MonoBehaviour monoBehaviour, Vector3 endPosition, float duration, Action onMoveEnd = null) {
           return monoBehaviour.gameObject.ExponentialMove(endPosition, duration, onMoveEnd);
        }

        public static SuperCoroutine ExponentialMoveLocal(this MonoBehaviour monoBehaviour, Vector3 localEndPosition, float duration, Action onMoveEnd = null) {
            return monoBehaviour.gameObject.ExponentialMoveLocal(localEndPosition, duration, onMoveEnd); 
        }

        public static SuperCoroutine ExponentialMoveUI(this MonoBehaviour monoBehaviour, Vector2 endPosition, float duration, Action onMoveEnd = null) {
            return monoBehaviour.gameObject.ExponentialMoveUI(endPosition, duration, onMoveEnd);
        }



        #endregion

        #endregion

        #region Rotations

        private static void Rotate(this MonoBehaviour monoBehaviour, Quaternion endRotation, float duration, Func<float, float> tFunc) {
            Quaternion startRotation = monoBehaviour.transform.rotation;
            Action<Quaternion> positionSetterFunc = (p) => monoBehaviour.transform.rotation = p;
            monoBehaviour.StartCoroutine(LerpRotateCoroutine(positionSetterFunc, startRotation, endRotation, duration, tFunc));
        }

        private static void RotateLocal(this MonoBehaviour monoBehaviour, Quaternion endRotation, float duration, Func<float, float> tFunc) {
            Quaternion startRotation = monoBehaviour.transform.rotation;
            Action<Quaternion> positionSetterFunc = (p) => monoBehaviour.transform.localRotation = p;
            monoBehaviour.StartCoroutine(LerpRotateCoroutine(positionSetterFunc, startRotation, endRotation, duration, tFunc));
        }
        
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
        
		private static IEnumerator LerpRotateCoroutine(Action<Quaternion> valueSetterFunc, Quaternion start, Quaternion end, float duration, 
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