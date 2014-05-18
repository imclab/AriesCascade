using UnityEngine;
using System.Collections;

public class roundResults : MonoBehaviour
{

    public static bool crashed;
    public static int roundNum;
    public int numOfRounds;
    public static bool landed;
    public Vector3 spawnPos;
    public Vector3 endPos;
    public static GameObject[] spaceshipArray;
    public GameObject[] landingPoints = new GameObject[5];
    public GameObject payload;
    public GameObject landP1;
    public GameObject landP2;
    public GameObject landP3;
    public GameObject landP4;
    public GameObject landP5;
    public static int TotalScore;
    public float distance;
    public bool didSum;
    public Quaternion startingRotation;
	public WindTrigger wbox;
    public static float altitude;
    public GUISkin skin;


    //private MouseOrbit cam;
    // Use this for initialization
    void Start()
    {
        numOfRounds = 4;
        spaceshipArray = new GameObject[numOfRounds + 1];
        spaceshipArray[0] = GameObject.Find("ShippingContainer");
        spawnPos = payload.transform.position;
        MouseOrbit.target  = spaceshipArray[0];
        roundNum = 0;
        TotalScore = 0;
        distance = 0.0f;
        startingRotation = payload.transform.rotation;
        didSum = false;
        landed = false;
        crashed = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Terrain" || collision.gameObject.tag == "MarsTerrain")
	    {

			if (InGameUI.velNum < 12.0)
	        {
	        	landed = true;

	        }
	        else                

	        {
                gameObject.renderer.enabled = false;
                gameObject.rigidbody.isKinematic = true;
                GameObject explosion = (GameObject)Instantiate(Resources.Load("Detonator-Upwards"), transform.position, transform.rotation);
	            crashed = true;
	        }
	    }
	}       

    void OnGUI()
    {
        GUI.skin = skin;
        skin.label.fontSize = 25;
        skin.button.fontSize = 13;
        if (roundNum < numOfRounds)
        {
            if (landed == true && !crashed)
            {
                GUI.Label(new Rect(Screen.width * 0.5f - 87.5f, Screen.height * 0.5f - 70, 250, 60), "Nice Landing!");
                if (GUI.Button(new Rect(Screen.width * 0.5f + 10, Screen.height * 0.5f - 20, 150, 40), "Next Launch ?"))
                {
                    //copies an empty payload at the spot of the landing and moves current payload back to start
                    //once all payload prefabs are in should create new payload, switch camera target, and delete last
                    endPos = payload.transform.position;
                    spaceshipArray[roundNum] = (GameObject)Instantiate(Resources.Load("SpaceshipDummy"), endPos, transform.rotation);
                    spaceshipArray[roundNum].gameObject.GetComponentInChildren<Camera>().enabled = false;
                    spaceshipArray[roundNum].gameObject.GetComponentInChildren<AudioListener>().enabled = false;
                    MouseOrbit.target = payload;
                    payload.transform.rotation = startingRotation;
                    payload.rigidbody.angularVelocity = Vector3.zero;
                    payload.rigidbody.velocity = Vector3.zero;
                    payload.transform.position = spawnPos;

                    if (roundNum >= 0)
                    {
                        int score;
                        // print("inside");
                        distance = Vector3.Distance(landingPoints[roundNum].transform.position, spaceshipArray[roundNum].transform.position);
                        print(distance);
                        if (distance > 50) score = 1;
                        else
                        {
                            score = (int)((50 - distance)*10);
                        }
                        TotalScore = TotalScore + score;
                    }

                    landed = false;
                    crashed = false;
                    landingPoints[roundNum].transform.Find("Mars_Arrow").gameObject.SetActive(false);

                    roundNum++;
                    landingPoints[roundNum].transform.Find("Mars_Arrow").gameObject.SetActive(true);
					WindTrigger.changeForce(roundNum);
					WindTrigger.changeAngle();
                }
                if (GUI.Button(new Rect(Screen.width * 0.5f - 190, Screen.height * 0.5f - 20, 150, 40), "Restart Launch ?"))
                {

                    MouseOrbit.target = payload;
                    payload.transform.rotation = startingRotation;
                    payload.rigidbody.angularVelocity = Vector3.zero;
                    payload.rigidbody.velocity = Vector3.zero;
                    payload.transform.position = spawnPos;
                    WindTrigger.changeForce(roundNum);
                    WindTrigger.changeAngle();
                    landed = false;
                    crashed = false;
                }
            }
            else if (crashed)
            {
                GUI.Label(new Rect(Screen.width * 0.5f - 87.5f, Screen.height * 0.5f - 70, 250, 60), "You Crashed!");
                if (GUI.Button(new Rect(Screen.width * 0.5f + 10, Screen.height * 0.5f - 20, 150, 40), "Next Launch ?"))
                {
                    endPos = payload.transform.position;
                    spaceshipArray[roundNum] = (GameObject)Instantiate(Resources.Load("SpaceshipDummy"), endPos, transform.rotation);
                    spaceshipArray[roundNum].gameObject.GetComponentInChildren<Camera>().enabled = false;
                    spaceshipArray[roundNum].gameObject.GetComponentInChildren<AudioListener>().enabled = false;
                    spaceshipArray[roundNum].gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
                    MouseOrbit.target = payload;
                    payload.renderer.enabled = true;
                    payload.rigidbody.isKinematic = false;
                    payload.transform.rotation = startingRotation;
                    payload.rigidbody.angularVelocity = Vector3.zero;
                    payload.rigidbody.velocity = Vector3.zero;
                    payload.transform.position = spawnPos;

                    landingPoints[roundNum].transform.Find("Mars_Arrow").gameObject.SetActive(false);
                    roundNum++;
                    landingPoints[roundNum].transform.Find("Mars_Arrow").gameObject.SetActive(true);
					WindTrigger.changeForce(roundNum);
					WindTrigger.changeAngle();
                    landed = false;
                    crashed = false;
                }
                if (GUI.Button(new Rect(Screen.width * 0.5f - 190, Screen.height * 0.5f - 20, 150, 40), "Restart Launch ?"))
                {
                    payload.rigidbody.isKinematic = false;
                    payload.renderer.enabled = true;
                    MouseOrbit.target = payload;
                    payload.transform.rotation = startingRotation;
                    payload.rigidbody.angularVelocity = Vector3.zero;
                    payload.rigidbody.velocity = Vector3.zero;
                    payload.transform.position = spawnPos;
                    WindTrigger.changeForce(roundNum);
                    WindTrigger.changeAngle();
                    landed = false;
                    crashed = false;
                }
            }
            
        }

        if (roundNum == numOfRounds)
        {
            if (landed || crashed)
            {
               
                // final round
                spaceshipArray[roundNum] = payload;
                if (!didSum)
                {
                    print(roundNum);
                    if(landed)
                    {
                        int score;
                        distance = Vector3.Distance(landingPoints[roundNum].transform.position, spaceshipArray[roundNum].transform.position);
                        if (distance > 50) score = 1;
                        else
                        {
                            score = (int)((50 - distance) * 10);
                        }
                        TotalScore = TotalScore + score;
                        KongregateAPI.instance.SubmitStats("Highscore", TotalScore);
                    }
                    else
                    {
                        KongregateAPI.instance.SubmitStats("Highscore", TotalScore);
                    }
                    roundNum++;
                    didSum = true;
                }
            }

        }
        if(roundNum == numOfRounds+1)
        {
            if (TotalScore > 0)
            {
                GUI.Label(new Rect(Screen.width * 0.5f - 120, Screen.height * 0.5f - 40, 300, 50), "Mission Complete!");
            }
            else
            {
                GUI.Label(new Rect(Screen.width * 0.5f - 100, Screen.height * 0.5f - 40, 300, 50), "Mission Failed!");
            }
            GUI.Label(new Rect(Screen.width * 0.5f - 105, Screen.height * 0.5f - 5, 250, 60), "Final Score : " + TotalScore.ToString("##0"));
            if (GUI.Button(new Rect(Screen.width / 2 - 85, Screen.height / 2 + 60, 170, 60), "Restart Mission"))
            {
                Application.LoadLevel("default");
            }
        }
        skin.label.fontSize = 40;
        skin.button.fontSize = 20;
    }



    void FixedUpdate()
    {

        
        RaycastHit hit;

        if(Physics.Raycast(transform.position, new Vector3(0,-1,0), out hit, 300.0f))
        {
            altitude = transform.position.y - hit.point.y;
            if (altitude < 2.0f) altitude = 0;
            Debug.DrawLine(transform.position, hit.point);
        }


    }


    public bool isUpright( GameObject theShip )
    {
    if ( theShip.transform.eulerAngles.z > 295 ) {
        return false;
    }
 
    if ( theShip.transform.eulerAngles.z < 65 ) {
        return false;
    }
 
    return true;
    }


    

}
