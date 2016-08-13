using UnityEngine;
using System.Collections;

namespace VRStandardAssets.Utils
{
    public class enterDoor : MonoBehaviour {

        Animator anim;



        // Use this for initialization
        void Start() {
            anim = GetComponent<Animator>();

        }

        // Update is called once per frame
        void Update() {

        }

        void OnTriggerExit(Collider other)
        {


            if (other.tag == "Player")
            {
                anim.SetTrigger("DoorClose");

            }
        }
    }
}

