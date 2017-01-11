using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class PointerEventsPropagator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public bool propagatePointerDown = true, propagatePointerUp = true;

    public void OnPointerDown(PointerEventData eventData) {

        if (propagatePointerDown) {
            ExecuteEvents.ExecuteHierarchy(transform.parent.gameObject, eventData, ExecuteEvents.pointerDownHandler); 
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (propagatePointerUp) {
            ExecuteEvents.ExecuteHierarchy(transform.parent.gameObject, eventData, ExecuteEvents.pointerUpHandler);

        }
    }
}
