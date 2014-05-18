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
    public int TotalScore;
    public float distance;
    public bool didSum;
    public Quaternion startingRotation;


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
        //newRounds();
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
        
        if (roundNum < numOfRounds)
        {
            if (landed == true && !crashed)
            {
              
                if (GUI.Button(new Rect(Screen.width * 0.5f - 40, Screen.height * 0.5f - 20, 120, 40), "Next Launch"))
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

                        if (distance > 200) score = 1;
                        else
                        {
                            score = (int)((200 - distance)*10);
                        }
                        TotalScore = TotalScore + score;
                    }

                    landed = false;
                    crashed = false;
                    landingPoints[roundNum].transform.Find("Mars_Arrow").gameObject.SetActive(false);

                    roundNum++;
                    landingPoints[roundNum].transform.Find("Mars_Arrow").gameObject.SetActive(true);
                }
            }
            else if (crashed)
            {
                //Debug.Log("CRASSSSSSSSHED!!!!");
                
                if (GUI.Button(new Rect(Screen.width * 0.5f - 40, Screen.height * 0.5f - 20, 80, 40), "Continue?"))
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
                    distance = Vector3.Distance(landingPoints[roundNum].transform.position, spaceshipArray[roundNum].transform.position);
                    TotalScore = scoreCalc(distance);
                    didSum = true;
                }
                GUI.Label(new Rect(Screen.width * 0.5f - 40, Screen.height * 0.5f - 40, 80, 40), "Mission Complete!");
                GUI.Label(new Rect(Screen.width * 0.5f - 40, Screen.height * 0.5f - 5, 80, 40), "Your Final Score:" + TotalScore.ToString("##0"));
                if (GUI.Button(new Rect(Screen.width/2 - 40, Screen.height / 2 + 60, 100, 40), "Restart"))
                {
                  Application.LoadLevel("default");
                }
            }

        }
        GUI.Label(new Rect(Screen.width - 300, 10, 2000, 2000), "Score: " + TotalScore.ToString("##0"));
    }



    void FixedUpdate()
    {
       
           
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
  
    public int scoreCalc(float distance)
   {
       int LastScore;
        //calculate score of player
       if (distance > 200)  LastScore = 1;
       else
       {
          LastScore = (int)((200 - distance) * 10);
       }
       TotalScore = TotalScore + LastScore;
       return TotalScore;
    }

    

}
