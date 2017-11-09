using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {
    public Vector3 CM=new Vector3 (0,-0.1f,0);
    public float T=0;
    public GameObject Skater;
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        T = T + Time.deltaTime;
        if (T%20 < 8)
            transform.Rotate(new Vector3(0, 0, 10f));
        else { 
            Debug.DrawLine(transform.position, Skater.transform.position, Color.red);
            transform.RotateAround(Skater.transform.position,new Vector3 (0,0,1),-10);
        }
    }
}
