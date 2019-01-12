using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAnimation : MonoBehaviour {
    public List<GameObject> places;
    public List<GameObject> choices;
    private List<GameObject> activeElements;
    private float xDist;

	void Start () {
        activeElements = new List<GameObject>
        {
            Instantiate(choices[choices.Count - 1]),
            Instantiate(choices[0]),
            Instantiate(choices[1]),
            Instantiate(choices[2 % choices.Count]),
            Instantiate(choices[3 % choices.Count])
        };
        xDist = places[1].transform.position.x - places[0].transform.position.x;
        activeElements[0].transform.position = places[0].transform.position - new Vector3(xDist, 0, 0);
        activeElements[1].transform.position = places[0].transform.position;
        activeElements[2].transform.position = places[1].transform.position;
        activeElements[3].transform.position = places[2].transform.position;
        activeElements[4].transform.position = places[2].transform.position + new Vector3(xDist, 0, 0);
    }
	
	void Update () {
        activeElements[1].transform.position = Vector3.MoveTowards(activeElements[0].transform.position, places[0].transform.position - new Vector3(xDist, 0, 0), 0.01f);
        activeElements[2].transform.position = Vector3.MoveTowards(activeElements[0].transform.position, places[0].transform.position, 0.01f);
        activeElements[3].transform.position = Vector3.MoveTowards(activeElements[1].transform.position, places[0].transform.position, 0.01f);
        activeElements[4].transform.position = Vector3.MoveTowards(activeElements[2].transform.position, places[0].transform.position, 0.01f);
	}
}
