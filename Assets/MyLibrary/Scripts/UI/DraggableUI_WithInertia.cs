using System;
using System.Collections.Generic;
using UnityEngine;

/** Not working
 */
public class DraggableUI_WithInertia : DraggableUI {
    
    [Tooltip("1 means no effect. >1 will always get faster and never stop. <1 will slowly converge to zero, then stop. 0 is no inertia at all")]
    public float drag = 0.9f;

    private Vector3 velocity;
    private Vector3 myPreviousPosition;

    protected override void Start() {
        base.Start();
        velocity = Vector3.zero;
        myPreviousPosition = this.transform.position;
    }

    protected override void Update() {
        base.Update();
        velocity += this.transform.position - myPreviousPosition;
        velocity *= drag;
        if (this.isPressed == false) {
            this.transform.position += velocity;
        }

        myPreviousPosition = this.transform.position;
    }

}
