using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
       Player.LevelScript.UpdateTarget(Level.TargetList, "Unicorn Horn Grind");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
