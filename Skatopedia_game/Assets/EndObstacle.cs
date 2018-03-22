using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndObstacle : Obstacle {

   private float fade=1000;
    private bool end=false;
    // Use this for initialization
    void Start () {
		
	}

    IEnumerator ZoomFinal( float espera)
    {
        yield return new WaitForSeconds(espera);
       Player.CameraScript.lastRoutineZoom = StartCoroutine(Player.CameraScript.ZoomCamera_Position(1.4f, 0.1f, 10, 5));
        yield return null;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Table" || collision.gameObject.tag == "Skater") && end==false)
        {
            end = true;
           Debug.Log("FINAL DE LA PANTALLA "+ collision.gameObject.transform.parent);
           // StartCoroutine();
            //   collision.gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity=new Vector3(0,0,0);
            StartCoroutine(PC.GetComponent<Player>().slowDown(15));
           StartCoroutine(ZoomFinal(2));
          //  Debug.Break();
           
            //Player.CameraScript.lastRoutineZoom = StartCoroutine(Player.CameraScript.MoveCameraY(1));
    
                PhysicsObject.levelOver = true;
            
            //  collision.gameObject.GetComponent<Player>().enabled = false;
        }


    }
    // Update is called once per frame
    void Update () {
		
	}
}
