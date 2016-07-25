using UnityEngine;
using System.Collections;

public class bubbleEnter : MonoBehaviour {

    public Transform player;
    public Transform bubblePosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter(Collider bubble)
    {
        if (bubble.gameObject.tag == "Bubble")
         {
            Debug.Log("enter bubble");
            player.localScale = new Vector3(0.02F, 0.02F, 0.02F);
            player.position = bubblePosition.position;
        }

    }
}
