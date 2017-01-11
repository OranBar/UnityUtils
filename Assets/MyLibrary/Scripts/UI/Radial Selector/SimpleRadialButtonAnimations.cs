using UnityEngine;
using OranUnityUtils;
using System;

public class SimpleRadialButtonAnimations : MonoBehaviour, IRadialSelectorAnimations<TreeNodePI> {

    public AnimationCurve openButtonAnimationCurve;
    public float duration = 0.6f;

    private RadialButtonSelector<TreeNodePI> rbs;

    public void Awake() {
        rbs = GetComponent<RadialButtonSelector<TreeNodePI>>();
    }

    public void OnChildNodeClickAnimation(TreeNode<TreeNodePI> node) {
        MoveToCenter(node);
        MoveTo(node.Parent, rbs.ComputeAnchoredPosition(315, rbs.radius + rbs.NodeToButton[node.Parent].GetComponent<RectTransform>().sizeDelta.x/2f));

        RectTransform rootRectTranf = rbs.RootButton.GetComponent<RectTransform>();

        float scaleFactor = rbs.ChildButtons[0].GetComponent<RectTransform>().sizeDelta.x / rootRectTranf.sizeDelta.x;
        Vector3 newScale = rbs.ChildButtons[0].transform.localScale * scaleFactor;
        rbs.RootButton.GetMonoBehaviour().LerpLocalScale(newScale, duration);
    }

    public void OnCurrentNodeClickAnimation(TreeNode<TreeNodePI> node) {
        rbs.CloseView(OnSelectorClose_ChildNode);
    }

    public void OnParentNodeClickAnimation(TreeNode<TreeNodePI> node) {
        MoveToCenter(node);
        if (rbs.Tree.IsRoot(node)) {
            rbs.ParentButton.GetMonoBehaviour().LerpLocalScale(Vector3.one, duration);
        } 
    }

    private void MoveTo(TreeNode<TreeNodePI> node, Vector3 targetPosition) {
        float animDuration = 0.5f;
        Vector3 velocity = Vector3.zero;
        rbs.NodeToButton[node].GetOrAddComponent<MonoBehaviour>().SmoothstepMoveUI(targetPosition, animDuration,() => ResetSelector(node));
    }

    private void MoveToCenter(TreeNode<TreeNodePI> node) {
        MoveTo(node, Vector3.zero);
    }

    protected void ResetSelector(TreeNode<TreeNodePI> newCentralNode) {
        if (newCentralNode.Value.Equals(rbs.Tree.Root.Value) == false) {
            rbs.ShowNode(newCentralNode, MoveInstantly);
        } else {
            rbs.ShowRoot( MoveInstantly );
        }
    }

    public void OnSelectorOpen_ChildNode(GameObject go, Vector2 targetAnchoredPosition) {
        go.GetOrAddComponent<FadeInUIElement>().FadeIn(Math.Max(0, duration-0.1f));
        go.GetOrAddComponent<DummyMonoBehaviour>().MoveWithCurveUI(targetAnchoredPosition, duration, openButtonAnimationCurve);
    }

    public void OnSelectorClose_ChildNode(GameObject go, Vector2 targetAnchoredPosition) {
        go.GetOrAddComponent<FadeInUIElement>().FadeOut(Math.Max(0, duration - 0.1f));
        go.GetOrAddComponent<DummyMonoBehaviour>().SmoothstepMoveUI(Vector2.zero, duration, ()=> go.SetActive(false) );

    }

    private void MoveInstantly(GameObject go, Vector2 pos) {
        go.GetComponent<RectTransform>().anchoredPosition = pos;
    }
}

