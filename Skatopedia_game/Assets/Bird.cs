using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Obstacle{

    // Use this for initialization
    Vector3 direction;
    Bird myScript;
    public GameObject Sprite;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (animationCam && collision.gameObject.tag == "MainCamera")
        {
          
            myScript = GetComponent<Bird>();
            myScript.enabled = true;
            GetComponent<MeshRenderer>().enabled = true;
            Sprite.GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<LoopAll>().enabled = true;

            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
             direction = new Vector3(Random.Range(3, 7), Random.Range(1, 4), 0).normalized * 5;
            //direction = new Vector3(1, 0.5f,0).normalized * 5;
            Debug.Log("Direction " + direction);
        }

        else if (collision.gameObject.tag == "Table" || collision.gameObject.tag == "Skater" && direction==null)
        {
            
            //   Debug.Log("Activa el collider de la banderola " + gameObject.name);
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            direction = new Vector3(Random.Range(0,10), Random.Range(5, 15),0).normalized*10;
            Debug.Log("Direction " + direction);
            myScript = GetComponent<Bird>();
            myScript.enabled = true;
            GetComponent<MeshRenderer>().enabled = true;
            Sprite.GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<LoopAll>().enabled = true;
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
        gameObject.GetComponent<Rigidbody2D>().velocity = direction;
    }
}
