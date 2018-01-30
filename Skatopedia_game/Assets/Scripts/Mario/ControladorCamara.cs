using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCamara : MonoBehaviour {
    private GameObject player;
    // Use this for initialization
    float height;
    float width;
    Camera cam;
    float cameraMargin;
    float MaxVelCam = 15f;
    private Player PC;
    private Obstacle obstacle;
    private Rect rect;
    private Vector3 scale;
    public static float zoom = 1.2f;
    public static float yOffset = 3;
    public static float xOffset = 6;
    public bool onZoom = false;
    float tamCam;
    public float LerpTime = 0;
    public float LerpTimey = 0;
    public static bool MoveY = false;
    private float maxzoom = 0.5f;
    private float minzoom = 1.5f;
    private float noZoom = 1.2f;


    public Coroutine lastRoutineZoom = null;


    void OnTriggerEnter2D(Collider2D collider)
    {

        if ((collider.gameObject.tag == "Obstacle"))
        {
            // collider.gameObject.SetActive(true);
            obstacle = collider.gameObject.transform.GetComponent<Obstacle>();
            //Debug.Log("El Script es:" + obstacle);
            //obstacle.enabled = true;
            obstacle.animationCam = true;
        }
    }


    public IEnumerator MoveCameraY(float Sign)
    {
       LerpTimey = 0;
        //  Debug.Log("MOVE CAMERA Y. LerpTimeY" + LerpTimey);

        //    Debug.Break();

        while ((Mathf.Abs(player.transform.position.y - transform.position.y) > yOffset))
      // while (LerpTimey < 1)
        {
            LerpTimey = LerpTimey + 0.5f*Time.deltaTime;
           //  Debug.Log("LerpTimey" + LerpTimey +" . Condicion posicion Y-Ycamara mayor que YOffset: " + (Mathf.Abs(player.transform.position.y - transform.position.y) > yOffset));
            // transform.position =Vector3.Lerp(transform.position, new Vector3(transform.position.x, player.transform.position.y+yOffset, transform.position.z), LerpTime);
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffset, transform.position.z), LerpTimey);

            yield return null;
            // Debug.Log("Distancia ejey"+ Mathf.Abs(player.transform.position.y- transform.position.y-yOffset ));

        }
        Debug.Log("LerpTimey" + LerpTimey + " . Condicion posicion Y-Ycamara mayor que YOffset: " + (Mathf.Abs(player.transform.position.y - transform.position.y) > yOffset));
        MoveY = false;
        // Debug.Log("MOVE CAMERA Y FINISH. LerpTimeY" + LerpTimey);

        //Debug.Break();
    }

    public IEnumerator ZoomCamera(float Zoom, float zoomVel, float X, float Y)
    {
        //Problema que no para de hacer zoom si sigue a mucha velocidad
   //    Debug.Log("ZOOM CAMERA START: Xoffset=" +X +". Yoffset=" +Y );
     //   Debug.Break();
        LerpTime = 0;
        onZoom = true;
        // Debug.Break();
        // yield return null;
        //onZoom = true;
        while (LerpTime < 1)
        {
            // Debug.Log("Lerp Time" + LerpTime*100 +"%");
            xOffset = Mathf.Lerp(xOffset, X, LerpTime);
            yOffset = Mathf.Lerp(yOffset, Y, LerpTime);
            // transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffset, transform.position.z), LerpTime);
            LerpTime = LerpTime + zoomVel*Time.deltaTime;
             cam.rect = new Rect(0, 0, Mathf.Lerp(1,zoom,LerpTime), 1);
            //transform.localScale = new Vector2(Mathf.Lerp(1, zoom, LerpTime), 1);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, tamCam * Zoom, LerpTime);
            //Debug.Log("SIGUE EL ZOOM. LerpTime zoomCamera" + LerpTime * 100 + "%. Zoom " + zoom + " yOffset="+ yOffset);
            yield return null;
        }
        onZoom = false;

        
        //Debug.Log("END ZOOM");
        //Debug.Break();

        //  onZoom = false;
    }



    void Start () {
        cam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
       // coroutineZoom = ZoomCamera(0.5f, 0, 0);

        PC = player.GetComponent<Player>();
        //PC = player.GetComponent<Player_Controller>();
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
        cameraMargin = height/5;
        rect = cam.rect;
        scale = transform.localScale;
        tamCam = cam.orthographicSize;
        lastRoutineZoom = StartCoroutine(ZoomCamera(1,1, xOffset, yOffset));
        //   Debug.Log("Las dimensiones iniciales del campo de apertura de la camara son: " + rect + " LA escala es: " +scale);
    }
    // Update is called once per frame
    void Update () {

        //Follow the player X position
        Debug.DrawLine(transform.position,player.transform.position,Color.green);
      //  Debug.Log("Xoffset: " + xOffset+ " yoffset: " +yOffset);
    
            transform.position = new Vector3(player.transform.position.x + xOffset, transform.position.y, transform.position.z);
       


        //Debug.Log("Las dimensiones iniciales del campo de apertura de la camara son: " + rect + " LA escala es: " + scale);
        //Follow Player Y position  out of the camera limits 
        // if (Mathf.Abs(player.transform.position.y - transform.position.y) >cameraMargin)
        //{
        if (MoveY == false && (Mathf.Abs(player.transform.position.y - transform.position.y) > height/4))
        {
            Debug.Log("EJECUTA MOVEY. YOffset= " +yOffset +" . Movey=" + MoveY);
           // Debug.Break();
            MoveY = true;
            float Sign = Mathf.Sign((player.transform.position.y - transform.position.y));
            StartCoroutine(MoveCameraY(Sign));
     
        }
        //   StartCoroutine(MoveCameraX());
        //}


        //Expand camera view if velocity less than Maxvel
        // if (player.transform.GetComponent<Rigidbody2D>().velocity.x> MaxVelCam && (!onZoom))
        //float currentVel = player.transform.GetComponent<Rigidbody2D>().velocity.magnitude;
        float currentVel = player.transform.GetComponent<Rigidbody2D>().velocity.x;
       // Debug.Log("Velocidad actual=" + currentVel +". MaVelCam= " + MaxVelCam);
    
        
        
        if (currentVel>MaxVelCam && (!PhysicsObject.gameOver) && !onZoom && zoom!=minzoom)
   
        {
            // StopCoroutine(lastRoutineZoom);
           // Debug.Log("Amplia el Zoom porque la velocidad " + currentVel + "es mayor que el umbral " + MaxVelCam);
         //   Debug.Break();
            zoom = minzoom;
            lastRoutineZoom=StartCoroutine(ZoomCamera(zoom,1,6,150));
           // zoomOut = true;   
        }


        
        //Problema porque al terminar el zoom vuelve a hacer siempre zoom si sigue a mucha velocidad
        //else if (currentVel <= PC.MaxVel && (!PhysicsObject.gameOver) && !onZoom && zoomOut)  {
            else if (currentVel <= PC.MaxVel && (!PhysicsObject.gameOver) && !onZoom && zoom!=noZoom)
            {
                Debug.Log("Reduce el Zoom porque la velocidad " + currentVel + "es menor que el umbral " + PC.MaxVel);
          //Debug.Break();
            // LerpTime2 = 0;
            //StopCoroutine(lastRoutineZoom);
            zoom = noZoom;
            lastRoutineZoom = StartCoroutine(ZoomCamera(zoom,1,xOffset,yOffset));
           //onZoom = false;
            onZoom = true;
        }
    }
}

  

