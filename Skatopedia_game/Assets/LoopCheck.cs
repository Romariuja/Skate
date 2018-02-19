using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopCheck : ControladorCamara
{
    private int check=0;
    public CustomImageEffect Effect;
   
   // private IEnumerator coroutine;
    

    void OnTriggerEnter2D(Collider2D collision)
    {
        //yield return new WaitForFixedUpdate();
        if (collision.gameObject.tag == "Table")
        {
       //     yield return new WaitForFixedUpdate();
           // Debug.Log("Antes_Check" + check);
            check=check+1;
           // Debug.Log("Despues_Check" + check);
            //Debug.Break();
           // Debug.Log(Player.cam);
           // Debug.Log(Player.puntua);
            if (check == 2 || check == 6)
            {
                //StartCoroutine(TargetLoop());
                Debug.Log("SLOW MOTION START");
                //  StopCoroutine(Player.CameraScript.lastRoutineZoom);
                Player.CameraScript.stopZoom();
                // Player.CameraScript.onZoom = false;
                //  StartCoroutine(Player.CameraScript.MoveCamera());

                zoom = 0.5f;
                Player.CameraScript.lastRoutineZoom = StartCoroutine(Player.CameraScript.ZoomCamera(zoom,5,0,0));
                //     StartCoroutine("coroutine");
              

                //lastRoutine = StartCoroutine(YourCoroutine());

                Time.timeScale = 0.1F;                    
                Player.EffectCam.enabled = true;
                //StartCoroutine(Player.CameraScript.MoveCamera(collision.gameObject.GetComponent<Transform>().position.x, collision.gameObject.GetComponent<Transform>().position.y));
            }
             else if (check == 4 || check == 8)
            {
                //Player.CameraScript.onZoom = false;
                Player.EffectCam.enabled = false;
                Time.timeScale = 1F;
                // Debug.Log(puntua.Inc);
                Player.combo++;
                Player.puntua.IncrementarCombo(Player.combo, "LOOP", 50000);
                Debug.Log("X " + Player.puntua.special.transform.position.x + "Y " + Player.puntua.special.transform.position.y);
                Debug.Log("X LOCAL" + Player.puntua.special.transform.localPosition.x + "Y LOCAL" + Player.puntua.special.transform.position.y);
             
                Player.puntua.IncrementSpecial("LOOP", Player.puntua.special.transform.position.x, Player.puntua.special.transform.position.y,283,0.5f);



                // StartCoroutine(Player.CameraScript.MoveCameraY());
                //StopCoroutine(Player.CameraScript.lastRoutineZoom);
                //Player.CameraScript.stopZoom();
                 // Debug.Log("SE DETIENE EL ZOOM");
                 zoom = 1;
                Player.CameraScript.lastRoutineZoom = StartCoroutine(Player.CameraScript.ZoomCamera(zoom,1, 6, 3));
                //   Debug.Break();
                //  Debug.Log("Zoom lanzado por obstaculo Loop, Lerptime:" +Player.CameraScript.LerpTime);
                //  Debug.Break();
                //     StartCoroutine(Player.CameraScript.ZoomCamera(1,6,3));
                // Debug.Log("Se ha lanzado zoom ,el  Lerptime debería reiniciarse:" + Player.CameraScript.LerpTime);
                // Debug.Break();

            }
          //  yield return new WaitForFixedUpdate();
            //Without the "WaitForFixedUpdate() it increments twice check value entering only on one collider"
            //  GetComponent<Collider2D>().enabled = false;
        }
        

    }

    // Use this for initialization
    void Start () {
        
    
        check = 0;
        //Aqui aun no está defida Cam, puntua etc
  
     
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
