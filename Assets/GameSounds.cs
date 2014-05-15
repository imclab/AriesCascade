using UnityEngine;
using System.Collections;

public class GameSounds : MonoBehaviour {

    private static GameSounds instance;
    public AudioClip bgm;
    public AudioClip thruster;

    // Make this game object and all its transform children
    // survive when loading a new scene.
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void OnLevelWasLoaded() {
       
         
      
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void playThrust()
    {
        print("play thrust SFX");
        audio.clip = thruster;
        audio.Play();
    }
}
