using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour {

    //KINEMATIC VARIABLES 
    protected bool jump = false;
    protected float JumpForce = 10;
    public float MaxVel = 10f;
    //public float currentVel=10f;
    protected float acel = 0.3f;
    protected float VelThreshold = 0f;
    protected float FlexVel = 18f;
    //protected Vector3 perpendicular;
    protected Vector2 TableCM=new Vector2(0f, 0.065f);
    public Vector3 perpendicular;
    protected float BreakTime;

    //STATE VARIABLES
    public bool onFloor;
    public bool onGrind = false;
    protected Rigidbody2D rb2d;
    public bool gameOver=false;

  
    //OBJECT VARIABLES
    protected Animator anim;
    protected Animation animation;
    protected int  layerFilter;
    protected float layerDistanceDetect=1f;
    protected float maxVel = 10f;
    static public GameObject Player;
    private Player PlayerScript;
  

    //LOCAL FUNCTIONS____________________________________________________________________________________________________________________________________________________________________

    //FUNCTIONS TO FREEZE OR UNFREEZE THE ROTATION---------------------------------------------------------------------------------------------------------------------------

    protected void FreezeConstraints()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    protected void UnFreezeConstraints(RigidbodyConstraints2D originalConstraints)
    {
        GetComponent<Rigidbody2D>().constraints = originalConstraints;
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------


    protected void OnEnable()
    {
        Player= GameObject.FindGameObjectWithTag("Player");
        rb2d = Player.GetComponent<Rigidbody2D>();
        Debug.Log(Player.name);
    }

    void Start()
    {  
        layerFilter = LayerMask.GetMask("Floor", "Grind");
        anim = Player.GetComponent<Animator>();
        PlayerScript = Player.GetComponent<Player>();
    }

    void Update()
    {
      //  ComputeVelocity();
    }

    //protected virtual void ComputeVelocity()
    //{
      //  currentVel = PlayerScript.currentVel;
    //}

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Collision") && !gameOver)
        {
           
            // Debug.Break();

            gameOver = true;
            anim.SetBool("gameOver", gameOver);
            // StartCoroutine(End());
            //Debug.Break();
            Debug.Log("GAMEOVER COLLISION: TIEMPO=" + Time.realtimeSinceStartup);
        }
        //    yield return new WaitForFixedUpdate();
    }


    void OnTriggerStay2D(Collider2D other)
    // IEnumerator OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        { 
             onFloor = true;
             MovementAlongFloor();
             jump = false;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Grind"))
        {
            onGrind = true;
            MovementAlongFloor();
            jump = false;
        }
       // yield return new WaitForFixedUpdate();
    }

    


    void FixedUpdate()
    {
        onFloor = false;
        onGrind = false;

    }

   public  void MovementAlongFloor()
    {
         Vector2 CM = new Vector2(transform.position.x- TableCM.x, transform.position.y - TableCM.y);
         RaycastHit2D hit = Physics2D.Raycast(new Vector3 (CM.x,CM.y,0), -transform.up, layerDistanceDetect, layerFilter, Mathf.Infinity);
       
        // Debug.DrawLine(hit.point,CM,Color.green);
        Debug.DrawRay(new Vector3(CM.x, CM.y, 0), -transform.up * layerDistanceDetect, Color.green);
        if ((hit.collider!=null) &&  (onFloor||onGrind))
         {
            Vector2 currentNormal = hit.normal;
             perpendicular = Vector3.Cross(new Vector3 (currentNormal.x, currentNormal.y,0), new Vector3(0,0,1)).normalized;
            Debug.DrawRay(new Vector3 (CM.x,CM.y,0), - transform.up * layerDistanceDetect, Color.green);
             Debug.DrawLine(new Vector3(CM.x, CM.y, 0), new Vector3(CM.x, CM.y, 0) + perpendicular.normalized *PlayerScript.currentVel , Color.blue);
            PlayerScript.perpendicular = perpendicular;
             rb2d.velocity = perpendicular * PlayerScript.currentVel;
        }
     
    }

    public void Allig2Floor(Vector3 perpendicular, GameObject Player)
    {
      //   Player.transform.right = Vector3.Lerp(Player.transform.right, perpendicular, Time.deltaTime * 100);
        Player.transform.right =perpendicular;
    }


}

