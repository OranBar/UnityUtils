//Uncommenting this line will make the class work with mouse as opposed to touches
//#define MOUSEINPUT

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using OranUnityUtils;

[RequireComponent(typeof(Rigidbody2D))]
public class DraggableUI_Rect: MonoBehaviour, IPointerDownHandler {
	
	[Tooltip("The class will work fine if the parameter is null. If this parameter is null, the class will grab a reference to a button component on this object, if it exists. ")]
	public Button disableButtonOnDrag;
	[Header("Momentum settings")]
	public bool useDrag = true;
	public float releaseDrag = 0.9f;
	public float speedMultOnRelease = 3f;
	protected bool isPressed = false;
	protected RectTransform myRectTransform;

	private Vector2 pressPosition;

	private float dragDelay = 0.1f;
	private float buttonDisable_Delay = 0.2f;
	private float buttonReEnableDelay = 0.1f;
	private float minDragDistanceToDisableButton = 20f;
	private float timeWhenPressed;
	private Rigidbody2D myRb;
	private List<Vector3> prevPosition;

    private const int prevPositionsBuffer = 3;

	private int draggingTouchIndex = -1;
	private Vector3 pressOffsetFromCenter;


	protected virtual void Awake() {
		if (disableButtonOnDrag == null) {
			disableButtonOnDrag = GetComponent<Button>();
		}
		myRb = GetComponent<Rigidbody2D>();
        prevPosition = new List<Vector3>();
    }

	protected virtual void Start() {
		myRectTransform = GetComponent<RectTransform>();
		if(GetComponent <Graphic>()==null){
			var image = this.gameObject.AddComponent <Image>();
			image.color = new Color(0,0,0,0);
		}
	}


	protected virtual void Update() {
		if (isPressed && DragTouchEnded ()) {
			PointerUp ();
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
        if(prevPosition.Count >= prevPositionsBuffer) {
            prevPosition.RemoveAt(0);
        }
		prevPosition.Add(this.transform.position);
		if (useDrag) {
			myRb.velocity *= releaseDrag;
		}
	}

	public bool DragTouchEnded() {
#if (UNITY_EDITOR || MOUSEINPUT)
        return Input.GetMouseButton(0)==false;
#else
        try {
            Touch temp = Input.GetTouch(draggingTouchIndex);
        	return false;
		} catch {
			return true;
		}
#endif
    }

    public void OnPointerDown(PointerEventData data) {
		pressPosition = myRb.position;
		timeWhenPressed = Time.time;
		isPressed = true;
		draggingTouchIndex = data.pointerId;
#if (UNITY_EDITOR || MOUSEINPUT)
		pressOffsetFromCenter = (Input.mousePosition - this.transform.position);
#else
		pressOffsetFromCenter = (Vector3) InputEx.GetTouchById (draggingTouchIndex).Value.position - this.transform.position;
#endif
	}

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

