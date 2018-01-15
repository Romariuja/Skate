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
    public Vector3 perpendicular=new Vector3(1,0,0);
    protected float BreakTime;
    protected float rotationVel=5;

    //STATE VARIABLES
    public bool onFloor;
    public bool onGrind = false;
    protected Rigidbody2D rb2d;
    public static bool gameOver=false;
    public static bool levelOver = false;

  
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
       // Debug.Log(Player.name);
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
       
  
     //   Debug.DrawRay(new Vector3(CM.x, CM.y, 0), -transform.up * layerDistanceDetect, Color.green);
        if ((hit.collider!=null) &&  (onFloor||onGrind) && (!levelOver && !gameOver))
         {
            Vector2 currentNormal = hit.normal;
            Debug.DrawLine(CM,hit.point, Color.blue);
            Debug.DrawLine( hit.point,hit.point+hit.normal*10f, Color.blue);
            perpendicular = Vector3.Cross(new Vector3 (currentNormal.x, currentNormal.y,0), new Vector3(0,0,1)).normalized;
           
            //   Debug.DrawRay(new Vector3 (CM.x,CM.y,0), - transform.up * layerDistanceDetect, Color.green);
            //  Debug.DrawLine(new Vector3(CM.x, CM.y, 0), new Vector3(CM.x, CM.y, 0) + perpendicular.normalized *PlayerScript.currentVel , Color.blue);

            PlayerScript.perpendicular = perpendicular;
             rb2d.velocity = perpendicular * PlayerScript.currentVel;
        }

    }


    //This returns the angle in radians
    public static float AngleInRad(Vector3 vec1, Vector3 vec2)
    {
        return Mathf.Atan2(vec2.y - vec1.y, vec2.x - vec1.x);
    }

    //This returns the angle in degrees
    public static float AngleInDeg(Vector3 vec1, Vector3 vec2)
    {
        return AngleInRad(vec1, vec2) * 180 / Mathf.PI;
    }

    public void Allig2Floor(Vector3 perpendicular, GameObject Player)
    {
        //   Player.transform.right = Vector3.Lerp(Player.transform.right, perpendicular, Time.deltaTime * 100);
        float dif = Vector3.Angle(transform.right, perpendicular);
       float dif2 = AngleInDeg(perpendicular, transform.right);
        Debug.Log("perpendicular: " + perpendicular + " right vector player: " + Player.transform.right + " .Diferencia Angular: " + dif);
        Debug.Log("perpendicular: " + perpendicular + " right vector player: " + Player.transform.right + " .Diferencia Angular2: " + dif);

        //ALLIGN ANGLE CONDITION

        if (Mathf.Abs(dif) > 50)
        {

            Debug.Log("ANGULO A ALINEAR DEMASIADO GRANDE-> GAMEOVER?");
          //  Debug.Break();
            gameOver = true;
        
        }
        else { Player.transform.right = perpendicular; }
        // Debug.Break();
       
    }


}

