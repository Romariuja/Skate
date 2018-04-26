using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitobject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {


        // Debug.Log(collision.gameObject.name);
        //Debug.Log(collision.GetType());
        //Debug.Log(collision is BoxCollider2D);
        // Debug.Log("Objeto con el que colisiona: "+ collision.gameObject.name + " Tag: "+ collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {

     
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
