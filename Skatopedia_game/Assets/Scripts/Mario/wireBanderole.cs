using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wireBanderole : Obstacle {

    // Use this for initialization

void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Table" || collision.gameObject.tag == "Skater")
        { 
//   Debug.Log("Activa el collider de la banderola " + gameObject.name);
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        //Debug.Log("Activa el collider de la banderola " + gameObject.name +" Se ha activado?" + gameObject.GetComponent<Rigidbody2D>().bodyType);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor") || collision.gameObject.layer == LayerMask.NameToLayer("Collision"))
        {
       //     Debug.Log("DESTRUYE"+gameObject.name);
           Destroy(gameObject);
        }
              
    }

    void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}
}
