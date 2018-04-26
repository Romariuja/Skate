using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndObstacle : Obstacle {

   private float fade=0;
    // public GameObject marcador;
    public TextMesh marcador;
    public GameObject targetList;
    public GameObject targetCross;
    public GameObject StageName;
    private Coroutine co;
    public GameObject TickPrefab;
    public GameObject CrossPrefab;
    // Use this for initialization
    void Start () {
		
	}

    IEnumerator FinalZoom(float espera)
    {
        yield return new WaitForSeconds(espera);
        Player.CameraScript.lastRoutineZoom = StartCoroutine(Player.CameraScript.ZoomCamera_position(1.5f, 0.01f, 10, 7, -7));
        yield return new WaitForSeconds(2*espera);
       
    //    Debug.Break();
       // Puntuacion.EndScore.transform.localPosition = new Vector3(0,0,1);
        //Debug.Log("El nombre del objeto blur es: " +ControladorCamara.Blur.name);
       Player.CameraScript.Blur.SetActive(true);
        //Player.CameraScript.shader1.SetFloat("_Size", 0);

       fade = 0;
        // Vector3 pos = Puntuacion.EndScore.transform.localPosition;
        //Defining pos the score position moves linealy
        StartCoroutine(Player.PuntuacionScript.Desvanece(marcador, 0.35f));
        float Xpos = 0;
        while (fade < 1)
        {
            //  Puntuacion.EndScore.transform.localPosition = Vector3.Lerp(pos, new Vector3(0,0,1), fade);
            Puntuacion.EndScore.transform.localPosition = Vector3.Lerp(Puntuacion.EndScore.transform.localPosition, new Vector3(0, 0, Puntuacion.EndScore.transform.localPosition.z), fade);
           marcador.transform.localScale = Vector3.Lerp(marcador.transform.localScale, marcador.transform.localScale*2, fade);

            targetList.transform.localPosition = Vector3.Lerp(targetList.transform.localPosition, new Vector3(Xpos, targetList.transform.localPosition.y, targetList.transform.localPosition.z), fade);
            targetCross.transform.localPosition = Vector3.Lerp(targetCross.transform.localPosition, new Vector3(Xpos, targetCross.transform.localPosition.y, targetCross.transform.localPosition.z), fade);

            StageName.transform.localPosition = Vector3.Lerp(StageName.transform.localPosition, new Vector3(-4.75f,StageName.transform.localPosition.y, StageName.transform.localPosition.z), fade);
            //  Debug.Log(marcador.text);
            marcador.transform.localScale = Vector3.Lerp(marcador.transform.localScale,Vector3.one*5,fade);
            marcador.transform.localPosition= Vector3.Lerp(marcador.transform.localPosition, new Vector3 (-3,3, marcador.transform.localPosition.z),fade);
            //   Debug.Log(Puntuacion.marcador);
            Player.CameraScript.Blur.GetComponent<CanvasRenderer>().SetAlpha(0.001f*fade);
            //Player.CameraScript.Blur.GetComponent<CanvasRenderer>().GetMaterial().SetFloat("_Size", fade);
            fade =fade+0.1f*Time.deltaTime;
            //Debug.Log("Position Score"+ Puntuacion.EndScore.transform.localPosition + "Fade: " +fade);
            yield return null;

        }
    }




    public IEnumerator writeTargetList()
    {

        //GameObject[] Ticks = GameObject.FindGameObjectsWithTag("Respawn");
        GameObject[] Ticks = new GameObject[Level.TargetList.Count];
        targetList.GetComponent<TextMesh>().text = "";
  

        Debug.Log("LONGITUD DE LA LISTA" +Level.TargetList.Count);
      // targetList.GetComponent<TextMesh>().text = "";
        int i;
        int j;
        for (i = 0; i < Level.TargetList.Count; i++)
        {
            targetList.GetComponent<TextMesh>().text = (targetList.GetComponent<TextMesh>().text + Level.TargetList[i].name + "\n");

        }
        yield return new WaitUntil(() => fade >= 0.5);
       // targetList.GetComponent<TextMesh>().text = "";
        for (i = 0; i < Level.TargetList.Count; i++)
        {

            //   targetList.GetComponent<TextMesh>().text = (targetList.GetComponent<TextMesh>().text + Level.TargetList[i].name + "      " + Level.TargetList[i].init + "\n");
            //for (j = 0; j < Level.TargetList[i].name.Length; j++)
            //  {
            //      targetCross.GetComponent<TextMesh>().text = (targetCross.GetComponent<TextMesh>().text + "___" + "\n");
            //    }
            // targetCross.GetComponent<TextMesh>().text = (targetCross.GetComponent<TextMesh>().text + "\n");
            if (Level.TargetList[i].init>0)
            { 

           Ticks[i]= Instantiate(TickPrefab, targetList.transform.position + new Vector3 (Level.TargetList[i].name.Length*0.1f+1.5f,-i*1.6f-0.5f,0) , targetList.transform.rotation);
            Ticks[i].transform.parent = targetList.transform;
          
            }
            else { 
                Ticks[i] = Instantiate(CrossPrefab, targetList.transform.position + new Vector3(Level.TargetList[i].name.Length * 0.1f + 1.5f, -i * 1.6f - 0.5f, 0), targetList.transform.rotation);
            Ticks[i].transform.parent = targetList.transform;
            }


            yield return new WaitForSeconds(1);
        }
              StartCoroutine(PC.GetComponent<Player>().slowDown(15));
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Table" || collision.gameObject.tag == "Skater") &&  (PhysicsObject.levelOver == false))
        {
           //Debug.Log("FINAL DE LA PANTALLA "+ collision.gameObject.transform.parent);
           // StartCoroutine();
            //   collision.gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity=new Vector3(0,0,0);
            StartCoroutine(PC.GetComponent<Player>().slowDown(25));
           // StartCoroutine(ControladorCamara.player.slowDown());
         StartCoroutine(FinalZoom(2));
            PhysicsObject.levelOver = true;
           StartCoroutine(writeTargetList());
            Player.combo = 0;
            
            //  collision.gameObject.GetComponent<Player>().enabled = false;
        }


    }
    // Update is called once per frame
    void Update () {
		
	}
}
