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
    float yOffset = 6;
    float xOffset = 6;
    public bool onZoom = false;
    float tamCam;
    public float LerpTime = 0;
    public float LerpTimey = 0;
    public float LerpTimex = 0;
    public static bool MoveY = false;
    private float maxzoom = 0.5f;
    private float minzoom = 1.5f;
    private float noZoom = 1.2f;


    public Coroutine lastRoutineZoom = null;


    void OnTriggerEnter2D(Collider2D collider)
    {

        if ((collider.gameObject.tag == "Obstacle"))
        {
            obstacle = collider.gameObject.transform.GetComponent<Obstacle>();
            obstacle.animationCam = true;
        }
    }


    public IEnumerator MoveCameraY(float Sign)
    {
       LerpTimey = 0;
        while ((Mathf.Abs(player.transform.position.y - transform.position.y) > yOffset))
      
        {
            LerpTimey = LerpTimey + 0.5f*Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffset, transform.position.z), LerpTimey);
            yield return null;           
        }
        MoveY = false;
    }

    

    public IEnumerator ZoomCamera(float Zoom, float zoomVel, float X, float Y)
    {
        LerpTime = 0;
        onZoom = true;
        while (LerpTime < 1)
        {
            xOffset = Mathf.Lerp(xOffset, X, LerpTime);
            yOffset = Mathf.Lerp(yOffset, Y, LerpTime);
            LerpTime = LerpTime + zoomVel * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffset, transform.position.z), LerpTimey);        
            cam.rect = new Rect(0, 0, Mathf.Lerp(1,zoom,LerpTime), 1);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, tamCam * Zoom, LerpTime);
            yield return null;
        }
        onZoom = false;
    }



    void Start () {
        cam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
        PC = player.GetComponent<Player>();
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
        cameraMargin = height/5;
        rect = cam.rect;
        scale = transform.localScale;
        tamCam = cam.orthographicSize;
        lastRoutineZoom = StartCoroutine(ZoomCamera(noZoom,1, xOffset, yOffset));
    }
    // Update is called once per frame
    void Update () {

   
       // Debug.DrawLine(transform.position,player.transform.position,Color.green);
       // Debug.Log("Xoffset: " + xOffset+ " yoffset: " +yOffset + "Zoom:"+zoom +" MOVER EJE Y "+ (Mathf.Abs(player.transform.position.y - transform.position.y) > yOffset));
        transform.position = new Vector3(player.transform.position.x + xOffset, transform.position.y, transform.position.z);
      
        //Follow Player Y position  out of the camera limits 
       
        if (MoveY == false && (Mathf.Abs(player.transform.position.y - transform.position.y) > yOffset))
        {
            MoveY = true;
            float Sign = Mathf.Sign((player.transform.position.y - transform.position.y));
            StartCoroutine(MoveCameraY(Sign));   
        }      
        
        float currentVel = player.transform.GetComponent<Rigidbody2D>().velocity.x;


        //Expand camera view if velocity greater than Maxvel
        if (currentVel>MaxVelCam && (!PhysicsObject.gameOver) && !onZoom && zoom!=minzoom)
   
        {
            // StopCoroutine(lastRoutineZoom);
           
            zoom = minzoom;
            lastRoutineZoom=StartCoroutine(ZoomCamera(zoom,1,12,6));
           // zoomOut = true;   
        }


       
            else if (currentVel <= PC.MaxVel && (!PhysicsObject.gameOver) && !onZoom && zoom!=noZoom)
            {
                Debug.Log("Reduce el Zoom porque la velocidad " + currentVel + "es menor que el umbral " + PC.MaxVel);
           
            //StopCoroutine(lastRoutineZoom);
            zoom = noZoom;
            lastRoutineZoom = StartCoroutine(ZoomCamera(zoom,1,6,3));
            onZoom = true;
        }
    }
}

  

