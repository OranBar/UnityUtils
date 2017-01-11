using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using OranUnityUtils;
using UnityEngine.UI;

public class RadialButtonSelector_PI : RadialButtonSelector<TreeNodePI> {

    public GameObject productInfographicPrefab;

    protected override void Start() {
        base.Start();
        GetComponent<DraggableUI>().disableButtonWhenDragging = this.RootButton.GetComponent<Button>();
    }

    protected override ATreeValuesGetter<TreeNodePI> TreeValuesGetter {
        get { return new DirectoryDataGetter(Constants.PATH_DATA_DIR); }
        set { throw new NotImplementedException(); }
    }

    protected override IRadialSelectorAnimations<TreeNodePI> SelectorAnimator {
        get { return this.GetComponent<IRadialSelectorAnimations<TreeNodePI>>(); }
        set { throw new NotImplementedException(); }
    }

    protected override string GetDisplayName(TreeNode<TreeNodePI> node) {
        //TODO: if the naming convention of the folders change, this method won't work. Just replace with node.Value.Name.
        return new string(node.Value.Name.ToCharArray().ToList()
            .SkipWhile(c=>c!='-')   
            .Skip(1)
            .ToArray()
            );
    }

    public override void OnLeafNodeClick(TreeNode<TreeNodePI> leaf) {
        StartCoroutine(OnLeafNodeClick_Coro(leaf));
    }
    
    private IEnumerator OnLeafNodeClick_Coro(TreeNode<TreeNodePI> leaf) {
        PIData productData = leaf.Value.GetProductData();

        GameObject newPI = Instantiate(productInfographicPrefab);

        yield return null;

        newPI.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform, false);
        newPI.GetComponent<ProductInfographic>().SetProduct(productData);
        newPI.GetComponent<ProductInfographic>().OnDestroyEvent += () => {
            this.gameObject.GetOrAddComponent<FadeInUIElement>().FadeIn();
            this.ToIEnum(() => buttonsToEnableWhenFadingBackIn.ForEach(b => b.SetActive(true)));
        };

        this.StartCoroutineTimeline(
            this.gameObject.GetOrAddComponent<FadeInUIElement>().FadeOut_Coro(),
            this.ToIEnum(() => DisableAllChildren(this.gameObject))
        );
        this.gameObject.GetOrAddComponent<FadeInUIElement>().FadeOut();
        
        yield return null;
    }

    private List<GameObject> buttonsToEnableWhenFadingBackIn;


    private void DisableAllChildren(GameObject go) {
        buttonsToEnableWhenFadingBackIn = new List<GameObject>();
        foreach (Transform t in go.transform) {
            buttonsToEnableWhenFadingBackIn.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }
    }

   
}
