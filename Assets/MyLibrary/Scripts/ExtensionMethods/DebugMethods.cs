using System;
using UnityEngine;
using System.Reflection;
using System.Diagnostics;

public static class DebugMethods
{
	public static void PrintMethodName(){
		StackTrace st = new StackTrace ();
		StackFrame sf = st.GetFrame (1);

		MethodBase currentMethodName = sf.GetMethod ();
		UnityEngine.Debug.Log ("I'm in "+currentMethodName);
	}

}


