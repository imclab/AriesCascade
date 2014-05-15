using UnityEngine;
using System.Collections;

public class roundResults : MonoBehaviour
{

    public static bool crashed;
    public static int roundNum;
    public int numOfRounds;
    public bool landed;
    public Vector3 spawnPos;
    public Vector3 endPos;
    public static GameObject[] spaceshipArray;
    public GameObject payload;

    //private MouseOrbit cam;
    // Use this for initialization
    void Start()
    {
        numOfRounds = 4;
        spaceshipArray = new GameObject[numOfRounds];
        // spaceshipArray[0] = GameObject.Find("ShippingContainer");
         spawnPos = payload.transform.position;
        //MouseOrbit.target  = spaceshipArray[0];
         roundNum = 0;
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
                print("landed");
                if (GUI.Button(new Rect(Screen.width * 0.5f - 40, Screen.height * 0.5f - 20, 80, 40), "Next Launch"))
                {

                    endPos = payload.transform.position;
                    spaceshipArray[roundNum] = (GameObject)Instantiate(Resources.Load("SpaceshipDummy"), endPos, transform.rotation);
                    spaceshipArray[roundNum].gameObject.GetComponentInChildren<Camera>().enabled = false;
                    spaceshipArray[roundNum].gameObject.GetComponentInChildren<AudioListener>().enabled = false;
                    MouseOrbit.target = payload;
                    payload.transform.position = spawnPos;
                    landed = false;
                    crashed = false;
                    roundNum++;
                }
            }
            else if (crashed == true)
            {
                Debug.Log("CRASSSSSSSSHED!!!!");
                if (GUI.Button(new Rect(Screen.width * 0.5f - 40, Screen.height * 0.5f - 20, 80, 40), "Continue?"))
                {
                    endPos = payload.transform.position;
                    spaceshipArray[roundNum] = (GameObject)Instantiate(Resources.Load("SpaceshipDummy"), endPos, transform.rotation);
                    spaceshipArray[roundNum].gameObject.GetComponentInChildren<Camera>().enabled = false;
                    spaceshipArray[roundNum].gameObject.GetComponentInChildren<AudioListener>().enabled = false;
                    MouseOrbit.target = payload;
                    payload.transform.position = spawnPos;
                    landed = false;
                    crashed = false;
                    roundNum++;
                }
            }
            
        }

    }

}
