using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour {
	void FixedUpdate () {
		if(Input.GetButton("Joystick1X") || Input.GetButton("Joystick2X"))
        {
            SceneManager.LoadScene("stachuj");
        }
	}
}
