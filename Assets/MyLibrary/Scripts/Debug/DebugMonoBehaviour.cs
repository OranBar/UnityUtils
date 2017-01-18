using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMonoBehaviour : MonoBehaviour {

	
    public new DebugTransform transform {
        get { return new DebugTransform(base.transform); }
        set { ; }
    }

    
    

    

}
