using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour {

    private Player PC;
    float delta;
    private Obstacle obstacle;
    private RigidbodyConstraints2D originalConstraintsTable;

    void awake()
    {
        originalConstraintsTable = GetComponent<Rigidbody2D>().constraints;
    }

    // Use this for initialization
    void Start()
    {
      GameObject  player = GameObject.FindGameObjectWithTag("Player");
        //PC = transform.parent.parent.GetComponent<Player_Controller>();
        PC=player.GetComponent<Player>();
        Debug.Log("El jugador es:" +player.name);
    }


    // TRIGGER COLLIDER FUNCTIONS
    //TO DETECT OBJECTS COLLISIONS___________________________________________________________________________________________________________________________________________________________

    //IF COLIIDER TRIGGER IS TOUCHING (STAYS) SOMETHING DIFFERENT THAN ITSELF OR THE CAMERA
    void OnTriggerStay2D(Collider2D collider)
    {

//Debug.Log("ESTA TOCANDO CON EL SUELO:EL COLLIDER ES" + collider.gameObject.tag + ". EL Frame actaul es" + Time.frameCount);
        if (collider.gameObject.tag != "Player" && collider.gameObject.tag != "MainCamera" && collider.gameObject.layer != LayerMask.NameToLayer("Grind"))
        {
            PC.onFloor = true;
//Debug.Log("ESTA TOCANDO CON EL SUELO:EL COLLIDER ES" +collider.gameObject.tag +". EL Frame actaul es" +Time.frameCount);
        }

        if (collider.gameObject.layer == LayerMask.NameToLayer("Grind") && !(PC.gameOver))
        {
            PC.onGrind = true;
            //PC.onFloor = true;
          
          
        }

    }

   /* private void OnTriggerExit2D(Collider2D collider)
    {
       
            PC.onFloor = false;
        PC.onGrind= false;

    }*/

    private void Update()
    {
       
        // PC.onFloor = false;
    }



    //IF COLIIDER TRIGGER HITS SOMETHING FLOOR, GRIND OR OBSTACLE
    /*void OnTriggerEnter2D(Collider2D collider)
    {
      
        if (collider.gameObject.layer == LayerMask.NameToLayer("Obstacle") && !PC.GameOver)
        {
            //obstacle.animationTable = true;
            Debug.Log("SE INTERACCIONA CON EL OBSTACULO " + collider.name);
            obstacle = collider.gameObject.transform.GetComponent<Obstacle>();
            obstacle.enabled = true;
           //  Debug.Log("El Script es:" + obstacle);
          

        }
        else if (collider.gameObject.layer == LayerMask.NameToLayer("Collision") && !PC.GameOver)
        {
            PC.GameOver = true;
            Debug.Log("FINAL2 Collision Object");
            return;
        }
        else if (collider.gameObject.layer == LayerMask.NameToLayer("Grind"))
        {

            delta = PC.Local_Gravity(PC.currentVel);
            //   transform.parent.parent.Rotate(new Vector3(0, 0, delta));           
            // transform.parent.parent.GetComponent<Rigidbody2D>().AddTorque(delta, ForceMode2D.Force);
           transform.parent.parent.RotateAround(transform.position, new Vector3(0, 0, 1), delta);

            PC.outGrind = false;
                PC.onGrind = true;
                PC.onFloor = true;
                PC.inGrind = true;
                 PC.onGrind = true;
         
        }
      else if (collider.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
         
                delta = PC.Local_Gravity(PC.currentVel);
            // transform.parent.parent.Rotate(new Vector3(0, 0, delta));  
            Debug.DrawRay(transform.position, transform.up, Color.red);
      
            transform.parent.parent.RotateAround(transform.position, new Vector3(0, 0, 1),delta);
           // Debug.DrawRay(transform.position, transform.up, Color.blue);
            //transform.parent.parent.GetComponent<Rigidbody2D>().AddTorque(delta, ForceMode2D.Force);
           
            PC.inGrind = false;
            PC.onFloor = true;
            PC.onGrind = false;
                PC.outGrind = false;

            }
            else if (collider.gameObject.layer == LayerMask.NameToLayer("Loop")) { 
            PC.VelThreshold = 12f;
            PC.inGrind = false;
            PC.onGrind = false;
            PC.outGrind = false;

        }
    }

    //TABLE EXITS FROM COLLIDER

    void OnTriggerExit2D(Collider2D collider)
    {
     
     
        PC.onGrind = false;
        PC.outGrind = false;
        PC.Grind_5_0 = false;
        PC.contGrind = 1;


        if (collider.gameObject.layer == LayerMask.NameToLayer("Grind"))
        {
            PC.outGrind = true;
        }
        else if (collider.gameObject.layer == LayerMask.NameToLayer("Loop"))
        {
            collider.enabled = false;
            Debug.Log("SALIENDO DEL LOOP " + gameObject.name);
            PC.VelThreshold = 4f;
        }

    }*/




}
