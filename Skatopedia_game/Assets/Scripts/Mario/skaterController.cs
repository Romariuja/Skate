using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skaterController : MonoBehaviour {

    private Player_Controller PC;
  

    
    void Start()
    {
        PC = transform.parent.parent.GetComponent<Player_Controller>();
    }

    // TRIGGER COLLIDER FUNCTIONS
    //TO DETECT OBJECTS COLLISIONS___________________________________________________________________________________________________________________________________________________________
    void OnTriggerEnter2D(Collider2D collider)
    {

        if ((collider.gameObject.layer == LayerMask.NameToLayer("Collision")) && !PC.GameOver)
        {
            PC.GameOver = true;
            Debug.Log("FINAL3. Skater collider hits Collision");
        }
      

    }

    void OnTriggerStay2D(Collider2D collider)
    {
      
    }

    void OnTriggerExit2D(Collider2D collider)
    {

     
    }

    
	
	// Update is called once per frame
	void Update () {
		
	}
}
