using UnityEngine;
using System.Collections;

public class ChronoBehavior : MonoBehaviour {


	// Update is called once per frame
	void Update () {
		GetComponent<RectTransform> ().localEulerAngles = new Vector3 (0, 0, -transform.parent.rotation.eulerAngles.z );

	}
}
