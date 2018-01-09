using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PhysicsObject {

    //STATE VARIABLES
    public bool Flex = false;
    protected List<transition> transitionsList;
    public float currentVel;

    //INPUT KEY VARIABLES
    int KeyUp = Animator.StringToHash("KeyUp");
    int KeyDown = Animator.StringToHash("KeyDown");
    int KeyRight = Animator.StringToHash("KeyRight");

    //OBJECT VARIABLES
    public RigidbodyConstraints2D originalConstraints;
    public static GameObject cam;
    public GameObject camGameOver;
    public GameObject Table;
    public GameObject Skater;
    private Collider2D col;
    Collider2D[] SkaterColliders;
    Collider2D[] TableColliders;
    //private Obstacle obstacle;
    //private Rigidbody2D rb2d;
    private PhysicsObject TableScript;
    public static ControladorCamara CameraScript;

    public Animation animation;
   
    //SCORE VARIABLES
    float Tiempo0;
    public static Puntuacion puntua;
    public  static int combo = 0;
    public static int PuntosGrind = 0;
    private int Grind_5_0Points = 3;
    private int Grind_50_50Points = 1;
    private int OlliePoints = 200;
    private int KickFlipPoints = 300;
    private int GrabTrickPoints = 400;
    private int ManualPoints = 0;
    public int contGrind = 1;
    public GameObject TableCrippled;
    public static CustomImageEffect EffectCam;

    void awake()
    {
        originalConstraints = GetComponent<Rigidbody2D>().constraints;
       
    }

    // Use this for initialization
    void Start() {

        

        Puntuacion.puntuacion=0;
    //  a.wrapMode = WrapMode.Loop;

    //animation = GetComponent<Animation>();
    //animation["CrippledM"].wrapMode = WrapMode.Once;
    anim = GetComponent<Animator>();
        //Table = GameObject.FindGameObjectWithTag("Table");
        //Debug.Log("Table es: " + Table.transform.parent.parent.name);
        TableScript = GetComponent<PhysicsObject>();
        Debug.Log(TableScript.gameObject);
        //TableCrippled = GameObject.FindGameObjectWithTag("TableCrippled");
        //Skater = GameObject.FindGameObjectWithTag("Skater");
        //Debug.Log("Skater es: " + Skater.transform.parent.parent.name);
        SkaterColliders = Skater.GetComponents<Collider2D>();
        //Debug.Log(SkaterColliders);
        TableColliders = Table.GetComponents<BoxCollider2D>();
        //Debug.Log(TableColliders);
        cam = GameObject.FindWithTag("MainCamera");
       
        puntua = cam.GetComponent<Puntuacion>();

        CameraScript=cam.GetComponent<ControladorCamara>();


        EffectCam = cam.GetComponent<CustomImageEffect>();
        transitionsList = new List<transition>();
        transitionsList.Add(new transition("Idle", false));
        transitionsList.Add(new transition("Grind", false));
        transitionsList.Add(new transition("Jump", false));
        transitionsList.Add(new transition("Manual", false));

        //Debug.Log("LA tabla es" + Table.name + ",y esta en el obbjeto padre " + Table.transform.parent.parent + "En la posicion " + Table.transform.localPosition );
    }

    //LOCAL FUNCTIONS____________________________________________________________________________________________________________________________________________________________________

    //UPDATE TRANSITIONS-----------------------------------------------------------------------------------------------------------------------------------------------------------
    protected class transition
    {
      public string name;
      public bool init;
      public transition(string newName, bool newInit)
      {
         name = newName;
         init = newInit;
      }   
    }

      protected void UpdateTransition(List<transition> TransitionList, string Name, bool Init)
      {

        for (int i=0; i < TransitionList.Count; i++)
        {
            if (TransitionList[i].name == Name)
            {
            //   Debug.Log("Ha activado al transicion" + TransitionList[i].name + Init);
             //  Debug.Break();
                 TransitionList[i].init=Init;
            }
            else
            {
                 TransitionList[i].init = false;
            }
        }
    }


    protected bool TestTransition(List<transition> TransitionList, string Name)
    {
     
        for (int i = 1; i < TransitionList.Count; i++)
        {
            if (TransitionList[i].name == Name)
            {
                return TransitionList[i].init ;
            }                     
        }
        return false;
    }
    //ROTATE FUNCTION-----------------------------------------------------------------------------------------------------


    void RotateAir()
    {
        if (Input.GetKey("right"))
        {
            transform.RotateAround(Skater.transform.position, new Vector3(0, 0, 1), -5 );
        }
        else if (Input.GetKey("left"))
        {
            transform.RotateAround(Skater.transform.position, new Vector3(0, 0, 1), +5 );
        }
        
    }


    //END COROUTINE. LAUNCH END ANIMATION---------------------------------------------------------------------------------------------------------------------------------------
    public IEnumerator End()
    {

        //CHANGE COLLIDERS TO DETECT SKATER COLLISIONS
        foreach (Collider2D bc in SkaterColliders)
        {
            bc.isTrigger = false;
        }
        foreach (Collider2D bc in TableColliders)
        {
            bc.isTrigger = true;
        }
        //Rigidbody2D rb2d = Skater.GetComponent<Rigidbody2D>();
        UnFreezeConstraints(originalConstraints);
        //Skater.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic
        TableCrippled.SetActive(true);
        TableCrippled.transform.parent = null;
        TableCrippled.GetComponent<Rigidbody2D>().simulated = true;
        
        Table.SetActive(false);      
        TableCrippled.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
  //      yield return new WaitForSeconds(0.01f);
        TableCrippled.GetComponent<Rigidbody2D>().simulated = true;
        TableCrippled.GetComponent<Rigidbody2D>().velocity = new Vector2(maxVel, 2 * maxVel / 3);
        TableCrippled.GetComponent<Rigidbody2D>().AddTorque(1, ForceMode2D.Impulse);
        Debug.Log("GAMEOVER RUTINE: TIEMPO=" + Time.realtimeSinceStartup);
        if (Puntuacion.puntuacion > EstadoJuego.estadoJuego.puntuacionMaxima)
        {
            EstadoJuego.estadoJuego.puntuacionMaxima = Puntuacion.puntuacion;
            Debug.Log("Nuevo record" + Puntuacion.puntuacion);
            EstadoJuego.estadoJuego.Guardar();
        }
        //anim.StopPlayback();

        yield return new WaitForSeconds(3);
        camGameOver.gameObject.SetActive(true);
        cam.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    //SLOW DOWN COROUTINE
    public IEnumerator slowDown()
    {
        while (BreakTime < 1)
            { 
        BreakTime = BreakTime + 0.1f * Time.deltaTime;
            Debug.Log("BreakTime: " + BreakTime + " rb2d.velocity: "  +rb2d.velocity +"LevelOver" +levelOver);
        rb2d.velocity = new Vector2(Mathf.Lerp(rb2d.velocity.x, 0, BreakTime), rb2d.velocity.y);
            yield return null;
        }
    }


    //______________________________________________________________________________________________________________________________________________________________________________________

    // Update is called once per frame
    void Update ()
    {

        //RESET VARIABLES
        AnimatorTransitionInfo CurrentTransition;
        CurrentTransition = anim.GetAnimatorTransitionInfo(0);
        anim.ResetTrigger("KeyUp");
        // anim.ResetTrigger("KeyDown");
        anim.ResetTrigger("KeyRight");
        anim.SetBool("onFloor", onFloor);
        anim.SetBool("onGrind", onGrind);
        anim.SetBool("jump", transitionsList[2].init);
        anim.SetBool("gameOver", gameOver);

        //STATE MACHINE-------------------------------------------------------------------------------------------------------------------------------------
       // if (rb2d.velocity.magnitude < MaxVel / 3 && (Time.timeSinceLevelLoad>4))
        //{
          //  gameOver = true;
        //}
        //GAMEOVER STATE
         if (anim.GetCurrentAnimatorStateInfo(0).IsName("CrippledM"))
        {
        
            if (gameOver)
            {
                Debug.Log("GAMEOVER STATE: TIEMPO=" + Time.realtimeSinceStartup);
                combo = 0;
                puntua.IncrementarCombo(combo, "Loquesea", 0);
                StartCoroutine(End());
                gameOver = false;
            }
            // BreakTime = BreakTime + Mathf.Pow(10f * Time.deltaTime, 1.2f);

            StartCoroutine(slowDown());
         //   BreakTime = BreakTime + 0.01f * Time.deltaTime;
           // rb2d.velocity = new Vector2(Mathf.Lerp(rb2d.velocity.x, 0, BreakTime), rb2d.velocity.y);

        }
        else

        //IDLE STATE
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            if (!transitionsList[0].init)
            {
                //Debug.Log("IDLE SOLO UNA VEZ");
                TableScript.MovementAlongFloor();
                Allig2Floor(perpendicular, gameObject);
                anim.ResetTrigger("KeyDown");
                combo = 0;
                puntua.IncrementarCombo(combo, "Loquesea", 0);
                UnFreezeConstraints(originalConstraints);
                UpdateTransition(transitionsList, "Idle", true);

            }
            currentVel = Mathf.Max(currentVel - acel, MaxVel);
        }

        //GRIND STATE
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Grind_50_50"))
        {
            if (!transitionsList[1].init)
            {
                Allig2Floor(perpendicular, gameObject);
                anim.ResetTrigger("KeyDown");
                combo++;
                UnFreezeConstraints(originalConstraints);
                UpdateTransition(transitionsList, "Grind", true);
            }
            currentVel = Mathf.Max(currentVel - acel, MaxVel);
            PuntosGrind = Grind_50_50Points;
            puntua.IncrementarCombo(combo, "Grind_50_50", Grind_50_50Points * contGrind);
            contGrind++;
        }

        //OLLIE STATE
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Ollie"))
        {
            if (!transitionsList[2].init)
            {
                UpdateTransition(transitionsList, "Jump", true);
                combo++;
                puntua.IncrementarCombo(combo, "Ollie", OlliePoints);
                FreezeConstraints();
                anim.SetBool("jump", transitionsList[2].init);
            }
          //RotateAir();
        }

        //GRAB STATE
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("GrabTrick"))
        {
            if (!transitionsList[2].init)
            {
                UpdateTransition(transitionsList, "Jump", true);
                combo++;
                puntua.IncrementarCombo(combo, "GrabTrick", OlliePoints);
                FreezeConstraints();
                anim.SetBool("jump", transitionsList[2].init);
            }
           // RotateAir();
        }

        //AIR STATE
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("FlexAir"))
        {
            if (!transitionsList[2].init)
            {
                FreezeConstraints();
                UpdateTransition(transitionsList, "Jump", true);
            }

            RotateAir();



        }

        //MANUAL STATE
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Manual"))
        {
            if (!transitionsList[3].init)
            {              
                UnFreezeConstraints(originalConstraints);
                // currentVel = Mathf.Max(currentVel - acel, MaxVel);
                UpdateTransition(transitionsList, "Manual", true);
            }
            currentVel = Mathf.Max(currentVel - acel, MaxVel);
        }

        //FLEX STATE
       else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Flex"))
        {
            combo = 0;
            currentVel = Mathf.Min(currentVel + acel, FlexVel);
            UnFreezeConstraints(originalConstraints);
            // currentVel = Mathf.Max(currentVel - acel, MaxVel);
            UpdateTransition(transitionsList, "Manual", true);
        }

        
        //CHECK KEY INPUT
        if (Input.GetKeyDown("up") && (onFloor || onGrind) && jump == false)
        {
            anim.SetTrigger("KeyUp");
            rb2d.velocity = new Vector2(rb2d.velocity.x, JumpForce);
        }

        else if (Input.GetKeyDown("right") && (onFloor || onGrind) && !jump)
        {
            anim.SetTrigger("KeyRight");
            rb2d.velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, JumpForce * 1.2f);
        }

        else if (Input.GetKeyDown("down"))
        {
            //anim.SetBool("KeyDown",true);
            anim.SetTrigger("KeyDown");
        }
    }

}
