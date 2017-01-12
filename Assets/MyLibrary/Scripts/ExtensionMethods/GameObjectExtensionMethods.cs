using UnityEngine;
using System.Collections;
using System;
using OranUnityUtils;

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

        public static Coroutine StartCoroutine(this GameObject go, IEnumerator routine) {
            return go.AddOrGetComponent<CoroutineHelper>().StartCoroutine(routine);
        }

        #region Coroutines: Coroutine Timeline and SuperCoroutine
        /// <summary>
        /// Starts a coroutine timeline, executing each routine in order.
        /// Each routine waits for the previous one to finish before executing
        /// </summary>
        /// <returns>The coroutine timeline.</returns>
        /// <param name="routines">Routines.</param>
        public static Coroutine StartCoroutineTimeline(this GameObject go, params IEnumerator[] routines) {
            return go.AddOrGetComponent<CoroutineHelper>().StartCoroutine(go.StartCoroutineTimeline_Coro(routines));
        }

        private static IEnumerator StartCoroutineTimeline_Coro(this GameObject go, params IEnumerator[] routines) {
            foreach (IEnumerator currentRoutine in routines) {
                yield return go.AddOrGetComponent<CoroutineHelper>().StartCoroutine(currentRoutine);
            }
        }


        public static SuperCoroutine<T> StartCoroutine<T>(this GameObject go, IEnumerator coroutine) {
            SuperCoroutine<T> coroutineObject = new SuperCoroutine<T>();
            coroutineObject.coroutine = go.AddOrGetComponent<CoroutineHelper>().StartCoroutine(coroutineObject.RoutineWithReturn(coroutine));
            return coroutineObject;
        }
        #endregion

        public static void ExecuteDelayed(this GameObject go, Action action, float delay) {
            go.AddOrGetComponent<CoroutineHelper>().StartCoroutineTimeline(
                go.WaitForSeconds_Coro(delay),
                go.ToIEnum(action)
            );
        }

        public static Coroutine LerpLocalScale(this GameObject go, Vector3 endLocalScale, float duration, Action onScaleEnd = null) {
            Func<float, float> tLinearFunc = (t) => { return t; };
            Vector3 startScale = go.transform.localScale;
            Action<Vector3> scaleSetterFunction = (p) => go.transform.localScale = p;

            return go.AddOrGetComponent<CoroutineHelper>().StartCoroutine(LerpCoroutine(scaleSetterFunction, startScale, endLocalScale, duration, tLinearFunc, onScaleEnd));
        }

        #region MonoBehaviour's GameObject Movement and Rotation Advanced Lerps
        #region Movement

        public static Coroutine MoveWithCurve(this GameObject go, Vector3 endPosition, float duration, AnimationCurve curve, Action onMoveEnd = null) {
            Func<float, float> tSmoothStepFunc = (t) => { return curve.Evaluate(t); };

            return Move(go, endPosition, duration, tSmoothStepFunc, onMoveEnd);
        }

        public static Coroutine MoveWithCurveUI(this GameObject go, Vector2 localEndPosition, float duration, AnimationCurve curve, Action onMoveEnd = null) {
            Func<float, float> tSmoothStepFunc = (t) => { return curve.Evaluate(t); };

            return MoveUI(go, localEndPosition, duration, tSmoothStepFunc, onMoveEnd);
        }

        #region Functions

        public static Coroutine LerpMove(this GameObject go, Vector3 endPosition, float duration, Action onMoveEnd = null) {
            Func<float, float> tLinearFunc = (t) => { return t; };

            return Move(go, endPosition, duration, tLinearFunc, onMoveEnd);
        }

        public static Coroutine LerpMoveLocal(this GameObject go, Vector3 localEndPosition, float duration, Action onMoveEnd = null) {
            Func<float, float> tLinearFunc = (t) => { return t; };

            return MoveLocal(go, localEndPosition, duration, tLinearFunc, onMoveEnd);
        }

        public static Coroutine LerpMoveUI(this GameObject go, Vector3 targetAnchoredPosition, float duration, Action onMoveEnd = null) {
            Func<float, float> tLinearFunc = (t) => { return t; };

            return MoveUI(go, targetAnchoredPosition, duration, tLinearFunc, onMoveEnd);
        }

        public static Coroutine SmoothstepMove(this GameObject go, Vector3 endPosition, float duration, Action onMoveEnd = null) {
            Func<float, float> tSmoothStepFunc = (t) => { return t * t * t * (t * (6f * t - 15f) + 10f); };

            return Move(go, endPosition, duration, tSmoothStepFunc, onMoveEnd);
        }

        public static Coroutine SmoothstepMoveLocal(this GameObject go, Vector3 localEndPosition, float duration, Action onMoveEnd = null) {
            Func<float, float> tSmoothStepFunc = (t) => { return t * t * t * (t * (6f * t - 15f) + 10f); };

            return MoveLocal(go, localEndPosition, duration, tSmoothStepFunc, onMoveEnd);
        }

        public static Coroutine SmoothstepMoveUI(this GameObject go, Vector3 targetAnchoredPosition, float duration, Action onMoveEnd = null) {
            Func<float, float> tSmoothStepFunc = (t) => { return t * t * t * (t * (6f * t - 15f) + 10f); };

            return MoveUI(go, targetAnchoredPosition, duration, tSmoothStepFunc, onMoveEnd);
        }

        public static Coroutine SmoothDampMove(this GameObject go, Vector3 endPosition, float duration, Vector3 startVelocity, Action onMoveEnd = null) {
            return go.StartCoroutine(SmoothDampCoroutine(go.transform, endPosition, startVelocity, duration, onMoveEnd));
        }
        #endregion



        private static Coroutine Move(this GameObject go, Vector3 endPosition, float duration, Func<float, float> tFunc, Action onMoveEnd) {
            Vector3 startPosition = go.transform.position;
            Action<Vector3> positionSetterFunc = (p) => go.transform.position = p;
            Coroutine coro = go.StartCoroutine(LerpCoroutine(positionSetterFunc, startPosition, endPosition, duration, tFunc, onMoveEnd));
            return coro;
        }

        private static Coroutine MoveLocal(this GameObject go, Vector3 endPosition, float duration, Func<float, float> tFunc, Action onMoveEnd) {
            Vector3 startPosition = go.transform.localPosition;
            Action<Vector3> positionSetterFunc = (p) => go.transform.localPosition = p;
            Coroutine coro = go.StartCoroutine(LerpCoroutine(positionSetterFunc, startPosition, endPosition, duration, tFunc, onMoveEnd));
            return coro;
        }

        private static Coroutine MoveUI(this GameObject go, Vector3 endPosition, float duration, Func<float, float> tFunc, Action onMoveEnd) {
            Vector3 startPosition = go.transform.localPosition;
            Action<Vector3> positionSetterFunc = (p) => go.GetComponent<RectTransform>().anchoredPosition = p;
            Coroutine coro = go.StartCoroutine(LerpCoroutine(positionSetterFunc, startPosition, endPosition, duration, tFunc, onMoveEnd));
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


        #endregion

        #region Rotations
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
        #endregion

        #endregion

    }
}


