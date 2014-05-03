using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	public float thrust = 20f;
	private float u = 0f;
	private float h;
	private float v;

	/*void FixedUpdate() {
		rigidbody.AddForce(Vector3.right * h * hThrust);
	   	rigidbody.AddForce(Vector3.forward * v * hThrust);
	   	rigidbody.AddForce(Vector3.up * u * vThrust);
	}*/

	void Update() {
		//if(Input.GetAxis("Horizontal") != 0) h = Input.GetAxis("Horizontal");
		//else h = 0;
		//if(Input.GetAxis("Vertical") != 0) v = Input.GetAxis("Vertical");
		//else v = 0;
		if(Input.GetKey("space")) u = 1;
		else u = 0;
	}

	void FixedUpdate() {
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3(h, u, v);
		rigidbody.AddForce(movement * thrust);
	}
}