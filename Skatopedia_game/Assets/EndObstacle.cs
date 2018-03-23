using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndObstacle : Obstacle {

   private float fade=1000;

    // Use this for initialization
    void Start () {
		
	}

    IEnumerator FinalZoom(float espera)
    {
        yield return new WaitForSeconds(espera);
        Player.CameraScript.lastRoutineZoom = StartCoroutine(Player.CameraScript.ZoomCamera_position(1.4f, 0.01f, 10, 6));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Table" || collision.gameObject.tag == "Skater") &&  (PhysicsObject.levelOver == false))
        {
           Debug.Log("FINAL DE LA PANTALLA "+ collision.gameObject.transform.parent);
           // StartCoroutine();
            //   collision.gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity=new Vector3(0,0,0);
            StartCoroutine(PC.GetComponent<Player>().slowDown(15));
           // StartCoroutine(ControladorCamara.player.slowDown());
         StartCoroutine(FinalZoom(2));
            PhysicsObject.levelOver = true;

            
            //  collision.gameObject.GetComponent<Player>().enabled = false;
        }


    }
    // Update is called once per frame
    void Update () {
		
	}
}
