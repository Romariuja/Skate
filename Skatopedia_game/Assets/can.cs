using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class can : Obstacle {



    void OnCollisionEnter2D(Collision2D collision)
    {

        // Debug.Log(collision.gameObject.name);
        //Debug.Log(collision.GetType());
        //Debug.Log(collision is BoxCollider2D);
       Debug.Log("Objeto con el que colisiona: "+ collision.gameObject.name + " Tag: "+ collision.gameObject.tag);
        if (collision.gameObject.tag=="Player")
        {
            //  StartCoroutine(Reaction(collision, "lightweight"));
           
                    //  yield return new WaitForSeconds(0.1f);
                    Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
                Debug.Log("Lata: " + GetComponent<Collider2D>().gameObject.name + " Objeto con el que choca: " + collision.collider.gameObject.name);
              
            }

        }

    
        // Use this for initialization
        void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       
        
	}
}
