using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;


public class TapData {

    public GameObject tappedObj;
    public PointerEventData eventData;

    public TapData(GameObject go, PointerEventData pointerData) {
        tappedObj = go;
        eventData = pointerData;
    }

    public Vector2 GetPosition() {
        return eventData.position;
    }
}

public class PreciseTap : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public bool enableLog = false;
    public bool propagateClick = true;
    public delegate void PreciseTapAction(TapData tapData);
    public event PreciseTapAction OnPreciseTap;

    //pointers eligible for precise tap
    private Dictionary<int, float> eligibleForTap = new Dictionary<int, float>();
    private const float MAX_TAP_DURATION = 0.25f;
    private const float TAP_COOLDOWN = 0.3f;
    private const float maximumAllowedDistance = 0.01f;
    private float lastTapTime = 0.0F;

    public static PreciseTap AddPreciseTapComponent(GameObject go, bool propagateClick = false) {
        PreciseTap tapScript = go.GetComponent<PreciseTap>();
        if (tapScript == null) {
            tapScript = go.AddComponent<PreciseTap>();
        }
        tapScript.propagateClick = propagateClick;
        return tapScript;
    }

    public void OnPointerDown(PointerEventData eventData) {

        if (enableLog)
            Debug.Log("PT -> Pointer Down");

        if (propagateClick) {
            if (transform.parent != null) {
                ExecuteEvents.ExecuteHierarchy(transform.parent.gameObject, eventData, ExecuteEvents.pointerDownHandler);
            }
        }

        //make pointer eligible for tap
        eligibleForTap.Remove(eventData.pointerId);
        eligibleForTap.Add(eventData.pointerId, Time.time);

    }

    public void OnPointerUp(PointerEventData eventData) {
        if (enableLog)
            Debug.Log("PT -> Pointer Up");

        if (propagateClick) {
            ExecuteEvents.ExecuteHierarchy(transform.parent.gameObject, eventData, ExecuteEvents.pointerUpHandler);
        }
        //check if it's a precise tap
        float tapDuration;
        if (eligibleForTap.TryGetValue(eventData.pointerId, out tapDuration)) {
            eligibleForTap.Remove(eventData.pointerId);
            tapDuration = Time.time - tapDuration;

            if (tapDuration < MAX_TAP_DURATION &&
                (eventData.delta.magnitude < maximumAllowedDistance * Screen.dpi) &&
                (Time.time - lastTapTime) > TAP_COOLDOWN) {
                //here we have a precise tap
                if (enableLog)
                    Debug.Log("PT -> TAP!!");
                TapData tapData = new TapData(this.gameObject, eventData);
                if (OnPreciseTap != null) {
                    OnPreciseTap(tapData);
                }

                lastTapTime = Time.time;
            }
        }
    }

    public bool HasPointersDown() {
        return eligibleForTap.Count > 0;
    }
}

