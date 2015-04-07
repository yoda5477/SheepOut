using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CheckBehavior : MonoBehaviour,IPointerDownHandler {
	public void OnPointerDown (PointerEventData eventData) {
		ManagerBehavior parent = (ManagerBehavior) transform.parent.GetComponent<MonoBehaviour>();
		parent.snap = Time.realtimeSinceStartup;
		Time.timeScale = 1-Time.timeScale;
	}

}
