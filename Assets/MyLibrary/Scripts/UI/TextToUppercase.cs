using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextToUppercase : MonoBehaviour {

    private Text myText;

    private string lastframeText;

	// Use this for initialization
	void Start () {
        myText = GetComponent<Text>();
        lastframeText = "";
    }
	
	// Update is called once per frame
	void Update () {
        if (myText.text.Equals(lastframeText) == false) {
            myText.text = myText.text.ToUpper();
        }
        lastframeText = myText.text;
    }
}
