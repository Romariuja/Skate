﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCamara : MonoBehaviour {
    private GameObject player;
    // Use this for initialization
    float height;
    float width;
    Camera cam;
    float cameraMargin;
    float MaxVelCam=15f;
    private Player PC;
    private Obstacle obstacle;
    private Rect rect;
    private Vector3 scale;
    private float zoom=1;
    public static float yOffset=3;
    public static float xOffset = 6;
    public bool onZoom=false;
    float tamCam;

 

    void OnTriggerEnter2D(Collider2D collider)
    {

        if ((collider.gameObject.tag=="Obstacle"))
        {
             // collider.gameObject.SetActive(true);
             obstacle=collider.gameObject.transform.GetComponent<Obstacle>();
             //Debug.Log("El Script es:" + obstacle);
             //obstacle.enabled = true;
             obstacle.animationCam = true;
        }
    }


    public IEnumerator MoveCamera()
    {
   
        float LerpTime = 0;
   
        while (Mathf.Abs(player.transform.position.y - transform.position.y) > yOffset)
        { 
            LerpTime = LerpTime +0.1f*Time.deltaTime;
            //Debug.Log("se sale de camara");
           // transform.position =Vector3.Lerp(transform.position, new Vector3(transform.position.x, player.transform.position.y+yOffset, transform.position.z), LerpTime);
            transform.position =Vector3.Lerp(transform.position, new Vector3(player.transform.position.x+xOffset, player.transform.position.y+yOffset, transform.position.z), LerpTime);
            yield return null;
           // Debug.Log("Distancia ejey"+ Mathf.Abs(player.transform.position.y- transform.position.y-yOffset ));
        }
         //Debug.Log("Reset Lerptime");
         LerpTime = 0;
    }

    public IEnumerator ZoomCamera(float Zoom)
    {
        //Probelma que no para de hacer zoom si sigue a mucha velocidad
 
        float LerpTime = 0;
        //onZoom = true;
        while (LerpTime<1)
        {
            LerpTime = LerpTime +Time.deltaTime;
            cam.rect = new Rect(0, 0, Mathf.Lerp(1,zoom,LerpTime), 1);
            //transform.localScale = new Vector2(Mathf.Lerp(1, zoom, LerpTime), 1);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, tamCam * Zoom, LerpTime);
            //Debug.Log("SIGUE EL ZOOM. LerpTime zoomCamera" + LerpTime * 100 + "%. Zoom " +zoom);
           yield return null;
        }
    
        LerpTime = 0;
     //  onZoom = false;
    }
    void Start () {
        cam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");

        PC = player.GetComponent<Player>();
        //PC = player.GetComponent<Player_Controller>();
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
        cameraMargin = height/5;
        rect = cam.rect;
        scale = transform.localScale;
        tamCam = cam.orthographicSize;
     //   Debug.Log("Las dimensiones iniciales del campo de apertura de la camara son: " + rect + " LA escala es: " +scale);
    }
    // Update is called once per frame
    void Update () {
        //Follow the player X position
        Debug.Log("xOffset: "+ xOffset +" yOffset: " + yOffset);
        
        transform.position = new Vector3(player.transform.position.x+xOffset, transform.position.y, transform.position.z);
      

        //Debug.Log("Las dimensiones iniciales del campo de apertura de la camara son: " + rect + " LA escala es: " + scale);
        //Follow Player Y position  out of the camera limits 
        if (Mathf.Abs(player.transform.position.y - transform.position.y) >cameraMargin)
        {
     
          StartCoroutine(MoveCamera());     
        }


        //Expand camera view if velocity less than Maxvel
        // if (player.transform.GetComponent<Rigidbody2D>().velocity.x> MaxVelCam && (!onZoom))
        float currentVel = player.transform.GetComponent<Rigidbody2D>().velocity.magnitude;
       // Debug.Log("Velocidad actual " + currentVel);
        if (currentVel>MaxVelCam && (!PC.gameOver) && !onZoom)
   
        {
          //  Debug.Log("Amplia el Zoom porque la velocidad " + currentVel + "es mayor que el umbral " + MaxVelCam);
            zoom = 1.3f;
            StartCoroutine(ZoomCamera(zoom));
            onZoom = !onZoom;
            return;
         
            
        }
        //Problema porque al terminar el zoom vuelve a hacer siempre zoom si sigue a mucha velocidad
        else if (onZoom==true && currentVel <= PC.MaxVel) {
            //Debug.Log("Reduce el Zoom porque la velocidad " + currentVel + "es menor que el umbral " + PC.MaxVel);
            // LerpTime2 = 0;
            zoom = 1;
            StartCoroutine(ZoomCamera(zoom));
           //onZoom = false;
            onZoom = !onZoom;
        }
    }
}

  

