using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun_controller : MonoBehaviour {
    private float difVelx = 0.25f;
    private float difVely = 0.35f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody2D>().velocity = new Vector3(ControladorCamara.player.GetComponent<Rigidbody2D>().velocity.x-difVelx,-difVely,0) ;
	}
}
