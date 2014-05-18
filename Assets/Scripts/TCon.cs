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
    public GameObject sound;

    private bool rThrust, lThrust, fThrust, bThrust, uThrust;

	// Use this for initialization
	void Start () {
        sound = GameObject.FindGameObjectWithTag("SoundManager");
        rThrust = lThrust = fThrust = bThrust = uThrust = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Horizontal") > 0)
        { //checking for right arrow
            rThrust = true;
        }
        else rThrust = false;
        if (Input.GetAxis("Horizontal") < 0)
        { //checking for left arrow
            lThrust = true;
        }
        else lThrust = false;
        if (Input.GetAxis("Vertical") > 0)
        { //checking for up arrow
            fThrust = true;
        }
        else fThrust = false;
        if (Input.GetAxis("Vertical") < 0)
        { //checking for down arrow
            bThrust = true;
        }
        else bThrust = false;
        if (Input.GetKey("space"))
        {
            uThrust = true;
        }
        else uThrust = false;
	}

    void FixedUpdate()
    {
        if(rThrust)
        {
            closeLeft.Play();
            farLeft.Play();
            closeRight.Stop();
            farRight.Stop();
            rigidbody.AddForceAtPosition(-locusR.transform.up * thrust, locusR.transform.position);
        }
        if(lThrust)
        {
            closeRight.Play();
            farRight.Play();
            closeLeft.Stop();
            farLeft.Stop();
            rigidbody.AddForceAtPosition(-locusL.transform.up * thrust, locusL.transform.position);
        }
        if (fThrust)
        {
            closeLeft.Play();
            closeRight.Play();
            farRight.Stop();
            farLeft.Stop();
            rigidbody.AddForceAtPosition(-locusF.transform.up * thrust, locusF.transform.position);
        }
        if (bThrust)
        {
            farLeft.Play();
            farRight.Play();
            closeRight.Stop();
            closeLeft.Stop();
            rigidbody.AddForceAtPosition(-locusB.transform.up * thrust, locusB.transform.position);
        }
        if (uThrust)
        {
            closeLeft.Play();
            closeRight.Play();
            farLeft.Play();
            farRight.Play();
            rigidbody.AddForce(locusC.transform.up * thrustU);
        }
    }
}
