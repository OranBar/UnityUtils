using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using OranUnityUtils;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class DraggableUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    [Tooltip("The class will work fine if the parameter is null. If this parameter is null, the class will grab a reference to a button component on this object, if it exists. ")]
    public Button disableButtonWhenDragging;
    public float dragDelay = 0.1f;
    public float buttonDisable_Delay = 0.2f;
    public float buttonReEnableDelay = 0.1f;
    public float minDragDistanceToDisableButton = 20f;
    public bool fitColliderToThisObj = true;
    [Tooltip("Momentum settings")]
    public bool useInertia = true;
    public float inertia;
    
    protected bool isPressed;
    protected RectTransform myRectTransform;

    private Vector2 pressPosition;

    private float timeWhenPressed;
    private Rigidbody2D myRb;
    private Vector3 prevPosition;
    
    protected virtual void Start() {
        myRectTransform = GetComponent<RectTransform>();
        if (fitColliderToThisObj) {
            GetComponent<CircleCollider2D>().radius = myRectTransform.sizeDelta.x/2;
        }
        disableButtonWhenDragging = GetComponent<Button>();
        myRb = GetComponent<Rigidbody2D>();
    }


    protected virtual void Update() {
        if (isPressed) {
            if(Time.time - timeWhenPressed > buttonDisable_Delay) {
                disableButtonWhenDragging.IfNotNull(b=>b.interactable = false);
            }
            if((myRb.position - pressPosition).magnitude > minDragDistanceToDisableButton) {
                disableButtonWhenDragging.IfNotNull(b => b.interactable = false);
            }

            if (Time.time - timeWhenPressed > dragDelay) {
                myRb.MovePosition(Input.mousePosition);
            }
        }
        prevPosition = this.transform.position;
        if (useInertia) {
            myRb.velocity *= inertia;
        }
    }

    public void OnPointerDown(PointerEventData data) {
        pressPosition = myRb.position;
        timeWhenPressed = Time.time;
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData data) {
        isPressed = false;
        if (useInertia) {
            myRb.velocity = this.transform.position - prevPosition;
        }
        this.StartCoroutineTimeline(
            this.WaitForSeconds_Coro(buttonReEnableDelay),
            this.ToIEnum(() => disableButtonWhenDragging.Do(b => b.interactable = true))
        );
    }  
}