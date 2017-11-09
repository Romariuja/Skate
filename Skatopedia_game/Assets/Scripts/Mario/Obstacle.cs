using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    public float actionDistance;
    public float objectPoints;
    public float objectVel;
    public bool animationCam;
    public bool animationTable;
    public Player PC;

    // Use this for initialization
    void Start () {
        PC = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    // Update is called once per frame
    void Update () {
		
	}
}
