using UnityEngine;
using System.Collections;
using System;


namespace OranUnityUtils 
{
    public static class GameObjectExtensionMethods {

        public static T GetOrAddComponent<T>(this GameObject go) where T : Component {
            T myComponent = go.GetComponent<T>();
            return (myComponent != null) ? myComponent : go.AddComponent<T>();
        }

        public static T AddOrGetComponent<T>(this GameObject go) where T : Component {
            return go.GetOrAddComponent<T>();
        }

        public static MonoBehaviour GetMonoBehaviour(this GameObject go) {
            if (go.GetComponent<MonoBehaviour>() == null) {
                return go.AddComponent<CoroutineHelper>();
            } else {
                return go.GetComponent<MonoBehaviour>();
            }
        }
     
        #region Coroutines: SuperCoroutine Timeline and SuperCoroutine
        /// <summary>
        /// Starts a SuperCoroutine timeline, executing each routine in order.
        /// Each routine waits for the previous one to finish before executing
        /// </summary>
        /// <returns>The SuperCoroutine timeline.</returns>
        /// <param name="routines">Routines.</param>
        public static Coroutine StartCoroutineTimeline(this GameObject go, params IEnumerator[] routines) {
            return go.AddOrGetComponent<CoroutineHelper>().StartCoroutine(go.StartCoroutineTimeline_Coro(routines));
        }

        private static IEnumerator StartCoroutineTimeline_Coro(this GameObject go, params IEnumerator[] routines) {
            foreach (IEnumerator currentRoutine in routines) {
                yield return go.AddOrGetComponent<CoroutineHelper>().StartCoroutine(currentRoutine);
            }
        }

        public static Coroutine StartCoroutine(this GameObject go, IEnumerator routine) {
            return go.AddOrGetComponent<CoroutineHelper>().StartCoroutine(routine);
        }

        public static SuperCoroutine<E> StartCoroutine<E>(this GameObject go, IEnumerator routine) {
            return go.AddOrGetComponent<CoroutineHelper>().StartCoroutine<E>(routine);
        }

        public static SuperCoroutine StartStoppableCoroutine(this GameObject go, IEnumerator routine) {
            return go.AddOrGetComponent<CoroutineHelper>().StartStoppableCoroutine(routine);
        }

        #endregion

        public static void ExecuteDelayed(this GameObject go, Action action, float delay) {
            go.AddOrGetComponent<CoroutineHelper>().StartCoroutineTimeline(
                go.WaitForSeconds_Coro(delay),
                go.ToIEnum(action)
            );
        }

        

        #region MonoBehaviour's GameObject Movement, Rotation and Scaling Advanced Lerps
        #region Movement

        public static SuperCoroutine MoveWithCurve(this GameObject go, Vector3 endPosition, float duration, AnimationCurve curve, Action onMoveEnd = null) {
            Func<float, float> tSmoothStepFunc = (t) => { return curve.Evaluate(t); };

            return Move(go, endPosition, duration, tSmoothStepFunc, onMoveEnd);
        }

        public static SuperCoroutine MoveWithCurveUI(this GameObject go, Vector2 localEndPosition, float duration, AnimationCurve curve, Action onMoveEnd = null) {
            Func<float, float> tSmoothStepFunc = (t) => { return curve.Evaluate(t); };

            return MoveUI(go, localEndPosition, duration, tSmoothStepFunc, onMoveEnd);
        }

        private static SuperCoroutine Move(this GameObject go, Vector3 endPosition, float duration, Func<float, float> tFunc, Action onMoveEnd) {
            Vector3 startPosition = go.transform.position;
            Action<Vector3> positionSetterFunc = (p) => go.transform.position = p;
            SuperCoroutine coro = go.StartStoppableCoroutine(LerpCoroutine(positionSetterFunc, startPosition, endPosition, duration, tFunc, onMoveEnd));
            return coro;
        }

        private static SuperCoroutine MoveLocal(this GameObject go, Vector3 endPosition, float duration, Func<float, float> tFunc, Action onMoveEnd) {
            Vector3 startPosition = go.transform.localPosition;
            Action<Vector3> positionSetterFunc = (p) => go.transform.localPosition = p;
            SuperCoroutine coro = go.StartStoppableCoroutine(LerpCoroutine(positionSetterFunc, startPosition, endPosition, duration, tFunc, onMoveEnd));
            return coro;
        }

        private static SuperCoroutine MoveUI(this GameObject go, Vector3 endPosition, float duration, Func<float, float> tFunc, Action onMoveEnd) {
            Vector3 startPosition = go.transform.localPosition;
            Action<Vector3> positionSetterFunc = (p) => go.GetComponent<RectTransform>().anchoredPosition = p;
            SuperCoroutine coro = go.StartStoppableCoroutine(LerpCoroutine(positionSetterFunc, startPosition, endPosition, duration, tFunc, onMoveEnd));
            return coro;
        }


        private static IEnumerator LerpCoroutine(Action<Vector3> valueSetterFunc, Vector3 start, Vector3 end, float duration,
            Func<float, float> tCurve, Action onMoveEnd) {

            float currentLerpTime = 0f;

            while (currentLerpTime < duration) {
                yield return null;
                currentLerpTime += Time.deltaTime;

                if (currentLerpTime > duration) {
                    currentLerpTime = duration;
                }
                float t = currentLerpTime / duration;
                valueSetterFunc(Vector3.LerpUnclamped(start, end, tCurve(t)));
            }
            onMoveEnd.Do(x => x());
        }

        private static IEnumerator SmoothDampCoroutine(Transform objTransf, Vector3 endPosition, Vector3 startVelocity, float duration, Action onMoveEnd) {
            Vector3 velocity = startVelocity;
            float t = duration;

            while (t > 0) {
                yield return null;
                t -= Time.deltaTime;

                if (t < 0f) {
                    t = 0f;
                }
                objTransf.position = Vector3.SmoothDamp(objTransf.position, endPosition, ref velocity, t);
            }
            onMoveEnd.Do(x => x());
        }
      
        #region Functions

        public static SuperCoroutine LerpMove(this GameObject go, Vector3 endPosition, float duration, Action onMoveEnd = null) {
            Func<float, float> tLinearFunc = (t) => { return t; };

            return Move(go, endPosition, duration, tLinearFunc, onMoveEnd);
        }

        public static SuperCoroutine LerpMoveLocal(this GameObject go, Vector3 localEndPosition, float duration, Action onMoveEnd = null) {
            Func<float, float> tLinearFunc = (t) => { return t; };

            return MoveLocal(go, localEndPosition, duration, tLinearFunc, onMoveEnd);
        }

        public static SuperCoroutine LerpMoveUI(this GameObject go, Vector3 targetAnchoredPosition, float duration, Action onMoveEnd = null) {
            Func<float, float> tLinearFunc = (t) => { return t; };

            return MoveUI(go, targetAnchoredPosition, duration, tLinearFunc, onMoveEnd);
        }

        public static SuperCoroutine ExponentialMove(this GameObject go, Vector3 endPosition, float duration, Action onMoveEnd = null) {
            Func<float, float> tSmoothStepFunc = (t) => t * t;

            return Move(go, endPosition, duration, tSmoothStepFunc, onMoveEnd);
        }

        public static SuperCoroutine ExponentialMoveLocal(this GameObject go, Vector3 localEndPosition, float duration, Action onMoveEnd = null) {
            Func<float, float> tSmoothStepFunc = (t) => t * t;

            return MoveLocal(go, localEndPosition, duration, tSmoothStepFunc, onMoveEnd);
        }

        public static SuperCoroutine ExponentialMoveUI(this GameObject go, Vector2 endPosition, float duration, Action onMoveEnd = null) {
            Func<float, float> tSmoothStepFunc = (t) => t * t;

            return MoveUI(go, endPosition, duration, tSmoothStepFunc, onMoveEnd);
        }

        public static SuperCoroutine SmoothstepMove(this GameObject go, Vector3 endPosition, float duration, Action onMoveEnd = null) {
            Func<float, float> tSmoothStepFunc = (t) => { return t * t * t * (t * (6f * t - 15f) + 10f); };

            return Move(go, endPosition, duration, tSmoothStepFunc, onMoveEnd);
        }

        public static SuperCoroutine SmoothstepMoveLocal(this GameObject go, Vector3 localEndPosition, float duration, Action onMoveEnd = null) {
            Func<float, float> tSmoothStepFunc = (t) => { return t * t * t * (t * (6f * t - 15f) + 10f); };

            return MoveLocal(go, localEndPosition, duration, tSmoothStepFunc, onMoveEnd);
        }

        public static SuperCoroutine SmoothstepMoveUI(this GameObject go, Vector3 targetAnchoredPosition, float duration, Action onMoveEnd = null) {
            Func<float, float> tSmoothStepFunc = (t) => { return t * t * t * (t * (6f * t - 15f) + 10f); };

            return MoveUI(go, targetAnchoredPosition, duration, tSmoothStepFunc, onMoveEnd);
        }

        public static SuperCoroutine SmoothDampMove(this GameObject go, Vector3 endPosition, float duration, Vector3 startVelocity, Action onMoveEnd = null) {
            return go.StartStoppableCoroutine(SmoothDampCoroutine(go.transform, endPosition, startVelocity, duration, onMoveEnd));
        }
        #endregion

        #endregion
        
        #region Rotations
        private static void Rotate(this GameObject go, Quaternion endRotation, float duration, Func<float, float> tFunc) {
            Quaternion startRotation = go.transform.rotation;
            Action<Quaternion> positionSetterFunc = (p) => go.transform.rotation = p;
            go.StartCoroutine(LerpRotateCoroutine(positionSetterFunc, startRotation, endRotation, duration, tFunc));
        }

        private static void RotateLocal(this GameObject go, Quaternion endRotation, float duration, Func<float, float> tFunc) {
            Quaternion startRotation = go.transform.rotation;
            Action<Quaternion> positionSetterFunc = (p) => go.transform.localRotation = p;
            go.StartCoroutine(LerpRotateCoroutine(positionSetterFunc, startRotation, endRotation, duration, tFunc));
        }

        private static IEnumerator LerpRotateCoroutine(Action<Quaternion> valueSetterFunc, Quaternion start, Quaternion end, float duration,
           Func<float, float> tCurve) {

            float currentLerpTime = 0f;

            while (currentLerpTime < duration) {
                yield return null;
                currentLerpTime += Time.deltaTime;

                if (currentLerpTime > duration) {
                    currentLerpTime = duration;
                }
                float t = currentLerpTime / duration;
                valueSetterFunc(Quaternion.Lerp(start, end, tCurve(t)));
            }
        }
        

        #region Functions
        public static void LerpRotate(this GameObject go, Quaternion endRotation, float duration) {
            Func<float, float> tLinearFunc = (t) => { return t; };

            Rotate(go, endRotation, duration, tLinearFunc);
        }

        public static void LerpRotateLocal(this GameObject go, Quaternion localEndRotation, float duration) {
            Func<float, float> tLinearFunc = (t) => { return t; };

            RotateLocal(go, localEndRotation, duration, tLinearFunc);
        }

        public static void SmoothstepRotate(this GameObject go, Quaternion endPosition, float duration) {
            Func<float, float> tSmoothStepFunc = (t) => { return t * t * t * (t * (6f * t - 15f) + 10f); };

            Rotate(go, endPosition, duration, tSmoothStepFunc);
        }

        public static void SmoothstepRotateLocal(this GameObject go, Quaternion localEndPosition, float duration) {
            Func<float, float> tSmoothStepFunc = (t) => { return t * t * t * (t * (6f * t - 15f) + 10f); };

            RotateLocal(go, localEndPosition, duration, tSmoothStepFunc);
        }
        #endregion

        #endregion

        #region Scaling
        public static SuperCoroutine LerpLocalScale(this GameObject go, Vector3 endLocalScale, float duration, Action onScaleEnd = null) {
            Func<float, float> tLinearFunc = (t) => { return t; };
            Vector3 startScale = go.transform.localScale;
            Action<Vector3> scaleSetterFunction = (p) => go.transform.localScale = p;

            return go.AddOrGetComponent<CoroutineHelper>().StartStoppableCoroutine(LerpCoroutine(scaleSetterFunction, startScale, endLocalScale, duration, tLinearFunc, onScaleEnd));
        }
        #endregion

        #endregion
    }
}


