//Uncommenting this line will make the class work with mouse as opposed to touches
//#define MOUSEINPUT

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using OranUnityUtils;
using System;
using System.Collections.Generic;

[Serializable]
public enum ColliderType {
    Circle2D, Box2D
}

[RequireComponent(typeof(Rigidbody2D))]
public class DraggableUI_Collider: MonoBehaviour {

    public ColliderType dragAreaCollider;
    public bool fitColliderToThisObj;
    [Tooltip("The class will work fine if the parameter is null. If this parameter is null, the class will grab a reference to a button component on this object, if it exists. ")]
    public Button disableButtonOnDrag;
    [Header("Momentum settings")]
    public bool useDrag = true;
    public float releaseDrag = 0.95f;
    public float speedMultOnRelease = 25f;

    protected bool isPressed = false;
    protected RectTransform myRectTransform;

    private Vector2 pressPosition;

    private float dragDelay = 0.1f;
    private float buttonDisable_Delay = 0.2f;
    private float buttonReEnableDelay = 0.1f;
    private float minDragDistanceToDisableButton = 20f;
    private Rigidbody2D myRb;
    private float timeWhenPressed;

    private const int prevPositionsBuffer = 3;

    private List<Vector3> prevPosition;

    private int draggingTouchIndex = -1;
	private Vector3 pressOffsetFromCenter;

    protected virtual void Awake() {
        if (disableButtonOnDrag == null) {
            disableButtonOnDrag = GetComponent<Button>();
        }
        myRb = GetComponent<Rigidbody2D>();
        prevPosition = new List<Vector3>();

        switch (dragAreaCollider) {
            case ColliderType.Circle2D:
                this.gameObject.AddOrGetComponent<CircleCollider2D>();
                break;
            case ColliderType.Box2D:
                this.gameObject.AddOrGetComponent<BoxCollider2D>();
                break;
        }
    }

    protected virtual void Start() {
        myRectTransform = GetComponent<RectTransform>();

        if (fitColliderToThisObj) {

            switch (dragAreaCollider) {
                case ColliderType.Circle2D:
                    this.gameObject.GetComponent<CircleCollider2D>().radius = myRectTransform.sizeDelta.x / 2;
                    break;
                case ColliderType.Box2D:
                    this.gameObject.GetComponent<BoxCollider2D>().size = myRectTransform.sizeDelta;
                    break;
            }
        }
        Camera.main.gameObject.GetOrAddComponent<Physics2DRaycaster>();
    }


    protected virtual void Update() {
		if (isPressed && DragTouchEnded ()) {
			PointerUp ();
		}

        if (DetectPointerDown()) {
            PointerDown();
        }

        if (isPressed) {
            if (Time.time - timeWhenPressed > buttonDisable_Delay) {
                disableButtonOnDrag.IfNotNull(b => b.interactable = false);
            }
            if ((myRb.position - pressPosition).magnitude > minDragDistanceToDisableButton) {
                disableButtonOnDrag.IfNotNull(b => b.interactable = false);
            }

            if (Time.time - timeWhenPressed > dragDelay) {
#if (UNITY_EDITOR || MOUSEINPUT)
				Vector3 position = (Vector3) Input.mousePosition;
				myRb.MovePosition(position - pressOffsetFromCenter);

#else
				if (InputEx.GetTouchById(draggingTouchIndex).HasValue) {
				Vector3 position = (Vector3) InputEx.GetTouchById(draggingTouchIndex).Value.position;
				myRb.MovePosition(position - pressOffsetFromCenter);
				}
#endif
            }
        }
        if (prevPosition.Count >= prevPositionsBuffer) {
            prevPosition.RemoveAt(0);
        }
        prevPosition.Add(this.transform.position);
        if (useDrag) {
            myRb.velocity *= releaseDrag;
        }
    }

    private bool DetectPointerDown() {
		Vector3 screenPoint = Vector3.one;
#if (UNITY_EDITOR || MOUSEINPUT)
		if(Input.GetMouseButtonDown (0)==false){ return false; }

		screenPoint = Input.mousePosition;

        var hits = Physics2D.RaycastAll(screenPoint, Camera.main.transform.forward, Mathf.Infinity);
		if(hits.Length > 0){
			foreach (RaycastHit2D hit in hits) {
				if (hit.collider.gameObject == this.gameObject) {
					return true;
				}
			}
		}
        return false;
#else
        foreach(Touch touch in Input.touches) {
			if(touch.phase != TouchPhase.Began) { continue; }
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
				foreach (RaycastHit2D hit in Physics2D.RaycastAll(touch.position, Camera.main.transform.forward, Mathf.Infinity)) {
                if (hit.collider.gameObject == this.gameObject) {
                    draggingTouchIndex = touch.fingerId;
                    return true;
                }
            }
        }
        return false;
#endif

    }

    public bool DragTouchEnded() {
#if (UNITY_EDITOR || MOUSEINPUT)
        return Input.GetMouseButton(0) == false;
#else
        try {
            Touch temp = Input.GetTouch(draggingTouchIndex);
        return false;
		} catch {
			return true;
		}
#endif
    }

    public void PointerDown() {
		pressPosition = myRb.position;
		timeWhenPressed = Time.time;
		isPressed = true;
#if (UNITY_EDITOR || MOUSEINPUT)
		pressOffsetFromCenter = (Input.mousePosition - this.transform.position);
#else
		pressOffsetFromCenter = (Vector3) InputEx.GetTouchById (draggingTouchIndex).Value.position - this.transform.position;
#endif
	}
    
	/*
	public void PointerDown() {
        pressPosition = myRb.position;
        timeWhenPressed = Time.time;
        isPressed = true;
        draggingTouchIndex = GetDraggingTouchIndex();
    }
	*/

    public void PointerUp() {
        isPressed = false;
        if (useDrag) {
            myRb.velocity = this.transform.position - prevPosition[0];
            myRb.velocity *= speedMultOnRelease;
        }
        this.StartCoroutineTimeline(
            this.WaitForSeconds_Coro(buttonReEnableDelay),
            this.ToIEnum(() => disableButtonOnDrag.Do(b => b.interactable = true))
        );
    }
}

