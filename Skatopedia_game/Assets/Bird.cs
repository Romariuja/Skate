using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Obstacle{

    // Use this for initialization
    Vector3 direction;
    Bird myScript;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Table" || collision.gameObject.tag == "Skater")
        {
            
            //   Debug.Log("Activa el collider de la banderola " + gameObject.name);
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            direction = new Vector3(Random.Range(0,10), Random.Range(5, 15),0).normalized*10;
            myScript = GetComponent<Bird>();
            myScript.enabled = !myScript.enabled;
            //GetComponent<Rigidbody2D>().velocity=direction;
            //Debug.Log("Activa el collider de la banderola " + gameObject.name +" Se ha activado?" + gameObject.GetComponent<Rigidbody2D>().bodyType);
        }

       
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MainCamera")
        {
            Destroy(gameObject);

        }
    }
        void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("PAJARO ACTIVO");
        GetComponent<Rigidbody2D>().velocity = direction;
    }
}
