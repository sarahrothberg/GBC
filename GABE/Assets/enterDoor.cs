using UnityEngine;
using System.Collections;

public class enterDoor : MonoBehaviour {

    Animator anim;
    public AudioClip sound;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnTriggerEnter()
    {
        anim.SetTrigger("DoorOpen");
    }

    void OnTriggerExit()
    {
        anim.SetTrigger("DoorClose");
    }
}

