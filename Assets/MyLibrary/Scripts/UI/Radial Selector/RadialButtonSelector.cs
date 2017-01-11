using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using System;

public abstract class RadialButtonSelector<E> : MonoBehaviour {

    public float radius = 50f;
    public int maxActiveNodes = 12; //We can avoid this with better pooling mechanism that instantiates new objets if the current limit is surpassed. For now, the limit is hard set.
    public GameObject childPrefab, rootPrefab;

    public bool useSpringJoints = true;
   
    private int pooledNodes {
        get { return maxActiveNodes;}
        set { }
    }

    public Tree<E> Tree { get; protected set; }
    public Dictionary<TreeNode<E>, GameObject> NodeToButton { get; protected set; }
    public List<GameObject> ChildButtons { get; protected set; }
    public GameObject RootButton { get; protected set; }
    public GameObject ParentButton { get; protected set; }

    public TreeNode<E> CurrentMiddleNode { get; protected set; }

    protected RectTransform myRectTransform;
    
    private Transform linesContainer;

    protected abstract ATreeValuesGetter<E> TreeValuesGetter { get; set; }
    protected abstract IRadialSelectorAnimations<E> SelectorAnimator { get; set; }

    protected abstract string GetDisplayName(TreeNode<E> node);

    protected virtual void Start () {
        linesContainer = new GameObject("Lines Container").transform;

        myRectTransform = GetComponent<RectTransform>();
        NodeToButton = new Dictionary<TreeNode<E>, GameObject>();

        Tree = new Tree<E>(TreeValuesGetter);
        
        InitializeNodesPool(pooledNodes);
        
        childRadius = ChildButtons[0].GetComponent<RectTransform>().rect.width / 2;
        rootRadius = RootButton.GetComponent<RectTransform>().rect.width / 2;
        

        Debug.Log("childRadius " + childRadius + " rootRadius" + rootRadius);


        CloseView((go, v2) => { } );
        
    }

    protected virtual void Update() {
        DrawLinesFromChildrenToRoot();
    }
    //Used by DrawLinesFromChildrenToRoot for performance reasons
    private float childRadius = 0f;
    private float rootRadius = 0f;

    protected virtual void DrawLinesFromChildrenToRoot() {
        
        foreach (GameObject child in ChildButtons) {
            if(child.activeSelf == false) {
                continue;
            }

            RectTransform childRectTransf = child.GetComponent<RectTransform>();
            RectTransform centerNodeTransf = NodeToButton[CurrentMiddleNode].GetComponent<RectTransform>();
            UILineRendererDistanceBasedNoise lineRenderer = GetNodeLineRenderer(child);

            Vector2 lineDir = (Vector2)childRectTransf.InverseTransformPoint(centerNodeTransf.TransformPoint(centerNodeTransf.rect.center)) - childRectTransf.rect.center;
            lineDir = lineDir.normalized;
            
            Vector2 pointOnChild = childRectTransf.rect.center + lineDir * childRadius;
            Vector2 pointOnCenterNode = Vector2.zero;

            pointOnCenterNode = childRectTransf.InverseTransformPoint(centerNodeTransf.TransformPoint(centerNodeTransf.rect.center - lineDir * rootRadius));
            /*
            if (Tree.IsRoot(CurrentMiddleNode)) {
                pointOnCenterNode = childRectTransf.InverseTransformPoint(centerNodeTransf.TransformPoint(centerNodeTransf.rect.center - lineDir * rootRadius));
            } else {
                pointOnCenterNode = childRectTransf.InverseTransformPoint(centerNodeTransf.TransformPoint(centerNodeTransf.rect.center - lineDir * rootRadius));
            }
            */


            Vector2[] linePoints = new Vector2[] { pointOnCenterNode, pointOnChild };

            lineRenderer.SetLinePoints(linePoints);
            //This line controls the line renderers
            //lineRenderer.SetPointsWithNoise(linePoints, 6, 1);
            
        }
    }
    
    public void CloseView(Action<GameObject, Vector2> buttonsMoveAnimationFunc) {
        ChildButtons.ForEach(b => buttonsMoveAnimationFunc(b, Vector2.zero) );
        RootButton.SetActive(false);
        ParentButton.SetActive(false);
        NodeToButton.Clear();

        GameObject rootObj = CreateRootButton(Tree.Root);
        rootObj.GetComponent<Button>().onClick.AddListener(OpenView);
        CurrentMiddleNode = Tree.Root;
    }


    private void OpenView() {
        ShowRoot( SelectorAnimator.OnSelectorOpen_ChildNode );
    }

    private void InitializeNodesPool(int desiredPoolSize) {
        ChildButtons = new List<GameObject>();
        for (int i = 0; i < desiredPoolSize; i++) {

            GameObject newChildObj = Instantiate(childPrefab);
            RectTransform childRectTransf = newChildObj.GetOrAddComponent<RectTransform>();
            childRectTransf.SetParent(this.transform, false);
            newChildObj.name = "ChildNode_" + (i + 1);

            ChildButtons.Add(newChildObj);
            newChildObj.SetActive(false);
        }

        RootButton = Instantiate(rootPrefab);
        RectTransform rootRectTransf = RootButton.GetOrAddComponent<RectTransform>();
        rootRectTransf.SetParent(this.transform, false);
        RootButton.name = "RootNode";

        RootButton.SetActive(false);

        ParentButton = Instantiate(rootPrefab);
        RectTransform parentRectTransf = ParentButton.GetOrAddComponent<RectTransform>();
        parentRectTransf.SetParent(this.transform, false);
        ParentButton.name = "ParentNode";
        

        float scaleFactor = ChildButtons[0].GetComponent<RectTransform>().sizeDelta.x / rootRectTransf.sizeDelta.x;
        Vector3 newScale = ChildButtons[0].transform.localScale * scaleFactor;
        ParentButton.transform.localScale = newScale;
        
        ParentButton.SetActive(false);
    }

    private GameObject GetChildButtonFromPool() {
        foreach (GameObject go in ChildButtons) {
            if(go.activeSelf == false) {
                go.GetComponent<Button>().interactable = true;
                go.SetActive(true);
                return go;
            }
        }
        return null;
    }

    private UILineRendererDistanceBasedNoise GetNodeLineRenderer(GameObject obj) {
        return obj.GetComponentInChildren<UILineRendererDistanceBasedNoise>();
    }
    
    public GameObject GetRootButton() {
        RootButton.GetComponent<Button>().interactable = true;
        RootButton.SetActive(true);
        return RootButton;
    }

    public GameObject GetParentButton() {
        ParentButton.GetComponent<Button>().interactable = true;
        ParentButton.SetActive(true);
        return ParentButton;
    }

    private GameObject CreateNewButton(TreeNode<E> node) {
        GameObject button = GetChildButtonFromPool();
        NodeToButton.Add(node, button);

        RectTransform childRectTransf = button.GetOrAddComponent<RectTransform>();
        childRectTransf.anchoredPosition = Vector2.zero;
        Button centerButton = button.GetComponent<Button>();
        centerButton.GetComponentInChildren<Text>().text = GetDisplayName(node);
        centerButton.onClick.RemoveAllListeners();

        return button;
    }

    private GameObject CreateRootButton(TreeNode<E> node) {
        GameObject button = GetRootButton();
        NodeToButton.Add(node, button);

        RectTransform rootRectTransf = button.GetOrAddComponent<RectTransform>();
        rootRectTransf.anchoredPosition = Vector2.zero;

        rootRectTransf.localScale = Vector3.one;
        
        Button rootButton = button.GetComponent<Button>();
        rootButton.GetComponentInChildren<Text>().text = GetDisplayName(node);
        rootButton.onClick.RemoveAllListeners();

        return button;
    }

    private GameObject CreateParentButton(TreeNode<E> node) {
        GameObject button = GetParentButton();
        NodeToButton.Add(node, button);

        RectTransform rootRectTransf = button.GetOrAddComponent<RectTransform>();
        rootRectTransf.anchoredPosition = Vector2.zero;

        //Rescaling Parent Button to size of Child Button
        float scaleFactor = ChildButtons[0].GetComponent<RectTransform>().sizeDelta.x / rootRectTransf.sizeDelta.x;
        Vector3 newScale = ChildButtons[0].transform.localScale * scaleFactor;
        ParentButton.transform.localScale = newScale;

        Button rootButton = button.GetComponent<Button>();
        rootButton.GetComponentInChildren<Text>().text = GetDisplayName(node);
        rootButton.onClick.RemoveAllListeners();

        return button;
    }

    public void ShowRoot(Action<GameObject, Vector2> buttonsMoveAnimationFunc) {
        ChildButtons.ForEach(b => b.SetActive(false));
        RootButton.SetActive(false);
        ParentButton.SetActive(false);
        NodeToButton.Clear();
        if (useSpringJoints) {
            ChildButtons.ForEach(b => b.GetComponent<TargetJoint2D>().enabled = false);
        }

        ArrangeNodes(Tree.Root.Children, radius, 0, 330, buttonsMoveAnimationFunc);

        GameObject centerNodeGO = CreateRootButton(Tree.Root);
        centerNodeGO.GetComponent<Button>().onClick.AddListener(() => OnRootNodeClick(Tree.Root));
        CurrentMiddleNode = Tree.Root;
    }

    public void ShowNode(TreeNode<E> centralNode, Action<GameObject, Vector2> buttonsMoveAnimationFunc) {
        ChildButtons.ForEach(b => b.SetActive(false));
        RootButton.SetActive(false);
        ParentButton.SetActive(false);
        NodeToButton.Clear();
        if (useSpringJoints) {
            ChildButtons.ForEach(b => b.GetComponent<TargetJoint2D>().enabled = false);
        }

        ArrangeNodes(centralNode.Children, radius, 0, 270);
        
        GameObject centerNodeGO = CreateRootButton(centralNode);
        centerNodeGO.GetComponent<Button>().onClick.AddListener( () => OnCenterNodeClick(centralNode) );

        GameObject parentNodeGO = CreateParentButton(centralNode.Parent);
        RectTransform parentRectTransf = parentNodeGO.GetComponent<RectTransform>();
        parentRectTransf.anchoredPosition = ComputeAnchoredPosition(315, radius+parentRectTransf.sizeDelta.x/2);
        parentNodeGO.GetComponent<Button>().onClick.AddListener(() => OnParentNodeClick(centralNode.Parent) );
        CurrentMiddleNode = centralNode;
    }

    /** <summary> Arranges the nodes radially around the center of this gameobject.
     * The angle is the same as a carthesian plane: 0 and 360 are on the middle right, </summary>
     */
    private void ArrangeNodes(HashSet<TreeNode<E>> nodes, float radius, int minAngle, int maxAngle) {
        ArrangeNodes(nodes, radius, minAngle, maxAngle, (go, pos) => go.GetComponent<RectTransform>().anchoredPosition = pos);
    }

    private void ArrangeNodes(HashSet<TreeNode<E>> nodes, float radius, int minAngle, int maxAngle, Action<GameObject, Vector2> moveAnimationFunc) {
        if (minAngle < 0 || minAngle > 360) {
            Debug.LogError("Invalid MinAngle: angles have to be between 0 and 360");
            return;
        }
        if (maxAngle < 0 || maxAngle > 360) {
            Debug.LogError("Invalid MaxAngle: angles have to be between 0 and 360");
            return;
        }
        if (minAngle > maxAngle) {
            Debug.LogError("MinAngle is greater than MaxAngle");
            return;
        }
        if (nodes.Count == 0) {
            Debug.Log("This node has no children");
            return;
        }

        int noOfChildren = nodes.Count;
        float angleBetweenEntries = (maxAngle - minAngle) / (noOfChildren-1.1f);

        int i = 0;
        foreach (TreeNode<E> c in nodes) {
            //No, I'm not crazy.
            //I do this because if I try to pass child to the AddListener lambda, it will put the last enumerated value in all of the lambdas
            TreeNode<E> child = c;
            
            GameObject newChildObj = CreateNewButton(child);
            
            Vector2 nextPosition = ComputeAnchoredPosition(angleBetweenEntries * i, radius);
            moveAnimationFunc(newChildObj, nextPosition);

            if (useSpringJoints) {
                TargetJoint2D joint = newChildObj.AddOrGetComponent<TargetJoint2D>();
                joint.enabled = true;
                joint.target = myRectTransform.TransformPoint(nextPosition);
                //newChildObj.GetComponent<TargetJoint2D_Local>().SetTargetInLocalPosition(nextPosition);
            }

            Button button = newChildObj.GetComponent<Button>();
            
            //Leaf node
            if (child.Children.Count == 0) {
                button.onClick.AddListener(() => OnLeafNodeClick(child));
            } else {
                button.onClick.AddListener(() => OnChildNodeClick(child));
            }

            i++;
        }
    }

    public Vector2 ComputeAnchoredPosition(float angleInDegrees, float radius) {
        float angle = angleInDegrees * Mathf.Deg2Rad;

        Vector2 position = new Vector2(-1, -1);
        position.x = radius * Mathf.Cos(angle);
        position.y = radius * Mathf.Sin(angle);

        return position;
    }

    public void OnRootNodeClick(TreeNode<E> rootNode) {
        SelectorAnimator.OnCurrentNodeClickAnimation(rootNode);
    }

    public virtual void OnLeafNodeClick(TreeNode<E> leafNode) {
        SelectorAnimator.OnChildNodeClickAnimation(leafNode);
    }

    public void OnChildNodeClick(TreeNode<E> childNode) {
        SelectorAnimator.OnChildNodeClickAnimation(childNode);
    }
    
    public void OnCenterNodeClick(TreeNode<E> centerNode) {
        SelectorAnimator.OnCurrentNodeClickAnimation(centerNode);
    }

    public void OnParentNodeClick(TreeNode<E> parentNode) {
        SelectorAnimator.OnParentNodeClickAnimation(parentNode);
    }

    public override string ToString() {
       string result = "Nodes are\n";
       Tree.Root.Children.ToList().ForEach( c => result += c+" - "  );
       return result;
    }
    
}
