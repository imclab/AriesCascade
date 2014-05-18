using UnityEngine;
using System.Collections;

public class menuGUI : MonoBehaviour {
    
    public GUISkin skin = null;
    private bool showCredits;

	// Use this for initialization
	void Start () {
        showCredits = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.skin = skin;
       
        Rect playBox = new Rect(Screen.width/2 - 110, Screen.height/2 - 50, 220, 100);
        Rect creditsBox = new Rect(Screen.width/2 - 110, Screen.height/2 + 70, 220, 100);

        if(!showCredits)
        {
            GUI.Label(new Rect(Screen.width / 2 - 350, Screen.height / 2 - 250, 1500, 250), "Ares Cascade");
            if (GUI.Button(playBox, "Play"))
            {
                Application.LoadLevel("default");
            }
            if (GUI.Button(creditsBox, "Credits"))
            {
                showCredits = true;
            }
        }
        else
        {
            GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 290, 1500, 250), "Credits");
            skin.label.fontSize = 35;
            GUI.Label(new Rect(Screen.width/2 - 150, Screen.height/2 - 100, 600, 100), "Marco Ancheta");
            GUI.Label(new Rect(Screen.width / 2 - 135, Screen.height / 2 - 50, 600, 100), "Logan McGhee");
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 , 600, 100), "Kevin McCotter");
            GUI.Label(new Rect(Screen.width / 2 - 170, Screen.height / 2 + 50, 600, 100), "Kenny Willoughby");


            skin.label.fontSize = 100;
            if(GUI.Button(new Rect(Screen.width - 180, Screen.height - 60, 175, 50), "Back"))
            {
                showCredits = false;
            }
        }
        

    }
}
