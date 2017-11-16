using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopCheck : MonoBehaviour {
    private int check=0;
    public CustomImageEffect Effect;

  
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
                Debug.Log("SLOW MOTION");
                ControladorCamara.xOffset = 0;
                ControladorCamara.yOffset = 0;
               // Player.CameraScript.onZoom = false;
                StartCoroutine(Player.CameraScript.MoveCamera());
               StartCoroutine(Player.CameraScript.ZoomCamera(0.5f));
                Time.timeScale = 0.2F;
               
             
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
                Player.puntua.IncrementarCombo(Player.combo, "LOOP", 1);
            
                //ControladorCamara.xOffset = 3;
                //ControladorCamara.yOffset = 6;
                StartCoroutine(Player.CameraScript.MoveCamera());
                StartCoroutine(Player.CameraScript.ZoomCamera(1));

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
