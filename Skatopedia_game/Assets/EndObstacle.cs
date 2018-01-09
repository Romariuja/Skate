using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndObstacle : Obstacle {

   
    // Use this for initialization
    void Start () {
		
	}
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Table" || collision.gameObject.tag == "Skater")
        {
           Debug.Log("FINAL DE LA PANTALLA "+ collision.gameObject.transform.parent);
            //   collision.gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity=new Vector3(0,0,0);
             StartCoroutine(PC.GetComponent<Player>().slowDown());  
             PC.levelOver = true;
            //Debug.Log("Activa el collider de la banderola " + gameObject.name +" Se ha activado?" + gameObject.GetComponent<Rigidbody2D>().bodyType);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
