using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CooldownButton : MonoBehaviour {

    public float cooldown = 0.5f;
    private Button myButton;
    

	void Start () {
        myButton = this.GetComponent<Button>();
        myButton.onClick.AddListener(ButtonCooldown);	
	}

    private void ButtonCooldown() {
        StartCoroutine(ButtonCooldown_Routine());
    }

    private IEnumerator ButtonCooldown_Routine() {
        myButton.interactable = false;
        yield return new WaitForSeconds(cooldown);
        myButton.interactable = true;
	}
}
