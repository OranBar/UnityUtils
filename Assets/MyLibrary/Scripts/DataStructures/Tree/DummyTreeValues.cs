using System;
using System.Collections.Generic;
using System.Linq;

public class DummyTreeValues : ATreeValuesGetter<string> {

    protected override Dictionary<string, string> GetNodesMappedToParent() {
        Dictionary<string, string> result = new Dictionary<string, string>();

        string rootValue = "GengisKhan";
        string[] firstLevel = new string[] { "Pen", "Nobody", "Pancake", "ChangeMe3", "GreenFox", "Jambalaya" };
        string[] secondLevel_1stLeaf = new string[] { "Casual_1", "FlippinTables_1", "RedHead_1", "Double_1" };
        string[] secondLevel_2ndLeaf = new string[] { "Casual_2", "FlippinTables_2", "RedHead_2", "Double_2" };
        string[] secondLevel_3rdLeaf = new string[] { "Casual_3", "FlippinTables_3", "RedHead_3", "Double_3" };
        string[] secondLevel_4thLeaf = new string[] { "Casual_4", "FlippinTables_4", "RedHead_4", "Double_4" };
        string[] secondLevel_5thLeaf = new string[] { "Casual_5", "FlippinTables_5", "RedHead_5", "Double_5" };
        string[] secondLevel_6thLeaf = new string[] { "Casual_6", "FlippinTables_6", "RedHead_6", "Double_6" };

        result.Add(rootValue, null);
        firstLevel.ToList().ForEach( e => result.Add(e, rootValue) );
        secondLevel_1stLeaf.ToList().ForEach( e => result.Add(e, firstLevel[0]) );
        secondLevel_2ndLeaf.ToList().ForEach(e => result.Add(e, firstLevel[1]));
        secondLevel_3rdLeaf.ToList().ForEach(e => result.Add(e, firstLevel[2]));
        secondLevel_4thLeaf.ToList().ForEach(e => result.Add(e, firstLevel[3]));
        secondLevel_5thLeaf.ToList().ForEach(e => result.Add(e, firstLevel[4]));
        secondLevel_6thLeaf.ToList().ForEach(e => result.Add(e, firstLevel[5]));
        
        return result;
    }

}
    
