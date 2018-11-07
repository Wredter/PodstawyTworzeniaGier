using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Joystick1X") || Input.GetButton("Joystick2X"))
        {
            SceneManager.LoadScene("stachuj");
        }
	}
}
