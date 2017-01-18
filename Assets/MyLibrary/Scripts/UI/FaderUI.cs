using UnityEngine;
using System.Collections;
using OranUnityUtils;

/**<summary>Add this behaviour to a GameObject (via inspector or AddComponent), then call FadeIn and FadeOut </summary>
 * 
 */
public class FaderUI : MonoBehaviour {

    private CanvasGroup myCanvasGroup;

    void Awake() {
        myCanvasGroup = gameObject.GetOrAddComponent<CanvasGroup>();

        if (gameObject.GetComponent<RectTransform>() == null) {
            Debug.LogWarning("Can't add a canvas group on this GameObject. FadeInUIElement only works on UI elements");
            this.enabled = false;
        }
        if (gameObject.GetComponents<FaderUI>().Length > 1) {
            Debug.LogWarning("Only one instance of FadeInUiElement is allowed per single GameObject. I'll go ahead and clean your mess by destroying the duplicate");
            Destroy(this);
        }
    }

    public void FadeIn(float duration = 0.3f, float delay = 0f) {
        this.StartCoroutineTimeline(
            this.WaitForSeconds_Coro(delay),
            FadeIn_Coro(duration)
        );
    }

    public void FadeOut(float duration = 0.3f, float delay = 0f) {
        this.StartCoroutineTimeline(
            this.WaitForSeconds_Coro(delay),
            FadeOut_Coro(duration)
        );
    }

    public IEnumerator FadeIn_Coro(float duration = 0.3f) {
        myCanvasGroup.alpha = 0f;
        float oneSecondIncrement = 1 / (duration);
        while (myCanvasGroup.alpha < 1) {
            myCanvasGroup.alpha += oneSecondIncrement * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    public IEnumerator FadeOut_Coro(float duration = 0.3f) {
        myCanvasGroup.alpha = 1f;
        float oneSecondIncrement = 1 / (duration);
        while (myCanvasGroup.alpha > 0) {
            myCanvasGroup.alpha -= oneSecondIncrement * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    public void SetAlpha(float alpha) {
        myCanvasGroup.alpha = alpha;
    }

}
