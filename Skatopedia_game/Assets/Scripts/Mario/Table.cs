using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour {


    GameObject Player;
    void OnEnable() {
      
    }
	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
