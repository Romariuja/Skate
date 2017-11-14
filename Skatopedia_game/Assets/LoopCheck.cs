using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopCheck : MonoBehaviour {
    private int check=0;
   
   void OnTriggerEnter2D(Collider2D collision)
    {
        //yield return new WaitForFixedUpdate();
        if (collision.gameObject.tag == "Table")
        {
       //     yield return new WaitForFixedUpdate();
            Debug.Log("Antes_Check" + check);
            check=check+1;
            Debug.Log("Despues_Check" + check);
            //Debug.Break();
         
            if (check == 2)
            {
                //StartCoroutine(TargetLoop());
                Debug.Log("SLOW MOTION");
                Time.timeScale = 0.2F;
               
            }
             else if (check == 4)
            {
                Time.timeScale = 1F;
            }
          //  yield return new WaitForFixedUpdate();
            //Without the "WaitForFixedUpdate() it increments twice check value entering only on one collider"
            //  GetComponent<Collider2D>().enabled = false;
        }
        

    }

    // Use this for initialization
    void Start () {
        int check = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
