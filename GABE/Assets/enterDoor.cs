using UnityEngine;
using System.Collections;


public class enterDoor : MonoBehaviour {

    Animator anim;
    public AudioClip openDoor;
    public AudioClip closeDoor;


    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {


    }

    void OnTriggerEnter(Collider other)
    {
        

        Debug.Log(other.tag);
        if (other.tag == "Player") {
            Debug.Log("not the door");
            anim.SetTrigger("DoorOpen");
            GetComponent<AudioSource>().PlayOneShot(openDoor);

        }

    }

    void OnTriggerExit(Collider other)
    {


        if (other.tag == "Player")
        {
            anim.SetTrigger("DoorClose");
            GetComponent<AudioSource>().PlayOneShot(closeDoor);

        }
      }

   
}

