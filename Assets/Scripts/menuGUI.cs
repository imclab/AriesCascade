using UnityEngine;
using System.Collections;

public class menuGUI : MonoBehaviour {
    
    public GUISkin skin = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.skin = skin;

        GUI.Label(new Rect(Screen.width/2 - 350, Screen.height/2 - 250, 1500, 250), "Ares Cascade");

        Rect playBox = new Rect(Screen.width/2 - 100, Screen.height/2 - 50, 200, 100);

        if(GUI.Button(playBox, "Play"))
        {
            Application.LoadLevel("default");
        }

    }
}
