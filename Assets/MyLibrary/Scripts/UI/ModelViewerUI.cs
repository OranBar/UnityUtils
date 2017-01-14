
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;


public class ModelViewerUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    private enum ModelViewState {
        Idle,
        Interacting
    };

    private ModelViewState state;

    private bool modelHandlingEnabled = true;

    public GameObject targetModel;
    public Camera targetCamera;

    private int firstPointerID = -2;
    private int secondPointerID = -2;
    public float degreesPerPixel = 0;

    public Vector2 lastDragDelta;
    private Vector2 lastDragPos;
    private float lastZAngle;
    public float lastZDeltaAngle;
    private const float DEG_PER_INCH = 20.0f;

    //zoom
    public bool twoFingersInput = true;
    public bool isZooming = false;
    public float zoomDelta = 0;
    private float zoomLastDistance = 0;
    private Vector3 zoomStartPos; //initial camera position when no zooming
    private Vector3 zoomDirection;
    private Vector3 zoomCurPos;
    private float zoomBeginTouchesDist; //touches distance when zoom begins
    private const float ZOOM_UNITS_PER_INCH = 1.0F;

    //smoothed cube rotation angles;
    private float smoothedXAngleVel = 0;
    private float smoothedYAngleVel = 0;
    private float smoothedZAngleVel = 0;

    public PreciseTap preciseTapScript;

    private float timeToIdle;
    private const float TIME_TO_IDLE = 0.5F;

    private float mousewheel = 0.0F;

    void Awake() {

        preciseTapScript = PreciseTap.AddPreciseTapComponent(this.gameObject);
        preciseTapScript.propagateClick = false;
        preciseTapScript.OnPreciseTap += OnPreciseTap;
    }

    // Use this for initialization
    void Start() {
        //degrees of rotation depends on screen pixel density
        degreesPerPixel = DEG_PER_INCH / Screen.dpi;

        /*
		//zoom camera starting position
		zoomStartPos = Camera.main.transform.localPosition;
		zoomCurPos = zoomStartPos;
		zoomDirection = Camera.main.transform.forward;
		*/
        state = ModelViewState.Idle;

        timeToIdle = TIME_TO_IDLE;
    }

    public void Init(Camera camera) {
        targetCamera = camera;
        zoomStartPos = targetCamera.transform.localPosition;
        zoomCurPos = zoomStartPos;
        zoomDirection = targetCamera.transform.forward;
    }

    // Update is called once per frame
    void Update() {
        if (targetCamera == null) {
            Debug.LogWarning("Target camera is null");
            return;
        }
        if (modelHandlingEnabled && targetModel == null) {
            Debug.LogWarning("Target model is null");
            return;
        }


        isZooming = false;

        //zoom by mouse wheel
        float mousewheel = Input.GetAxis("Mouse ScrollWheel");
        //mousewheel = 0f;
        if (mousewheel != 0f) {
            isZooming = true;
            zoomDelta = mousewheel;
        } else {
            zoomDelta = 0;
        }

        //if pointer is a mouse
        if (firstPointerID == -1) {
            lastDragDelta = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - lastDragPos;
            lastDragPos.x = Input.mousePosition.x;
            lastDragPos.y = Input.mousePosition.y;
        }
        //if we have two touches zoom, roll and drag
        else if (firstPointerID != -2 && secondPointerID != -2 && twoFingersInput) {
            Vector2 firstTouchPos = GetPositionById(firstPointerID);
            Vector2 secondTouchPos = GetPositionById(secondPointerID);

            //zoom
            isZooming = true;
            float currTouchesDist = (secondTouchPos - firstTouchPos).magnitude;
            float zoomUnits = currTouchesDist - zoomBeginTouchesDist;
            zoomUnits *= ZOOM_UNITS_PER_INCH / Screen.dpi;

            zoomDirection = targetCamera.transform.forward;
            print(zoomUnits);
            zoomCurPos = zoomStartPos + zoomUnits * zoomDirection;

            zoomDelta = (currTouchesDist - zoomLastDistance) * ZOOM_UNITS_PER_INCH / Screen.dpi;
            zoomLastDistance = currTouchesDist;

            Vector2 currDragPos = firstTouchPos + secondTouchPos;
            currDragPos /= 2;
            lastDragDelta = currDragPos - lastDragPos;
            lastDragPos = currDragPos;

            float currAngle = GetTwoFingerAxisAngle();

            lastZDeltaAngle = currAngle - lastZAngle;
            if (Mathf.Abs(lastZDeltaAngle) > 180.0F) {
                lastZDeltaAngle = lastZDeltaAngle - Mathf.Sign(lastZDeltaAngle) * 360.0F;
            }
            lastZAngle = currAngle;

        }
        //if we have one touch
        else if (firstPointerID >= 0) {
            Vector2 currDragPos = GetPositionById(firstPointerID);
            lastDragDelta = currDragPos - lastDragPos;
            lastDragPos = currDragPos;
        }


        if (!modelHandlingEnabled) {
            return;
        }

        switch (state) {
            case ModelViewState.Idle:

                float dt = Time.deltaTime;
                SmoothRotateModel(5.0F * dt, 1.5F * dt, 5.0F * dt, 3.0F);
                if (HasPointersDown()) {
                    state = ModelViewState.Interacting;
                }

                break;

            case ModelViewState.Interacting:
                //if there's one finger only damp rotation on z axis
                if (firstPointerID == -2 || secondPointerID == -2) {
                    lastZDeltaAngle = Mathf.Lerp(lastZDeltaAngle, 0, Time.deltaTime * 3.0F);
                }

                //if no pointers down (touches nor mouse)
                if (firstPointerID == -2 && secondPointerID == -2) {
                    //check idle time
                    timeToIdle -= Time.deltaTime;
                    if (timeToIdle < 0.0F) {
                        state = ModelViewState.Idle;
                    }

                } else {
                    timeToIdle = TIME_TO_IDLE;
                }

                if (firstPointerID == -2 && secondPointerID == -2) {
                    SmoothRotateModel(-lastDragDelta.y * degreesPerPixel, -lastDragDelta.x * degreesPerPixel, -lastZDeltaAngle, 0.5F);
                } else {
                    SmoothRotateModel(-lastDragDelta.y * degreesPerPixel, -lastDragDelta.x * degreesPerPixel, -lastZDeltaAngle, 20.0F);
                }

                //zoom
                if (twoFingersInput) {
                    if ((firstPointerID == -2 || secondPointerID == -2)) {
                        targetCamera.transform.localPosition = Vector3.Lerp(targetCamera.transform.localPosition, zoomStartPos, Time.deltaTime * 1.0F);
                    } else {
                        targetCamera.transform.position = zoomCurPos;
                    }
                }
                break;

            default:
                break;
        }
    }

    public void OnPointerDown(PointerEventData eventData) {

        //if we have two touches get medium point for dragging
        //and angle for z rotation
        if (firstPointerID > -2) {
            secondPointerID = eventData.pointerId;
            lastDragPos = GetPositionById(firstPointerID) + GetPositionById(secondPointerID);
            lastDragPos /= 2;

            lastZAngle = GetTwoFingerAxisAngle();

            //zoom beginning touches distance
            zoomBeginTouchesDist = (GetPositionById(secondPointerID) - GetPositionById(firstPointerID)).magnitude;
            zoomLastDistance = zoomBeginTouchesDist;
        } else {
            firstPointerID = eventData.pointerId;
            lastDragPos = eventData.position;
        }
    }

    public void OnPointerUp(PointerEventData eventData) {

        if (eventData.pointerId == firstPointerID) {
            //if we had a second touch make it primary (first)
            if (secondPointerID > -2) {
                lastDragPos = GetPositionById(secondPointerID);
                firstPointerID = secondPointerID;
                secondPointerID = -2;
            } else {
                firstPointerID = -2;
            }
        } else if (eventData.pointerId == secondPointerID) {
            lastDragPos = GetPositionById(firstPointerID);
            secondPointerID = -2;
        } else {
            firstPointerID = -2;
            secondPointerID = -2;
        }

        //reset zoom
        if (firstPointerID == -2 || secondPointerID == -2) {
            zoomCurPos = zoomStartPos;
        }

        if (firstPointerID == -2 && secondPointerID == -2) {
            lastDragDelta = new Vector2(0, 0);
        }
    }

    private void OnPreciseTap(TapData tapData) {

    }

    public bool HasPointersDown() {
        return preciseTapScript.HasPointersDown();
    }

    private void SmoothRotateModel(float xAngle, float yAngle, float zAngle, float lerpVel) {

        smoothedXAngleVel = Mathf.Lerp(smoothedXAngleVel, xAngle, Time.deltaTime * lerpVel);
        smoothedYAngleVel = Mathf.Lerp(smoothedYAngleVel, yAngle, Time.deltaTime * lerpVel);
        smoothedZAngleVel = Mathf.Lerp(smoothedZAngleVel, zAngle, Time.deltaTime * lerpVel);
        targetModel.transform.Rotate(smoothedXAngleVel, smoothedYAngleVel, smoothedZAngleVel, Space.World);
    }

    private Vector2 GetPositionById(int fingerId) {
        if (Input.touchCount < 1) {
            return new Vector2(0, 0);
        }

        for (int i = 0; i < Input.touchCount; i++) {
            if (fingerId == Input.GetTouch(i).fingerId) {
                return Input.GetTouch(i).position;
            }
        }

        return new Vector2(0, 0);
    }

    private float GetTwoFingerAxisAngle() {
        Vector2 Axis = GetPositionById(firstPointerID) - GetPositionById(secondPointerID);
        float Angle = Vector2.Angle(Axis, new Vector2(1, 0));
        if (Axis.y < 0) {
            Angle = 360 - Angle;
        }

        return Angle;
    }

    public void Reset() {
        lastDragDelta.x = lastDragDelta.y = 0;
        lastZDeltaAngle = 0;
        smoothedXAngleVel = smoothedYAngleVel = smoothedZAngleVel = 0;
    }

    public void DisableModelHandling() {
        modelHandlingEnabled = false;
    }

    public void EnableModelHandling() {
        modelHandlingEnabled = true;
    }

    public void Clear() {
        targetModel = null;
        targetCamera = null;
    }
}
