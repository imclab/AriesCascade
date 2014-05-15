using UnityEngine;
using System.Collections;

public class TConOne : MonoBehaviour {
	
	public float thrust;
	public float thrustU;
	public ParticleSystem BackT;
	public ParticleSystem SBackT;
	public ParticleSystem ForeT;
	public ParticleSystem SForeT;
	public ParticleSystem RightT;
	public ParticleSystem SRightT;
	public ParticleSystem LeftT;
	public ParticleSystem SLeftT;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Horizontal") > 0) { //checking for right arrow
			SLeftT.Play ();
			//SRightT.Stop ();
			//SForeT.Stop ();
			//SBackT.Stop ();
			rigidbody.AddForceAtPosition(-SLeftT.transform.up * thrust, SLeftT.transform.position);
		}
		if (Input.GetAxis("Horizontal") < 0) { //checking for left arrow
			//SLeftT.Stop ();
			SRightT.Play ();
			//SForeT.Stop ();
			//SBackT.Stop ();
			rigidbody.AddForceAtPosition(-SRightT.transform.up * thrust, SRightT.transform.position);
		}
		if(Input.GetAxis("Vertical") > 0) { //checking for up arrow
			//SLeftT.Stop ();
			//SRightT.Stop ();
			//SForeT.Stop ();
			SBackT.Play ();
			rigidbody.AddForceAtPosition(-SBackT.transform.up * thrust, SBackT.transform.position);
		}
		if(Input.GetAxis("Vertical") < 0) { //checking for down arrow
			//SLeftT.Stop ();
			//SRightT.Stop ();
			SForeT.Play ();
			//SBackT.Stop ();
			rigidbody.AddForceAtPosition(-SForeT.transform.up * thrust, SForeT.transform.position);
		}
		if(Input.GetKey("space")) {
			BackT.Play ();
			ForeT.Play ();
			LeftT.Play ();
			RightT.Play ();
			rigidbody.AddForce(Vector3.up * thrustU);
		}
	}
}
