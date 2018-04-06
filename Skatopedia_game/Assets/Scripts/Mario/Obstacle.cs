using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    //public float actionDistance;
    public float objectPoints;
    public float objectVel;
    public bool animationCam;
    public bool animationTable;
    public static Player PC;
    public float timeLimit;
    public Object CollisionObject;
    public bool impact = false;
    protected string type;
   // public Collider2D collision;

    // Use this for initialization
    void Start () {
        //PC = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }


    public IEnumerator Reaction(Collider2D collision, string type)
    {


        yield return null;


        }

        public IEnumerator VelMin()
    {
      
        if (PC.currentVel< objectVel && !PhysicsObject.gameOver)
        {
            yield return new WaitForSeconds(timeLimit);
            PhysicsObject.gameOver = true;
            Debug.Log("GAMEOVER MINVEL");

        }
        yield return null;
    } 


    // Update is called once per frame
    void Update () {
       
	}
}
