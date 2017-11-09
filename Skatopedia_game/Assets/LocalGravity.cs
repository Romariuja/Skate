using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalGravity :Player_Controller
{
  public static Vector2  tangent2D;
    public static float Angle;
   // public static bool GameOver = false;
   // public static bool onFloor;
    //public static bool onGrind;
    // Use this for initialization
    void Start () {
      //  Debug.Log(" gameObject"+gameObject.name);
        //Debug.Log(" Abuelo"+gameObject.transform.parent.parent.name);
        //El abuelo es el que tiene el Rigidbody
    }
    void FixedUpdate()
    {
      //  RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1 << LayerMask.NameToLayer("Floor"), -2);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);

        if (hit.collider != null)

        {
            var distanceToGround = hit.distance;
            //  Debug.Log(hit.transform); //results: tree_log(Clone) (UnityEngine.Transform)
            // Debug.Log(hit.collider);  // result: tree_log(Clone) (UnityEngine.BoxCollider2D)
          //  Debug.Log(hit.collider.name); //results: NullReferenceException: Object reference not set to an instance of an object
          //  Debug.Log("Distancia" + distanceToGround);
           // Debug.Log("Normal al collider" + hit.normal);

            Vector3 tangent = Vector3.Cross(hit.normal, Vector3.forward);
        tangent2D = new Vector2(tangent.x* Mathf.Sign(Mathf.Cos(gameObject.transform.parent.parent.transform.eulerAngles.z * Mathf.PI / 180)), tangent.y);
           // Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
            if (tangent.magnitude == 0)
            {
                tangent = Vector3.Cross(hit.normal, Vector3.up);
                tangent2D = new Vector2(tangent.x*Mathf.Sign(Mathf.Cos(gameObject.transform.parent.parent.transform.eulerAngles.z * Mathf.PI / 180)), tangent.y);
            }
            //FALLA EL SIGNO NEGATIVO DE LA TANGENTE EN LA COMPONENTE X

            //Debug.Log("Tangente "+ tangent2D);
            // Debug.Log(hit.transform.name); //results: NullReferenceException: Object reference not set to an instance of an object
            //Debug.DrawLine(transform.position, hit.point, Color.green);
            //Ray2D rayotang = new Ray2D(position2D,tangent2D);
            //  Debug.DrawRay(transform.position,tangent,Color.red);
            Angle = Mathf.Tan(tangent2D.y / tangent2D.x);
            Debug.Log((Mathf.Sin(Angle)));
            //Debug.Log("Angulo" + Angle);
            
               if ((onFloor || onGrind) && Input.anyKey == false  && !(GameOver)) {
                Debug.Log("A TOPE CON LA LOCAL GRAVITY");
               //gameObject.transform.parent.parent.transform.position = new Vector2(hit.point.x, hit.point.y+2 );
              // gameObject.transform.parent.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(tangent2D.x,tangent2D.y).normalized*MaxVel;
               gameObject.transform.parent.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(tangent2D.x, tangent2D.y).normalized * MaxVel;
                if (Flex)
                    gameObject.transform.parent.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(tangent2D.x, tangent2D.y).normalized * FlexVel;

                //  Debug.Log("Signo de la componente x de la velocidad" + Mathf.Sign(Mathf.Cos(gameObject.transform.parent.parent.transform.eulerAngles.z * Mathf.PI / 180)));
                // Debug.Log("componente X de la velocidad: " + tangent2D.x + "Componente y de la velocidad: " + tangent2D.y);
                //    gameObject.transform.parent.parent.transform.rotation = Quaternion.Lerp(gameObject.transform.parent.parent.rotation, Quaternion.Euler(0, 0, Angle), Time.deltaTime * 10f);
            }
        }
    }
    

    // Update is called once per frame
    void Update () {
      
}
}
