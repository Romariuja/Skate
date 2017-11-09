using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour {
   
    //STATE VARIABLES
	public bool onFloor=false;
    public bool onGrind = false;
    public bool Flex=false; 
    public bool Idle = false;
    public bool outGrind = false;
    public bool inGrind = false;
    public bool GameOver = false;
    public bool Grind_50_50 = false;
    public bool Grind_5_0 = false;
    public bool manual = false;
    public bool manualair = false;
    public bool air;
 
   

	//KINETIC VARIABLES
    public  float MaxVel = 10f;
    private float hillVelFactor = 10;
    public  float currentVel;
    private float acel = 0.1f;
    public  float VelThreshold =0f;
    private float MaxAngle = 75;
    private float rotationSpeed = 40.0f;
    public float FlexVel = 16f;
    private float BreakTime = 0;
    private float JumpForce = 5f;
    float GrabForce = 8f;
    private Vector2 tangent2D;
    public float delta;
    //private float Angle;
    private RigidbodyConstraints2D originalConstraints;



    //OBJECT VARIABLES
    public LayerMask FloorMask;
    public LayerMask GrindMask;
    public LayerMask Player;
    public PlayClip MoviePlayer;
    public PlayClip MoviePlayerTable;
    private Collision2D collision;
    public GameObject cam;
    public GameObject camGameOver;
    public GameObject Table;
    public GameObject TableLocator;
    public GameObject Skater;
    private Collider2D col;
    Collider2D[] SkaterColliders;
    Collider2D[] TableColliders;
    private Obstacle obstacle;
    HarmonyAnimation animation;
    public List<AnimationClip> ListaAnimaciones;



    //SCORE VARIABLES
    float Tiempo0;
    public Puntuacion puntua;
    public static int combo = 0;
    public static int PuntosGrind = 0;
    private int Grind_5_0Points=3;
    private int Grind_50_50Points = 1;
    private int OlliePoints = 200;
    private int KickFlipPoints = 300;
    private int GrabTrickPoints = 400;
    private int ManualPoints = 0;
    public  int contGrind =1;
    private float distanceToGround;

    //ANIMATION VARIABLES
    private bool launched=false;

    // Use this for initialization

    void Start () {

        //ASIGN GAMEOBJECTS VARIABLES  
        MoviePlayer =gameObject.GetComponent<PlayClip>();
        MoviePlayerTable = Table.GetComponent<PlayClip>();
        cam = GameObject.FindWithTag("MainCamera");
        puntua = cam.GetComponent<Puntuacion>();
        SkaterColliders = Skater.GetComponents<Collider2D>();
        TableColliders = TableLocator.GetComponents<BoxCollider2D>();
        FreezeConstraints();
        animation = GetComponent<HarmonyAnimation>();
        Tiempo0 = Time.time;

    }



    void awake()
    {
        
        originalConstraints = GetComponent<Rigidbody2D>().constraints;
    }

    //FUNCTIONS TO FREEZE OR UNFREEZE THE ROTATION---------------------------------------------------------------------------------------------------------------------------

    void FreezeConstraints()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void UnFreezeConstraints(RigidbodyConstraints2D originalConstraints)
    {
        GetComponent<Rigidbody2D>().constraints = originalConstraints;
    }


     //LOCAL FUNCTIONS____________________________________________________________________________________________________________________________________________________________________

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
         //Skater.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
       MoviePlayer.Anime("SkaterPro_CrippledM", false, true);
         Table.SetActive(true);
         Table.transform.parent = null;
         Table.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
         yield return new WaitForSeconds(0.3f);
         Table.GetComponent<Rigidbody2D>().velocity = new Vector2(MaxVel, 2 * MaxVel / 3);
         Table.GetComponent<Rigidbody2D>().AddTorque(1, ForceMode2D.Impulse);
         if (Puntuacion.puntuacion > EstadoJuego.estadoJuego.puntuacionMaxima)
         {
             EstadoJuego.estadoJuego.puntuacionMaxima = Puntuacion.puntuacion;
             Debug.Log("Nuevo record" + Puntuacion.puntuacion);
             EstadoJuego.estadoJuego.Guardar();
         }

         yield return new WaitForSeconds(5);
         camGameOver.gameObject.SetActive(true);
         cam.gameObject.SetActive(false);
         gameObject.SetActive(false);
     }



     //LOCAL GRAVITY FUNCTION
      public virtual float Local_Gravity(float Vel) {
      // IEnumerator Local_Gravity(float Vel) {  
         Vector2 Dir = -TableLocator.transform.up;
         Vector2 Dirglob=-transform.up;
         Vector2 CM = TableLocator.transform.position + TableLocator.transform.right.normalized * 0f- TableLocator.transform.up.normalized * 0.085f;
         //Vector2 VelMoving;
         RaycastHit2D hit = Physics2D.Raycast(CM, Dir,1.5f);
         float Angle;
         float deltaAngle=0f;
         Vector3 v3Current = transform.eulerAngles;
        //  Debug.Log("Posicion inicial"+ TableLocator.transform.position +" direccion compensacion "+ TableLocator.transform.forward.normalized * 20 + "CM compensado" + CM);
        // Debug.Log(hit.collider.gameObject.layer);
        if (hit.collider!=null && (hit.collider.gameObject.layer != LayerMask.NameToLayer("Collision") && hit.collider.gameObject.layer != LayerMask.NameToLayer("Default")) && !GameOver)
         {
            
            var distanceToGround = hit.distance;
             Vector3 tangent = Vector3.Cross(hit.normal, Vector3.forward);
             tangent2D = new Vector2(tangent.x, tangent.y);

             if (tangent.magnitude == 0)
             {
                 tangent = Vector3.Cross(hit.normal, Vector3.up);
                 tangent2D = new Vector2(tangent.x, tangent.y);
             }


             Debug.DrawLine(CM, hit.point, Color.red);
             //   Debug.DrawLine(TableLocator.transform.position, hit.point, Color.green);
             Angle = Mathf.Max((Mathf.Atan(tangent2D.y / tangent2D.x) * 180 / Mathf.PI) % 360, (Mathf.Atan(tangent2D.y / tangent2D.x) * 180 / Mathf.PI + 360) % 360);
             deltaAngle = Angle - transform.eulerAngles.z;

             if (Mathf.Abs(deltaAngle) >= 90)
             {
                 //NO FUNCIONA BIEN
                // Debug.Log("HAy que corregir el angulo de orientacion que es" + deltaAngle);
                 deltaAngle = (deltaAngle - Mathf.Sign(deltaAngle) * 360) % 360;
                 if (Mathf.Abs(deltaAngle) > 90)
                 {
                     deltaAngle = Mathf.Sign(deltaAngle) * (Mathf.Abs(deltaAngle) - 180);
                 }
              //   Debug.Log("El angulo corregido  es" + deltaAngle);
                 // Angle = (Angle + 360)%361;
             }


             Vector3 v3To = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, (transform.eulerAngles.z + deltaAngle));
            Vector2 VelHill=new Vector2(tangent2D.x, tangent2D.y).normalized * Mathf.Clamp((- (int)hillVelFactor * Mathf.Sin(Angle * Mathf.PI / 180)),0, hillVelFactor/5);
            if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
            { 
                obstacle = hit.collider.gameObject.transform.GetComponent<Obstacle>();
                //  Debug.Log("El Script es:" + obstacle);
                //obstacle.animationCam = true;
                //      Debug.Log("Velocidad del objecto movie3ndose" + hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity);
                
            VelHill =VelHill+hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity;
            }
            GetComponent<Rigidbody2D>().velocity = new Vector2(tangent2D.x, tangent2D.y).normalized * (Vel)+VelHill;

            Debug.DrawRay(TableLocator.transform.position, tangent, Color.red);
        
            float difdelta = Mathf.Sign(delta) * Mathf.Min(Mathf.Abs(delta/5), rotationSpeed);
            Debug.DrawRay(transform.position,Dir,Color.blue);
          //  transform.Rotate(new Vector3(0, 0, difdelta));
            transform.RotateAround(Table.transform.position, new Vector3(0, 0, 1), difdelta);
            // GetComponent<Rigidbody2D>().AddForce(Dir);
            //  Debug.Log("Se ha rotado " + difdelta);
            // transform.eulerAngles = v3To;
            // transform.Rotate(0, 0, deltaAngle);
            return deltaAngle;
             /*  while (transform.eulerAngles.z != Angle) { 
                   yield return null;

                   }*/
}
        else
            return deltaAngle;
    }



    void Update()
    {
       // if (!onFloor)
       //Debug.Log("OnFloor " +onFloor + " outGrind" + outGrind);
        //THE GAME IS OVER_____________________________________________________________________________________________________________________________________________________________
        if (GameOver)
        {
            if (!launched) {
                combo = 0;
              //  MoviePlayer.Anime("SkaterPro_CrippledM", false, true);
                puntua.IncrementarCombo(combo, "Loquesea", 0);
                StartCoroutine(End());
                launched = true;
                return;
            }
          
            BreakTime = BreakTime + Mathf.Pow(10f * Time.deltaTime, 1.2f);
           // Debug.Log("Velocidad " +Mathf.Lerp(GetComponent<Rigidbody2D>().velocity.x, 0, BreakTime)+" " +BreakTime *100f +"%");
            //delta = Local_Gravity(Mathf.Lerp(GetComponent<Rigidbody2D>().velocity.x, 0, BreakTime));
            delta = Local_Gravity(0);

            // GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Lerp(GetComponent<Rigidbody2D>().velocity.x, 0, BreakTime), GetComponent<Rigidbody2D>().velocity.y);
            Table.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Lerp(Table.GetComponent<Rigidbody2D>().velocity.x, 0, BreakTime), Table.GetComponent<Rigidbody2D>().velocity.y);
            return;
        }

        //CHECK IF THE GAME HAS TO FINISH_______________________________________________________________________________________________________________________________________________
        else {

        
        //     if ((Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) < VelThreshold && Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) < VelThreshold) && Time.time > 3)
          //   {    
            //     GameOver = true;
              //  Debug.Log("FINAL4_VELOCIDAD PARCIAL<LIM");
                //return;     
            // }
            if ((Mathf.Abs(GetComponent<Rigidbody2D>().velocity.sqrMagnitude) < VelThreshold) && Time.time > 3+Tiempo0)
            {
                GameOver = true;
                Debug.Log("FINAL1_VELOCIDAD GLOBAL<LIM");
                return;
            }
               

            //THE GAME DOESN'T HAVE TO FINISH. STATE'S DIAGRAM CONDITIONS________________________________________________________________________________________________________________________
            else 
                //GRINDING MOVES
                //SPECIAL GRINDING STATE
                if (onGrind && Input.GetKeyDown("down")  && !Grind_5_0)
                {
             //   Debug.Log("ESTADO 1");
                      MoviePlayer.Anime("SkaterPro_Grind_5_0", true, true);
             
                      PuntosGrind = Grind_5_0Points;
                      puntua.IncrementarCombo(combo, "Grind_5_0", Grind_5_0Points * contGrind);
                      contGrind++;       
                      currentVel = Mathf.Max(currentVel - acel, MaxVel);           
                      Idle = false;         
                      manual = false;
                      Grind_5_0 = true;
                      air = false;
                      return;
                }

                
                //GRINDING STATE.
                else if (onGrind && !Grind_5_0 && !GameOver)
                {
                //Debug.Log("Se lanza animacion Grind_50_50???" +inGrind);
                  // Debug.Log("ESTADO 2");
                if (inGrind) { 
                  //  Debug.Log("Se lanza animacion Grind_50_50!!!!!");
                         MoviePlayer.Anime("SkaterPro_Grind_50_50", true,true);
                         inGrind = false;
                         combo++;
                     }
              
                     PuntosGrind = Grind_50_50Points;           
                     puntua.IncrementarCombo(combo, "Grind_50_50", Grind_50_50Points * contGrind);
                     contGrind++;
                     currentVel = Mathf.Max(currentVel - acel, MaxVel);
                     delta = Local_Gravity(currentVel);
                  // Debug.Log("Grindando");
                     manual = false;
                     Idle = false;
                     air = false;
            }
                //ALL OF THIS MOVES HAVE ONFLOOR TRUE SO THEY HAVE TO LAUNCH LOCAL GRAVITY----------------
                //JUMPING MOVES
                 //OLLIE STATE
                  if ((onFloor||onGrind) && Input.GetKeyDown("up"))
                  {
                MoviePlayer.Anime("SkaterPro_Ollie", false,true);
                      air = true;
                      GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, JumpForce);
                      Idle = false;
                      manual = false;
                      outGrind = false;
         
                combo++;
                      puntua.IncrementarCombo(combo, "Ollie", OlliePoints);
                  }

                  //KICKFLIP STATE
                   else if ((onFloor || onGrind) && Input.GetKeyDown ("left"))
                   {
         //       Debug.Log("ESTADO 4");
                MoviePlayer.Anime("SkaterPro_KickFlip", false,true);
                       air = true;
                       GetComponent<Rigidbody2D>().velocity=new Vector2(GetComponent<Rigidbody2D>().velocity.x, JumpForce);
                       Idle = false;
                       manual = false;
                       outGrind = false;
                       combo++;
                       puntua.IncrementarCombo(combo, "KickFlip", KickFlipPoints);
                   }

                  //GRABTRICK STATE
                  else if ((onFloor || onGrind) && Input.GetKeyDown("right"))
                  {
         //       Debug.Log("ESTADO 5");
                MoviePlayer.Anime("SkaterPro_GrabTrick", false,true);
                        combo++;
                        puntua.IncrementarCombo(combo, "GrabTrick", GrabTrickPoints);
                        air = true;
                        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, GrabForce);
                        Idle = false;
                        manual = false;
                        outGrind = false;
                  }

                //MANUAL FROM THE AIR
       
                  else if ((onFloor && manualair) || (onFloor && manual))
                  {
        //   Debug.Log("ESTADO 6");
                if (!manual)
                      MoviePlayer.Anime("SkaterPro_Manual", true, true);
                     
                      currentVel = Mathf.Max(currentVel - acel, MaxVel);
                      delta = Local_Gravity(currentVel);                    
                      Idle = false;
                      manualair = false;
                      manual = true;
                      air = false;
                      delta = Local_Gravity(currentVel);

            }

            //FLEX STATE TO INCREASE VELOCITY
            if (onFloor && Input.GetKey("down") && !onGrind)
                   {
         //       Debug.Log("ESTADO 7");
            //    Debug.Log("FLEX DOWN KEY");
                          if (!Flex)
                                  MoviePlayer.Anime("SkaterPro_Flex", true, true);
                          currentVel = Mathf.Min(currentVel+acel, FlexVel);
                          Idle = false;
                          Flex = true;
                          manual = false;
                          air = false;
                          combo = 0;
                          delta = Local_Gravity(currentVel);
               

            }

                 
       
                    //NO BUTTON PRESSED & ON FLOOR IDLE STATE ON FLOOR
                    else if (onFloor && Input.anyKey == false  && !(onGrind) && !manual && !manualair)
                    {
               
                if (!Idle)
                        {
         //           Debug.Log("ESTADO 8");
                            
                    //animation.StopAnimation();
                    MoviePlayer.Anime("SkaterPro_Idle", true, true);
             //      Debug.Log("LANZA LA ANIMACION IDLE " );
                             Idle = true;
                              Flex = false;
                              manual = false;
                              air = false;
                              combo = 0;
                              puntua.IncrementarCombo(combo, "Loquesea", 0);
                        }
              //  Idle = true;
              //  Flex = false;
               // manual = false;
               // air = false;
                currentVel = Mathf.Max(currentVel -acel, MaxVel);
                        delta= Local_Gravity(currentVel);
             
            }

            //JUMPING STATE
          if (!onFloor && !onGrind)
            {
         //       Debug.Log("ESTADO 9");
                if (outGrind)
                {
                    MoviePlayer.Anime("SkaterPro_Flex", false, true);
             //   Debug.Log("FLEX  OUTGRIND");
                outGrind = false;
                    air = true;
                    Flex = true;
                    Idle = false;
                    inGrind = false;
                }
                //Preapre Manual indaair like you just dont care
                if (Input.GetKeyDown("down") && !manual)
                {
                    manualair = true;
                    Flex = false;
                    Idle = false;
                    inGrind = false;
       
                }
                if (!air) {


                MoviePlayer.Anime("SkaterPro_Flex", true, true);
            //        Debug.Log("FLEX AIR");
                    
                    air = true;
                    Flex = true;
                    Idle = false;
                    inGrind = false;
                }

                //Flex = true;
                //Idle = false;
                //inGrind = false;
               

                // MoviePlayer.Anime("SkaterPro_Flex", false, false);
                //Para DEVOLVER LA ROTACION A NO ROTACION
                // if (transform.eulerAngles.z > MaxAngle && transform.eulerAngles.z < 360 - MaxAngle && !(GameOver)) { 
                // if (Input.anyKey == false)
                  //{
                  //Quaternion rot = new Quaternion(transform.rotation.x, transform.rotation.y, -transform.rotation.z,1);
                     //   Debug.Log("HAY QUE ENDEREZAR EL ANGULO!!!!Rotacion actual: "+ transform.eulerAngles.z +"º. Proceso de rotacion:" + Time.deltaTime * 10f*100f +"%");
                    //    gameObject.transform.parent.rotation = Quaternion.Lerp(gameObject.transform.parent.rotation, rot, 10*Time.deltaTime);
                      //  manual = false;
                  //}
           // }


        }
    }
}
}