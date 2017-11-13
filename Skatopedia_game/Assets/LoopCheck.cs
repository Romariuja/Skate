using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopCheck : MonoBehaviour {
    private int check=0;
   
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Table")
        {
            check++;
            Debug.Log("Check" + check);
                Debug.Break();
            if (check == 2)
            {
                //StartCoroutine(TargetLoop());
                Debug.Log("SLOW MOTION");
                Time.timeScale = 0.2F;
            }
           

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
