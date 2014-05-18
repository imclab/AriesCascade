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
    public TerrainCollider marsTerrain;
    public Vector3 windVelocity = Vector3.zero;
    public GameObject landingPos;

    private bool rThrust, lThrust, fThrust, bThrust, uThrust;

	// Use this for initialization
	void Start () {
        sound = GameObject.FindGameObjectWithTag("SoundManager");
        rThrust = lThrust = fThrust = bThrust = uThrust = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(!roundResults.landed && !roundResults.crashed)
        {
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
        else
        {
            rThrust = lThrust = fThrust = bThrust = uThrust = false;
        }
        
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
            //landingPos.transform.position = GetLandingPos();
        }
        if(lThrust)
        {
            closeRight.Play();
            farRight.Play();
            closeLeft.Stop();
            farLeft.Stop();
            rigidbody.AddForceAtPosition(-locusL.transform.up * thrust, locusL.transform.position);
            //landingPos.transform.position = GetLandingPos();
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
            //landingPos.transform.position = GetLandingPos();
        }
        if (uThrust)
        {
            closeLeft.Play();
            closeRight.Play();
            farLeft.Play();
            farRight.Play();
            rigidbody.AddForce(locusC.transform.up * thrustU);
            //landingPos.transform.position = GetLandingPos();
        }
        landingPos.transform.position = GetLandingPos();
    }

    

    Vector3 GetLandingPos()
    {
        if(rigidbody.velocity.y < 0)
        {
            Vector3 lastPosition = transform.position;
            Vector3 lastVelocity = rigidbody.velocity;//transform.InverseTransformDirection(rigidbody.velocity);
            const int iterSteps = 50;
            float deltaY = lastPosition.y / iterSteps;
            Debug.DrawRay(lastPosition, new Vector3(lastVelocity.x, 0f, 0f), Color.blue);
            Debug.DrawRay(lastPosition, new Vector3(0f, 0f, lastVelocity.z), Color.red);
            Debug.DrawRay(lastPosition, new Vector3(0f, lastVelocity.y, 0f), Color.green);
            Debug.DrawRay(lastPosition, lastVelocity, Color.white);
            for (int i = 0; i < iterSteps; i++)
            {
                float newY = lastPosition.y - deltaY;
                float vsqr = (2 * Mathf.Abs(Physics.gravity.y) * lastPosition.y) + (2 * Mathf.Abs(Physics.gravity.y) * newY) + (Mathf.Pow(lastVelocity.y, 2));
                float v = Mathf.Sqrt(vsqr);
                float t = (v - Mathf.Abs(lastVelocity.y)) / Mathf.Abs(Physics.gravity.y);
                float x = lastPosition.x + (lastVelocity.x * t);// +(0.5f * windVelocity.x * t * t);
                float z = lastPosition.z + (lastVelocity.z * t);// +(0.5f * windVelocity.z * t * t);
                float newVX = lastVelocity.x;// +windVelocity.x * t;
                float newVZ = lastVelocity.z;// +windVelocity.z * t;

                Vector3 newPosition = new Vector3(x, newY, z);
                Debug.DrawLine(lastPosition, newPosition, Color.grey);
                Debug.DrawLine(lastPosition, lastPosition + Vector3.down, Color.magenta);
                if (marsTerrain.bounds.Contains(newPosition))
                {
                    RaycastHit info;
                    if (Physics.Raycast(new Ray(newPosition, Vector3.down), out info, 20.0f))
                    {
                        //Debug.Log("raycast hit!");
                        return info.point;
                    }
                }
                lastPosition = newPosition;
                lastVelocity = new Vector3(newVX, v, newVZ);
            }
        }
        return Vector3.zero;
    }
}
