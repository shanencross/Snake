using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEvents : MonoBehaviour, ISelectHandler, ISubmitHandler {
	public void OnSelect(BaseEventData eventData) {
		if (MenuManager.instance.firstSelectionDone)
			SoundManager.instance.PlaySound("menuSelect");
	}

	public void OnSubmit(BaseEventData eventData) {
		SoundManager.instance.PlaySound("menuSubmit");
	}
}
