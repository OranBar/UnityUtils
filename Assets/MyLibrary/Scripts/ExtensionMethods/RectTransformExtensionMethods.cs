using UnityEngine;

namespace OranUnityUtils
{
    public static class RectTransformExtensionMethods {
        
        public static Rect RectTransformToScreenSpace(this RectTransform transform) {
            Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
            Rect rect = new Rect(transform.position.x, transform.position.y, size.x, size.y);
            rect.x -= (transform.pivot.x * size.x);
            rect.y -= (transform.pivot.y * size.y);
            return rect;
        }
        public static Vector2 CenterToScreenSpace(this RectTransform rectTransf) {
            return rectTransf.RectTransformToScreenSpace().center;
        }
    }
}
