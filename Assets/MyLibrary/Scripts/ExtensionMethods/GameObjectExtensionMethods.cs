using UnityEngine;
using System.Collections;

public static class GameObjectExtensionMethods {

	public static T GetOrAddComponent<T>(this GameObject go) where T : Component{
		T myComponent = go.GetComponent<T>();
		return (myComponent!=null) ? myComponent : go.AddComponent<T>();
	}


}
