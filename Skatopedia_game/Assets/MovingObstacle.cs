using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle :Obstacle {


    float currentVelx = 0;
    float currentVely = 2;
    float time = 5f;
    float T;
    //private Player_Controller PC;

    // Use this for initialization
    void Start()
    {
        animationCam = false;
        //PC = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animationCam && !PhysicsObject.gameOver && T<time)
        {
            transform.position = transform.position+ new Vector3(currentVelx * Time.deltaTime, currentVely * Time.deltaTime,transform.position.z);
            // currentVel = currentVel - acel * Time.deltaTime;
            T = T + Time.deltaTime;

        }
    }
}
