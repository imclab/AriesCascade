using UnityEngine;
using System.Collections;

public class TCon : MonoBehaviour {

	public float thrust;
	public ParticleSystem doorRightT;
	public ParticleSystem doorLeftT;
	public ParticleSystem endRightT;
	public ParticleSystem endLeftT;
	public GameObject locusL;
	public GameObject locusR;
	public GameObject locusF;
	public GameObject locusB;
	public GameObject locusC;
    public GameObject sound;

	// Use this for initialization
	void Start () {
        sound = GameObject.FindGameObjectWithTag("SoundManager");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Horizontal") > 0) { //checking for right arrow
			doorLeftT.Play ();
			endLeftT.Play ();
			doorRightT.Stop ();
			endRightT.Stop ();
			rigidbody.AddForceAtPosition(-locusR.transform.up * thrust, locusR.transform.position);
		}
		if (Input.GetAxis("Horizontal") < 0) { //checking for left arrow
			doorRightT.Play ();
			endRightT.Play ();
			doorLeftT.Stop ();
			endLeftT.Stop ();
			rigidbody.AddForceAtPosition(-locusL.transform.up * thrust, locusL.transform.position);
		}
		if(Input.GetAxis("Vertical") > 0) { //checking for up arrow
			doorLeftT.Play ();
			doorRightT.Play ();
			endRightT.Stop ();
			endLeftT.Stop ();
			rigidbody.AddForceAtPosition(-locusF.transform.up * thrust, locusF.transform.position);
		}
		if(Input.GetAxis("Vertical") < 0) { //checking for down arrow
			endLeftT.Play ();
			endRightT.Play ();
			doorRightT.Stop ();
			doorLeftT.Stop ();
			rigidbody.AddForceAtPosition(-locusB.transform.up * thrust, locusB.transform.position);
		}
		if(Input.GetKey("space")) {
			doorLeftT.Play ();
			doorRightT.Play ();
			endLeftT.Play ();
			endRightT.Play ();
			rigidbody.AddForce(locusC.transform.up * (thrust * 10));
            sound.GetComponent<GameSounds>().playThrust();
		}
		/*else {
		//else if((Input.GetAxis("Vertical") == 0) || (Input.GetAxis ("Horizontal") == 0)) { //checking if no Vert keys are down
			doorRightT.Stop ();
			doorLeftT.Stop ();
			endRightT.Stop ();
			endLeftT.Stop ();
		}*/
	}
}
