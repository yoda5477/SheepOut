using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SheepBehavior : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
	
	public GameObject dragLineProto;
	GameObject moveLine = null;
	public float cross;
	public float angle;
	Vector2 target = new Vector2 (0, 0);
	public float Force = 3;
	public float Torque = 3;
	public float MaxTorque = 200;
	public float MaxAmort = 10;
	public float dist = 0;
	public float rotToDo = 0;
	public float amort;
	public float newTorque;
	public Vector2 newForce;

	public void OnBeginDrag (PointerEventData eventData)
	{
		//		Debug.Log ("MouseOnUnit.OnBeginDrag");
		if (moveLine == null) {
			moveLine = (GameObject)Instantiate (dragLineProto, transform.position, transform.rotation);
			moveLine.transform.SetParent (transform.parent);
		}
		//		Time.timeScale = 0;
		moveLine.transform.position = transform.position;
		moveLine.GetComponent<RectTransform> ().sizeDelta = new Vector2 (moveLine.GetComponent<RectTransform> ().rect.width, 100);
	}
	
	public void OnDrag (PointerEventData eventData)
	{
		//		Debug.Log ("MouseOnUnit.OnDrag");
		target.x = eventData.position.x;
		target.y = eventData.position.y;
	}
	
	public void OnEndDrag (PointerEventData eventData)
	{
		//		Debug.Log ("MouseOnUnit.OnEndDrag");
		target.x = eventData.position.x;
		target.y = eventData.position.y;
		//Time.timeScale = 1;
	}
	
	public void Update ()
	{
		if (moveLine != null) {
			angle = Vector2.Angle (new Vector2 (0, 1), new Vector2 (target.x - transform.position.x, target.y - transform.position.y));
			cross = Mathf.Sign (Vector3.Cross (new Vector2 (0, 1), new Vector2 (target.x - transform.position.x, target.y - transform.position.y)).z);
			moveLine.GetComponent<RectTransform> ().sizeDelta = new Vector2 (moveLine.GetComponent<RectTransform> ().rect.width, Vector2.Distance (new Vector2 (target.x, target.y), new Vector2 (transform.position.x, transform.position.y)));
			moveLine.GetComponent<RectTransform> ().localEulerAngles = new Vector3 (0, 0, transform.rotation.z + angle * cross);
			rotToDo = Mathf.DeltaAngle (transform.rotation.eulerAngles.z, angle * cross);
		}
	}
	
	public void FixedUpdate ()
	{
		if (moveLine != null) {
			moveLine.transform.position = transform.position;
			dist = Vector2.Distance (new Vector2 (target.x, target.y), new Vector2 (transform.position.x, transform.position.y));
			if (dist < 10) {
//				moveLine=null;
			} else {
				if (rotToDo > 0) {
					newTorque = Mathf.Min (MaxTorque, rotToDo * Torque);
				} else {
					newTorque = Mathf.Max (-MaxTorque, rotToDo * Torque);
				}
				amort = Mathf.Min(Mathf.Max (1, Mathf.Abs(newTorque)/10),MaxAmort)+1;
				newForce = new Vector2 ((0) * Force * dist / amort, (1) * Force * dist / amort);
				this.GetComponent<Rigidbody2D> ().AddRelativeForce(newForce,ForceMode2D.Impulse);
				this.GetComponent<Rigidbody2D> ().AddTorque (newTorque, ForceMode2D.Impulse); 
				//moveLine.GetComponent<RectTransform> ().localEulerAngles = new Vector3 (0, 0, angle * cross);
			}
		}
	}
}
