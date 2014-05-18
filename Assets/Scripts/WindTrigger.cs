using UnityEngine;
using System.Collections;

public class WindTrigger : MonoBehaviour {
	
	static int force;
	Vector3 angle;
	static GameObject wbox;

	void Start () {
		force = 0;
		angle = Vector3.forward;
		wbox = GameObject.Find("WindBox");
	}

	// Update is called once per frame
	void OnTriggerStay (Collider other) {
		other.rigidbody.AddForce (angle * force);
	}

	public static void changeForce(int round) {
		force = Random.Range (2 * round, 6 * round);
	}

	public static void changeAngle() {
		float ry = Random.Range (0.0f, 360.0f);
		wbox.transform.Rotate (0, ry, 0);
	}

}