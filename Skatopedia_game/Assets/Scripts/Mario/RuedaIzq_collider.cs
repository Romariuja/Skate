using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuedaIzq_collider : Player_Controller{

   // public static bool onFloorizq=false;
    //public static bool inGrindizq = false;
    // Use this for initialization
    void Start () {
		
	}
    void OnTriggerEnter2D(Collider2D collider)
    {
        // Debug.Log(gameObject.name);
        onFloor = true;
        if (collider.gameObject.layer == LayerMask.NameToLayer("Collision"))
            GameOver = true;
        //MoviePlayer.Anime("Skater_CrippledM", false, true);
       // StartCoroutine(End());
       else if (collider.gameObject.layer == LayerMask.NameToLayer("Grind"))
            inGrind = true;

    }

    void OnTriggerStay2D(Collider2D collider)
    {
        // Debug.Log(gameObject.name);
        if(collider.gameObject.tag!="Player")
        onFloor = true;
   
    }

    void OnTriggerExit2D(Collider2D collider)
    {
     //   Debug.Log(collider.gameObject.name);
        onFloor = false;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
