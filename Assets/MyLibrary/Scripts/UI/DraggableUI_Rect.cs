using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using IMX.ExtensionMethods;
using System;

/**<summary>This class allows for a 2D object to become draggable using touch input</summary>
 * <author>Oran Bar</author>
 */
[RequireComponent(typeof(Rigidbody2D))]
public class DraggableUI_Rect: MonoBehaviour, IPointerDownHandler {
	
	[Tooltip("This parameter can be null")]
	public Button disableButtonOnDrag;
	[Header("Momentum settings")]
	public bool useDrag = true;
	public float releaseDrag = 0.9f;
	public float speedMultOnRelease = 5f;
	protected bool isPressed = false;
	protected RectTransform myRectTransform;

	private Vector2 pressPosition;

	private float dragDelay = 0.1f;
	private float buttonDisable_Delay = 0.2f;
	private float buttonReEnableDelay = 0.1f;
	private float minDragDistanceToDisableButton = 20f;
	private float timeWhenPressed;
	private Rigidbody2D myRb;
	private Vector3 prevPosition;

	private int draggingTouchIndex = -1;

	protected virtual void Awake() {
		myRb = GetComponent<Rigidbody2D>();
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

		if (useDrag) {
			myRb.velocity *= releaseDrag;
		}

		if (isPressed) {
			if (Time.time - timeWhenPressed > buttonDisable_Delay) {
				disableButtonOnDrag.IfNotNull(b => b.interactable = false);
			}
			if ((myRb.position - pressPosition).magnitude > minDragDistanceToDisableButton) {
				disableButtonOnDrag.IfNotNull(b => b.interactable = false);
			}

			if (Time.time - timeWhenPressed > dragDelay) {
				myRb.MovePosition(Input.GetTouch(draggingTouchIndex).position);
			}
		}
		
		prevPosition = this.transform.position;
		
	}

	public bool DragTouchEnded() {
		try{
			Touch temp = Input.GetTouch(draggingTouchIndex);
			return false;
		} catch {
			return true;
		}
	}

	public void OnPointerDown(PointerEventData data) {
		pressPosition = myRb.position;
		timeWhenPressed = Time.time;
		isPressed = true;
		draggingTouchIndex = data.pointerId;
	}

	public void PointerUp() {
		isPressed = false;
		if (useDrag) {
			myRb.velocity = this.transform.position - prevPosition;
			myRb.velocity *= speedMultOnRelease;
		}
		this.StartCoroutineTimeline(
			this.WaitForSeconds_Coro(buttonReEnableDelay),
			this.ToIEnum(() => disableButtonOnDrag.Do(b => b.interactable = true))
		);
	}
}

