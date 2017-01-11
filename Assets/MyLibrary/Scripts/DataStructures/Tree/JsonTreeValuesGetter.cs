using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class JsonTreeValuesGetter_String : ATreeValuesGetter<string> {

    private Dictionary<string, string> nodesToParents;

    public JsonTreeValuesGetter_String(string jsonName) {
        nodesToParents = new Dictionary<string, string>();
        ReadJson(jsonName);
    }
    
    private void ReadJson(string jsonPath) {
        jsonPath = jsonPath.Replace(".json", "");

        if (File.Exists(jsonPath + ".json") == false) {
            Debug.LogError("Can't find Json file");
        }

        string root = jsonPath.Substring(jsonPath.LastIndexOf("/")+1);
        nodesToParents.Add(root, null);

        string jsonStr = File.ReadAllText(jsonPath + ".json");
        World_Json worldData = JsonUtility.FromJson<World_Json>(jsonStr);

        foreach(WorldCategory_Json worldCategory in worldData.categories) {
            string categoryName = worldCategory.categoryName;
            nodesToParents.Add(categoryName, root);

            foreach(Product_Json product in worldCategory.products) {
                nodesToParents.Add(product.name, categoryName);
            }
        }
    }
    
    protected override Dictionary<string, string> GetNodesMappedToParent() {
        return nodesToParents;
    }
}

