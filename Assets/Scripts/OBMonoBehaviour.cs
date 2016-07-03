using UnityEngine;
using System.Collections;

[System.Obsolete("I don't see any reason to use this over Extension Methods, apart from overrides")]
public class OBMonoBehaviour : MonoBehaviour {

	public T GetOrAddComponent<T>() where T : Component{
		T myComponent = base.GetComponent<T>();
		return (myComponent!=null) ? myComponent : gameObject.AddComponent<T>();
	}


}
