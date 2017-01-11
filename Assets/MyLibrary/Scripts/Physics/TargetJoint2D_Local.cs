using UnityEngine;

public class TargetJoint2D_Local : MonoBehaviour {

    private Vector2 targetLocalPositon;
    private TargetJoint2D myTargetJoint;

    private Vector2 previousTarget;
    private RectTransform myRectTransf;

    private Vector3 parentPreviousPosition;
    private bool wasTargetJointEnabledLastFrame;

    private int skipFramesBeforeResuming = 0;

    void Start() {
        myTargetJoint = GetComponent<TargetJoint2D>();
        parentPreviousPosition = this.transform.parent.position;
    }

    void LateUpdate() {
        if (myTargetJoint.enabled) {
            if(wasTargetJointEnabledLastFrame == false) {
                skipFramesBeforeResuming = 5;
            }
            //This line effectively skips the frame when the TargetJoint2D is re-enabled. It thus allows for setting a new position in the same frame the component is activated. 
            //if (wasTargetJointEnabledLastFrame==true) {
            if (skipFramesBeforeResuming-- <= 0) {
                myTargetJoint.target += (Vector2)(this.transform.parent.position - parentPreviousPosition);
                parentPreviousPosition = this.transform.parent.position;
            }
        }
        wasTargetJointEnabledLastFrame = myTargetJoint.enabled;
    }

    public void OnEnable() {
        parentPreviousPosition = this.transform.parent.Return(p=>p.position, Vector3.zero);
    }


}

