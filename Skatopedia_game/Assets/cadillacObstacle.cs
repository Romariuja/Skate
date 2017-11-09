using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cadillacObstacle : Obstacle
{
    float Vel = 12;
    float currentVel = -3;
    float acel = 1f;
    //private Player_Controller PC;

   void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "stopPoint" || PC.gameOver==true)
            animationCam = false;
       
    }
    // Use this for initialization
    void Start()
    {
        animationCam = false;
        PC = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animationCam && !PC.gameOver)
        {
            transform.GetComponent<Rigidbody2D>().velocity = new Vector2(currentVel, 0);
           // currentVel = currentVel - acel * Time.deltaTime;

        }
    }
}
