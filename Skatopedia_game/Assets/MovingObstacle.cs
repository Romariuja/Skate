using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle :Obstacle {


    public float currentVelx = 0.8f;
    public float currentVely = 0.6f;
    public float time = 10f;
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
        if (animationCam && T<time)
        {
            transform.position = transform.position+ new Vector3(currentVelx * Time.deltaTime, currentVely * Time.deltaTime,0);
            // currentVel = currentVel - acel * Time.deltaTime;
            T = T + Time.deltaTime;
         //   Debug.Log(T/time*100);

        }
    }
}
