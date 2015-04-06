using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SheepBehavior : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler {
	
	public GameObject dragLineProto;
	GameObject moveLine = null;
	float cross;
	float angle;
	Vector2 target = new Vector2 (0, 0);
	public int Force = 3;
	public float dist = 0;
	public float mult = 1;
	
	public void OnBeginDrag (PointerEventData eventData){
		//		Debug.Log ("MouseOnUnit.OnBeginDrag");
		if (moveLine == null) {
			moveLine = (GameObject) Instantiate (dragLineProto, transform.position, transform.rotation);
			moveLine.transform.SetParent(transform.parent);
		}
		//		Time.timeScale = 0;
		moveLine.transform.position = transform.position;
		moveLine.GetComponent<RectTransform> ().sizeDelta=new Vector2(moveLine.GetComponent<RectTransform> ().rect.width,100);
	}
	
	public void OnDrag (PointerEventData eventData){
		//		Debug.Log ("MouseOnUnit.OnDrag");
		target.x = eventData.position.x;
		target.y = eventData.position.y;
	}
	
	public void OnEndDrag (PointerEventData eventData){
		//		Debug.Log ("MouseOnUnit.OnEndDrag");
		target.x = eventData.position.x;
		target.y = eventData.position.y;
		//Time.timeScale = 1;
	}
	
	public void Update() {
		if (moveLine != null) {
			angle = Vector2.Angle (new Vector2 (0, 1), new Vector2 (target.x - transform.position.x, target.y - transform.position.y));
			cross = Mathf.Sign (Vector3.Cross (new Vector2 (0, 1), new Vector2 (target.x - transform.position.x, target.y - transform.position.y)).z);
			moveLine.GetComponent<RectTransform> ().sizeDelta = new Vector2 (moveLine.GetComponent<RectTransform> ().rect.width, Vector2.Distance (new Vector2 (target.x, target.y), new Vector2 (transform.position.x, transform.position.y)));
			moveLine.GetComponent<RectTransform> ().localEulerAngles = new Vector3 (0, 0, mult*transform.rotation.z+angle * cross);
		}
	}
	
	public void FixedUpdate() {
		if (moveLine != null) {
			moveLine.transform.position = transform.position;
			dist = Vector2.Distance (new Vector2 (target.x, target.y), new Vector2 (transform.position.x, transform.position.y));
			if ( dist < 10) {
//				moveLine=null;
			}
			else {
				this.GetComponent<Rigidbody2D> ().AddForce (new Vector2 ((target.x - transform.position.x)*Force, (target.y - transform.position.y)*Force),ForceMode2D.Impulse);
				//moveLine.GetComponent<RectTransform> ().localEulerAngles = new Vector3 (0, 0, angle * cross);
			}
		}
	}
}
