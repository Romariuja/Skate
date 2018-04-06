using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dog :Obstacle {

    float Vel = 5;
    float currentVel =5.25f;
    float acel = 1f;
    private bool hit = false;
    //private Player_Controller PC;

    void OnCollisionEnter2D(Collision2D collision)
    {
        //  Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "stopPoint" || PhysicsObject.gameOver == true)
            animationCam = false;

        if (collision.gameObject.tag == "Player")
        {
            //  StartCoroutine(Reaction(collision, "lightweight"));
            //  yield return new WaitForSeconds(0.1f);
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            hit = true;

        }
}
    // Use this for initialization
    void Start()
    {
        animationCam = false;
       // PC = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animationCam  && !hit )
        {
           transform.GetComponent<Rigidbody2D>().velocity = new Vector2(currentVel, 0);
          

            // currentVel = currentVel - acel * Time.deltaTime;

        //}
    }
}
}