using System.Collections.Generic;
using System.Linq;

public abstract class ATreeValuesGetter<E> {
    
    protected abstract Dictionary<E, E> GetNodesMappedToParent();

    public E GetRoot() {
        return GetNodesValues().First(v => GetParentValue(v) == null);
    }

    public List<E> GetNodesValues() {
        return GetNodesMappedToParent().Keys.ToList();
    }
    
    public E GetParentValue(E value) {
        return GetNodesMappedToParent()[value];
    }

}

