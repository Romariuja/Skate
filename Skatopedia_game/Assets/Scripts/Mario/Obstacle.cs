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
    public float timeLimit;

    // Use this for initialization
    void Start () {
        //PC = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    public IEnumerator VelMin()
    {
      
        if (PC.currentVel< objectVel && !PC.gameOver)
        {
            yield return new WaitForSeconds(timeLimit);
            PC.gameOver = true;
            Debug.Log("GAMEOVER MINVEL");

        }
        yield return null;
    } 


    // Update is called once per frame
    void Update () {
       
	}
}
