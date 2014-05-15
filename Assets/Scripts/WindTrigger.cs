using UnityEngine;
using System.Collections;

public class WindTrigger : MonoBehaviour {

	public bool enableZone;
	public float force;
	public Vector3 direction;

	// Update is called once per frame
	void OnTriggerStay (Collider other) {
		if(enableZone)	other.rigidbody.AddForce (direction * force);
	}

}