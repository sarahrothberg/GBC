using UnityEngine;
using System.Collections;

public class PlaqueController : MonoBehaviour {
    public GameObject Plaques;

    void OnTriggerEnter(Collider other)
    {
        Plaques.SetActive(true);
        
    }
    void OnTriggerExit()
    {
        Plaques.SetActive(false);
    }
}
