using UnityEngine;
using System.Collections;

public class ChronoBehavior : MonoBehaviour {

	public bool ordering = true;
	public float turnDuration = 300;
	// Update is called once per frame

	void Init () {
		Animation anim = GetComponent<Animation> ();
		foreach (AnimationState state in anim) {
			state.speed=0.5F;
		}
	}

	void FixedUpdate () {
		GetComponent<RectTransform> ().localEulerAngles = new Vector3 (0, 0, -transform.parent.rotation.eulerAngles.z );

	}
}
