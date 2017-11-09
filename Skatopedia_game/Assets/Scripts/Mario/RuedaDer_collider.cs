using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuedaDer_collider : Player_Controller {
    public bool inGrindder=false;
    public static bool onFloorder=false;
	// Use this for initialization
	void Start () {
		
	}
    void OnTriggerEnter2D(Collider2D collider)
    {
        // Debug.Log(gameObject.name);
        onFloorder = true;
    //    Debug.Log(collider.gameObject.layer);
        if (collider.gameObject.layer == LayerMask.NameToLayer("Collision"))
          GameOver = true;
        //MoviePlayer.Anime("Skater_CrippledM", false, true);
      //  StartCoroutine(End());
       else if (collider.gameObject.layer == LayerMask.NameToLayer("Grind"))
            inGrindder = true;
     

    }

    void OnTriggerStay2D(Collider2D collider)
    {
        // Debug.Log(gameObject.name);
        if(collider.gameObject.tag!="Player")
        onFloorder = true;


        if (collider.gameObject.layer == LayerMask.NameToLayer("Grind"))
        {
            onGrind = true;
            onFloor = true;
            puntua.IncrementarPuntos(PuntosGrind * combo);

            puntua.ActualizarMarcador();
        }
        else
            onGrind = false;


    }

    void OnTriggerExit2D(Collider2D collider)
    {
        //  Debug.Log(collider.gameObject.name);
        onFloor = false;
        onGrind = false;
        outGrind = false;



        if (collider.gameObject.layer == LayerMask.NameToLayer("Grind"))
        {
            outGrind = true;
            //  MoviePlayer.Anime("Skater_Idle", false, true);
            //Debug.Log("Ahora sale del Grind");

            //onGrind = false;

            // MoviePlayer.Anime("Skater_Flex2Idle", false);

        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
