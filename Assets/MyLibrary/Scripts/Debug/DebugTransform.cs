using UnityEngine;
using System;

public class DebugTransform : Transform {

    public bool debugPosition = true;
    public bool debugRotation = true;
    public bool debugScale = true;


    private Transform refTransform;

    public DebugTransform(Transform t) {
        this.refTransform = t;
    }

    public new Vector3 position {
        get {
            return refTransform.position;
        }
        set {
            if (debugPosition) {
                MonoBehaviour.print("position Changed");
            }
            refTransform.position = value;
        }
    }

    public new Vector3 localPosition {
        get {
            return refTransform.localPosition;
        }
        set {
            if (debugPosition) {
                MonoBehaviour.print("localPosition Changed");
            }
            refTransform.localPosition = value;
        }
    }

    public new Quaternion rotation {
        get {
            return refTransform.rotation;
        }
        set {
            if (debugRotation) {
                MonoBehaviour.print("rotation Changed");
            }
            refTransform.rotation = value;
        }
    }

    public new Vector3 localScale {
        get {
            return refTransform.localScale;
        }
        set {
            if (debugScale) {
                MonoBehaviour.print("localScale Changed");
            }
            refTransform.localScale = value;
        }
    }

    public new Vector3 eulerAngles {
        get {
            return refTransform.eulerAngles;
        }
        set {
            if (debugScale) {
                MonoBehaviour.print("eulerAngles Changed");
            }
            refTransform.eulerAngles = value;
        }
    }

    public new Vector3 localEulerAngles {
        get {
            return refTransform.eulerAngles;
        }
        set {
            if (debugScale) {
                MonoBehaviour.print("localEulerAngles Changed");
            }
            refTransform.localEulerAngles = value;
        }
    }

    public new Transform parent {
        get {
            return refTransform.parent;
        }
        set {
            if (debugScale) {
                MonoBehaviour.print("parent Changed");
            }
            refTransform.parent = value;
        }
    }

}
