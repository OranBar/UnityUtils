using UnityEngine;
using System;
using System.Collections;
using System.Linq;

public class ForEachTest : MonoBehaviour {

    
    // Use this for initialization
    [ContextMenu("Run Test")]
	void Start () {
        var myEnum = Enumerable.Range(0, 11);
        myEnum.ForEach(x => print(x));
        print("-------------------------");
        myEnum.ForEach((i, x) => print(i + " " + x));
        print("-------------------------");
        myEnum.Select(x => { print(x); return x; } ).ToList();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
