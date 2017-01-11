using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IRadialSelectorAnimations<E> {

    void OnChildNodeClickAnimation(TreeNode<E> node);
    void OnParentNodeClickAnimation(TreeNode<E> node);
    void OnCurrentNodeClickAnimation(TreeNode<E> node);
    void OnSelectorOpen_ChildNode(GameObject go, Vector2 targetAnchoredPosition);
    void OnSelectorClose_ChildNode(GameObject go, Vector2 targetAnchoredPosition);

}

