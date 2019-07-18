using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour {

    [SerializeField] int screenWidth = 900;
    [SerializeField] int screenHeight = 1600;



    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        //float rate = screenWidth / screenHeight;
        if (Screen.width!=screenWidth)
        {
            Screen.SetResolution(screenWidth, screenHeight, true);
            // set to true if you want a fullscreen game
        }
    }
}
