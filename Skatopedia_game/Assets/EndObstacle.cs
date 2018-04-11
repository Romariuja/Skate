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
        Player.CameraScript.lastRoutineZoom = StartCoroutine(Player.CameraScript.ZoomCamera_position(1.5f, 0.01f, 10, 7));
        yield return new WaitForSeconds(2*espera);
       Debug.Log( Puntuacion.EndScore.transform.localPosition.x);
    //    Debug.Break();
       // Puntuacion.EndScore.transform.localPosition = new Vector3(0,0,1);
        //Debug.Log("El nombre del objeto blur es: " +ControladorCamara.Blur.name);
       Player.CameraScript.Blur.SetActive(true);
        //Player.CameraScript.shader1.SetFloat("_Size", 0);

        float fade = 0;
        // Vector3 pos = Puntuacion.EndScore.transform.localPosition;
        //Defining pos the score position moves linealy
        while (fade < 1)
        {
            //  Puntuacion.EndScore.transform.localPosition = Vector3.Lerp(pos, new Vector3(0,0,1), fade);
            Puntuacion.EndScore.transform.localPosition = Vector3.Lerp(Puntuacion.EndScore.transform.localPosition, new Vector3(0, 0, 1), fade);
            Player.CameraScript.Blur.GetComponent<CanvasRenderer>().SetAlpha(0.001f*fade);
            //Player.CameraScript.Blur.GetComponent<CanvasRenderer>().GetMaterial().SetFloat("_Size", fade);
            fade =fade+0.1f*Time.deltaTime;
            Debug.Log("Position Score"+ Puntuacion.EndScore.transform.localPosition + "Fade: " +fade);
            yield return null;

        }
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Table" || collision.gameObject.tag == "Skater") &&  (PhysicsObject.levelOver == false))
        {
           //Debug.Log("FINAL DE LA PANTALLA "+ collision.gameObject.transform.parent);
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
