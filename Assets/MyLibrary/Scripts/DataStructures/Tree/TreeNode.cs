using System.Collections.Generic;


public class TreeNode<E> {

    public static int nodeId = 0;
    
    public E Value { get; set; }
    public TreeNode<E> Parent { get; set; }
    public HashSet<TreeNode<E>> Children { get; private set; }
    public int Id { get; protected set; }


    public TreeNode() {
        this.Value = default(E);
        this.Parent = null;
    }

    public TreeNode(E value) {
        this.Id = nodeId++; 
        this.Value = value;
        this.Parent = null;
        this.Children = new HashSet<TreeNode<E>>();
    }

    public TreeNode(E value, TreeNode<E> parent){
        this.Value = value;
        this.Children = new HashSet<TreeNode<E>>();
        this.Parent = parent;
        if(parent != null) {
            parent.AddChild(this);
        }
    }

    public void AddChild(TreeNode<E> child) {
        this.Children.Add(child);
    }

    public void AddChildren(HashSet<TreeNode<E>> newChildren) {
        this.Children.UnionWith(newChildren);
    }

    public void RemoveChild(TreeNode<E> child) {
        this.Children.Remove(child);
    }

    public void RemoveChildren(List<TreeNode<E>> childrenToRemove) {
        foreach(TreeNode<E> node in childrenToRemove){
            this.Children.Remove(node);
        }
    }

    public override string ToString() {
        return "Node {Value: "+Value+" - Parent "+Parent.Value+"}";
    }

    public override bool Equals(object obj) {
        TreeNode<E> node = obj as TreeNode<E>;
        if(node != null) {
            return this.Equals(node);
        } else {
            return false;
        }
    }

    public bool Equals(TreeNode<E> other) {
        return this.Value.Equals(other.Value);
        /*
        if (this.Id == other.Id) {
            return true;
        } else {
            return false;
        }
        */
    }

    /*
    public override int GetHashCode() {
        return 17 * Id.GetHashCode();
    }
    */

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public static bool operator ==(TreeNode<E> a, TreeNode<E> b) {
        // If both are null, or both are same instance, return true.
        if (System.Object.ReferenceEquals(a, b)) {
            return true;
        }

        // If one is null, but not both, return false.
        if (((object)a == null) || ((object)b == null)) {
            return false;
        }

        // Return true if the fields match:
        return a.Equals(b);
    }

    public static bool operator !=(TreeNode<E> a, TreeNode<E> b) {
        return !(a == b);
    }
}
