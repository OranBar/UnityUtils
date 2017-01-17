using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OranUnityUtils
{
	public static class UnityObjectExtensionMethods 
	{

		public static IEnumerator ToIEnum(this object unityObj, Action method){
			return ToIEnumImpl(method);
		}

		private static IEnumerator ToIEnumImpl(Action method){
			method();
			yield return null;
		}

		public static IEnumerator WaitForSeconds_Coro(this object unityObj, float time){
			return WaitForSecondsImpl(time);
		}

		private static IEnumerator WaitForSecondsImpl(float time){
			yield return new WaitForSeconds(time);
		}

		public static IEnumerator WaitForAnim(this object unityObj, Animator anim, int layer=0){
			AnimatorStateInfo animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
			float currentAnimTime = animStateInfo.normalizedTime * animStateInfo.length;
			yield return new WaitForSeconds( animStateInfo.length - currentAnimTime );
		}

	}
}

