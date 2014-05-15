using UnityEngine;
using System.Collections;

public class TCon : MonoBehaviour {
	
	public float thrust;
	public float thrustU;
	public ParticleSystem closeRight;
	public ParticleSystem closeLeft;
	public ParticleSystem farRight;
	public ParticleSystem farLeft;
	public GameObject locusL;
	public GameObject locusR;
	public GameObject locusF;
	public GameObject locusB;
	public GameObject locusC;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Horizontal") > 0) { //checking for right arrow
			closeLeft.Play ();
			farLeft.Play ();
			closeRight.Stop ();
			farRight.Stop ();
			rigidbody.AddForceAtPosition(-locusR.transform.up * thrust, locusR.transform.position);
		}
		if (Input.GetAxis("Horizontal") < 0) { //checking for left arrow
			closeRight.Play ();
			farRight.Play ();
			closeLeft.Stop ();
			farLeft.Stop ();
			rigidbody.AddForceAtPosition(-locusL.transform.up * thrust, locusL.transform.position);
		}
		if(Input.GetAxis("Vertical") > 0) { //checking for up arrow
			closeLeft.Play ();
			closeRight.Play ();
			farRight.Stop ();
			farLeft.Stop ();
			rigidbody.AddForceAtPosition(-locusF.transform.up * thrust, locusF.transform.position);
		}
		if(Input.GetAxis("Vertical") < 0) { //checking for down arrow
			farLeft.Play ();
			farRight.Play ();
			closeRight.Stop ();
			closeLeft.Stop ();
			rigidbody.AddForceAtPosition(-locusB.transform.up * thrust, locusB.transform.position);
		}
		if(Input.GetKey("space")) {
			closeLeft.Play ();
			closeRight.Play ();
			farLeft.Play ();
			farRight.Play ();
			rigidbody.AddForce(locusC.transform.up * thrustU);
		}
	}
}
