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
    public GameObject payload;
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

			if (InGameUI.velVec.y < 10.0)
	        {
	        	landed = true;

	        }
	        else
	        {
	            crashed = true;
	        }
	    }
	}       

    void OnGUI()
    {
        
        if (roundNum < numOfRounds)
        {
            if (landed == true)
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
                    payload.transform.position = spawnPos;

                    if (roundNum > 0)
                    {
                        // print("inside");
                        distance = Vector3.Distance(spaceshipArray[0].transform.position, spaceshipArray[roundNum].transform.position);
                        if (distance > 10000) distance = 9999;
                        int score = (int)(10000 - distance);
                        TotalScore = TotalScore + score;
                    }

                    landed = false;
                    crashed = false;
                    roundNum++;
                }
            }
            else if (crashed)
            {
                Debug.Log("CRASSSSSSSSHED!!!!");
                if (GUI.Button(new Rect(Screen.width * 0.5f - 40, Screen.height * 0.5f - 20, 80, 40), "Continue?"))
                {
                    endPos = payload.transform.position;
                    spaceshipArray[roundNum] = (GameObject)Instantiate(Resources.Load("SpaceshipDummy"), endPos, transform.rotation);
                    spaceshipArray[roundNum].gameObject.GetComponentInChildren<Camera>().enabled = false;
                    spaceshipArray[roundNum].gameObject.GetComponentInChildren<AudioListener>().enabled = false;
                    MouseOrbit.target = payload;
                    payload.transform.rotation = startingRotation;
                    payload.transform.position = spawnPos;
                    roundNum++;
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
                    distance = Vector3.Distance(spaceshipArray[0].transform.position, spaceshipArray[roundNum].transform.position);
                    TotalScore = scoreCalc(distance);
                    didSum = true;
                }
                GUI.Label(new Rect(Screen.width * 0.5f - 40, Screen.height * 0.5f - 40, 80, 40), "Mission Complete!");
                GUI.Label(new Rect(Screen.width * 0.5f - 40, Screen.height * 0.5f - 5, 80, 40), "Your Final Score:" + TotalScore.ToString("##0"));
            }

        }
        GUI.Label(new Rect(Screen.width - 300, 10, 2000, 2000), "Score: " + TotalScore.ToString("##0"));
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
        //calculate score of player
       if (distance > 10000) distance = 9999;
       int lastScore = (int)(10000 - distance);
       TotalScore = TotalScore + lastScore;
       return TotalScore;
    }

}
