using UnityEngine;
using System.Collections;

public class InGameUI : MonoBehaviour {

    private bool isPaused;
    public GUISkin skin;
    public GameObject payload;

	// Use this for initialization
	void Start () {
        isPaused = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("escape") && !isPaused)
        {
            print("Paused");
            Time.timeScale = 0.0f;
            isPaused = true;
        }
        else if (Input.GetKeyDown("escape") && isPaused)
        {
            print("Unpaused");
            Time.timeScale = 1.0f;
            isPaused = false;
        }
	}

    void OnGUI()
    {
        GUI.skin = skin;

        if(isPaused)
        {

        }
        else
        {
            if(GUI.Button(new Rect(Screen.width - 120, 10, 100, 50), "Pause"))
            {
                isPaused = true;
            }
        }

        float velNum = (payload.GetComponent<Rigidbody>().velocity.y) * -1.0f;
        

        GUI.Label(new Rect(10, 10, 1000, 2000), "Velocity: " + velNum.ToString("##0.00"));
    }
}
