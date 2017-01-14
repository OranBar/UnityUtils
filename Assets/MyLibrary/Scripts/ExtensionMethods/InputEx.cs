using UnityEngine;


public static class InputEx {

    public static Touch? GetTouchById(int fingerId) {
        for (int i = 0; i < Input.touchCount; i++) {
            if (fingerId == Input.GetTouch(i).fingerId) {
                return Input.GetTouch(i);
            }
        }
        return null;
    }

}
