using UnityEngine;
using System.Collections;

public static class GameObjectExtensionMethods {

	public static T GetOrAddComponent<T>(this GameObject go) where T : Component{
		T myComponent = go.GetComponent<T>();
		return (myComponent!=null) ? myComponent : go.AddComponent<T>();
	}

    public static T AddOrGetComponent<T>(this GameObject go) where T : Component {
        return go.GetOrAddComponent<T>();
    }

    public static MonoBehaviour GetMonoBehaviour(this GameObject go) {
        if(go.GetComponent<MonoBehaviour>() == null) {
            return go.AddComponent<DummyMonoBehaviour>();
        } else {
            return go.GetComponent<MonoBehaviour>();
        }

    }
}
