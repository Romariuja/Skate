using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loop : Obstacle {

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Table")
        {
            GetComponent<Collider2D>().enabled = false;
        }

    }


    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Table")
        {
          StartCoroutine(VelMin());
            //   Debug.Log("Activa el collider de la banderola " + gameObject.name);
            // gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            //Debug.Log("Activa el collider de la banderola " + gameObject.name +" Se ha activado?" + gameObject.GetComponent<Rigidbody2D>().bodyType);
        }

      

    }

    void Start()
    {
        objectVel =12;
        timeLimit = 0.7f;
        animationCam = false;
        PC = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
   
    }
}
