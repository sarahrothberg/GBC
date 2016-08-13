using UnityEngine;
using System.Collections;

public class InterviewAudio : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    }

    void OnTriggerEnter()
    {
        GetComponent<AudioSource>().Play();
    }

    void OnTriggerExit()
    {
        GetComponent<AudioSource>().Stop();
    }
}
