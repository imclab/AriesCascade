using UnityEngine;
using System.Collections;

public class InGameUI : MonoBehaviour {

    private bool isPaused;
    public GUISkin skin;
    public GameObject payload;
    public static Vector3 velVec;
    public static float velNum;
    public static Vector3 rayCastDirection;

	// Use this for initialization
	void Start () {
        isPaused = false;
        Time.timeScale = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("escape") && !isPaused)
        {
            Time.timeScale = 0.0f;
            isPaused = true;
        }
        else if (Input.GetKeyDown("escape") && isPaused)
        {
            Time.timeScale = 1.0f;
            isPaused = false;
        }
	}

    void OnGUI()
    {
        GUI.skin = skin;

        if(isPaused)
        {
            if (GUI.Button(new Rect(Screen.width - 120, 10, 100, 50), "Resume"))
            {
                isPaused = false;
                Time.timeScale = 1.0f;
            }
            if(GUI.Button(new Rect(Screen.width - 120, 70, 100, 50), "Menu"))
            {
                Application.LoadLevel("Menu");
            }
        }
        else
        {
            if(GUI.Button(new Rect(Screen.width - 120, 10, 100, 50), "Pause"))
            {
                isPaused = true;
                Time.timeScale = 0.0f;
            }
        }

        velVec = (payload.GetComponent<Rigidbody>().velocity);

        velNum = Mathf.Sqrt(Mathf.Pow(velVec.x, 2) + Mathf.Pow(velVec.y, 2) + Mathf.Pow(velVec.z, 2));

        if(roundResults.roundNum != 5 && (!roundResults.landed || !roundResults.crashed))
        {
            GUI.Label(new Rect(10, 10, 1000, 2000), "Velocity: " + velNum.ToString("##0.00"));
            GUI.Label(new Rect(10, Screen.height - 50, 1000, 2000), "Altitude: " + roundResults.altitude.ToString("##0") + " meters");
            GUI.Label(new Rect(Screen.width - 275, Screen.height - 80, 1000, 200), "Launch : " + (roundResults.roundNum + 1));
            GUI.Label(new Rect(Screen.width - 275, Screen.height - 50, 1000, 200), "Score : " + roundResults.TotalScore.ToString("##0"));
        }
        
    }

    
}
