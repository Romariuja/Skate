using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectTrain : Obstacle {
    float Vel = 12;
    float TrainVel = 0;
    float acel = 1f;
    //private Player_Controller PC;


    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "stopPoint" ) { 
            animationCam = false;
        //Debug.Log("PARA EL TREN");
        }
    }
    // Use this for initialization
    void Start () {
        animationCam = false;
        PC = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }
	
	// Update is called once per frame
	void Update () {
        if (animationCam) { 
        transform.GetComponent<Rigidbody2D>().velocity =new Vector2(TrainVel,0);
           TrainVel = TrainVel + acel * Time.deltaTime;
            
        }
    }
}
