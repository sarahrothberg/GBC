using UnityEngine;
using System.Collections;

public class PlaqueController : MonoBehaviour {
    public GameObject Plaques;
    public AudioClip openDoor;
    public AudioClip closeDoor;


    void OnTriggerEnter(Collider other)
    {
        Plaques.SetActive(true);
        

    }
    void OnTriggerExit()
    {
        Plaques.SetActive(false);
        GetComponent<AudioSource>().PlayOneShot(openDoor);
    }
}
