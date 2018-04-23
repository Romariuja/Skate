using UnityEngine;
using System.Collections;
using System.Collections.Generic; 		//Allows us to use Lists.
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator.

[AddComponentMenu("Harmony/Mario/Puntuacion")]
public class Puntuacion : MonoBehaviour {
	
	public static int puntuacion = 0;
    public static int combo=0;
   // public List<string> specialTrick = new List<string>();
  //  public static int specialCont=0;
	public TextMesh marcador;
    private List<string> TrickList = new List<string>();
    private List<specialList> specialTrick = new List<specialList>();

    private List <string> TrickNames;
	//public TextMesh marcadorTiempo;
    public TextMesh marcadorCombo;
    public static GameObject EndScore;
   //public TextMesh trickName;ff
   // public TextMesh trickPoints;
   // public TextMesh TotalPoints;
    public TextMesh Tricks;
    public TextMesh special;
    private int parcialPoints;
    public AudioClip Audio1;
	public AudioClip Audio2;
	private float espera;
    private Coroutine co;
    private bool fading=false;
    private Vector2 positionSpecial = new Vector2(0,0);

    protected class specialList
    {
        public string name;
        public int points;
       public specialList(string newName, int newPoints)
        {
           name = newName;
            points = newPoints;
        }
    }




    // Use this for initialization
    void Start () {
        EndScore = GameObject.FindGameObjectWithTag("EndScore");
        Debug.Log(EndScore.name);
       // Debug.Break();
        ActualizarMarcador ();
        InitialiseList();
        InitialiseSpecial();
      //  positionSpecial = new Vector2(special.transform.localPosition.x, special.transform.localPosition.y);
        co = StartCoroutine(Desvanece(Tricks, 0.5f));
    }

    void InitialiseSpecial()
    {
      
        specialTrick.Clear();
    }

    void InitialiseList()
    {
        TrickList.Clear();
    }

    public void IncrementarPuntos(int puntos){
		puntuacion += puntos;
		ActualizarMarcador ();
	}


    public void IncrementSpecial(string comboName, float X, float Y, int fontSize, float fade)
    {

            //InitialiseSpecial();
        specialTrick.Add(new specialList(comboName,1000));
       // Debug.Log(specialTrick[0]);
        //Debug.Break();
        //  Tricks.text = "";
       StartCoroutine( ActualizaSpecialTrick(X,Y,fontSize,fade));
    }


        public void IncrementarCombo(int xcombo, string comboName, float puntosCombo)
    {
    
        combo = xcombo;
        puntuacion += combo * (int) puntosCombo;
     //   Debug.Log("INCREMENTA COMBO: Nombre"+ comboName+ " xcombo:" + xcombo + " puntos combo" + puntosCombo + " Longitud de la lista de trucos " +TrickList.Count);
       

        if (combo == 0)
        { 
            InitialiseList();
        //  Tricks.text = "";
            ActualizarMarcador();
            if (!fading)
            { 
                co = StartCoroutine(Desvanece(Tricks,0.5f));
                fading = true;
            }
        }

       else if (combo==1)
       {
                    
            fading = false;
            Tricks.text = "";
          //  Debug.Log("Longitud TrickList " +TrickList.Count);
            if (TrickList.Count == 0)
            { 
            TrickList.Add(combo + "x" + "  " + comboName + "  " + (int) puntosCombo + " =" + (int)puntosCombo * combo);
            }
            Tricks.text = Tricks.text + "\n" + TrickList[combo - 1];
            ActualizarMarcador();
            //marcadorCombo.text = combo.ToString();
            StopCoroutine(co);
            Tricks.color= new Color(Tricks.color.r, Tricks.color.g, Tricks.color.b, 1);
            //StartCoroutine(Desvanece(Tricks, 6, 1));
        }


        else if (combo == TrickList.Count) {
            fading = false;
            // marcadorCombo.text = combo.ToString();
            //Debug.Log("NO HACE FALTA AUMENTAR COMBO PERO BORRA LA PRIMERA LINEA, ESTO A VECES FALLA CON GRIND");
      //    Debug.Break();
            TrickList[combo-1] = combo + "x " + comboName + "  " + (int) puntosCombo + " = " + (int)puntosCombo * combo;           
            Tricks.text = TrickList[0];
            for (int i=1; i<TrickList.Count; i++)
            {
                Tricks.text = Tricks.text+ "\n" + TrickList[i];
            }     
            //Hay que actualizar solo la ultima linea no está hecho aun        
        }
        else {
            fading = false;
              // Debug.Log("COMBO NOOOOOOOOOO    ES IGUAL A LONG. DE LA LISTA DE TRUCOS. POSIBLE GRIND");
            TrickList.Add(combo + "x" + "  " + comboName + (int) puntosCombo + " =" + (int) puntosCombo * combo);
        Tricks.text = Tricks.text+ "\n" + TrickList[combo-1];



         //   marcadorCombo.text = combo.ToString();
        }  
    }

   public IEnumerator Desvanece(TextMesh desvanece, float espera)
    {
        //Debug.Log("Before Waiting 2 seconds");
        // yield return new WaitForSeconds(espera);
        // Debug.Log("Desvanece!!!!");

        desvanece.color = new Color(desvanece.color.r, desvanece.color.g, desvanece.color.b, 1);

        yield return new WaitForSeconds(espera);
        while (desvanece.color.a>0.0f)
        {
            desvanece.color = new Color(desvanece.color.r, desvanece.color.g, desvanece.color.b, desvanece.color.a-((Time.deltaTime)/espera));
         //  Debug.Log("Desvaneciendo "+ (1- desvanece.color.a)*100 + "%");
            yield return null;
           // yield break;
        }
        //desvanece.GetComponent<MeshRenderer>().enabled = false;
     

    }
    
    public void ActualizarMarcador(){
		marcador.text = puntuacion.ToString ();
      //  marcadorCombo.gameObject.SetActive(true);
       // marcadorCombo.text = combo.ToString();             
    }
    public void ActualizarCombo()
    {
       
        marcadorCombo.text = combo.ToString();   
    }
    public IEnumerator ActualizaSpecialTrick(float X, float Y, int fontSize, float fade)
    {
     
        //Debug.Log(specialTrick.ToString());
        //Debug.Break();
        //Debug.Log("POSICION ACTUAL DEL TEXTO SPECIAL TRICK: " +special.transform.position + " Posicion por argumento: " + new Vector3(X,Y,special.transform.position.z));
        //Debug.Break();
        special.transform.position = new Vector3(special.transform.position.x+X-positionSpecial.x, special.transform.position.y+Y - positionSpecial.y, special.transform.position.z);
        positionSpecial = new Vector2(X, Y);
        special.fontSize = fontSize;
        special.text = specialTrick[specialTrick.Count-1].name;
        //yield return new WaitForSeconds(3);
        StopCoroutine(co);
        co = StartCoroutine(Desvanece(special, fade));
        yield return new WaitForSeconds(fade);
        //special.transform.position = new Vector3(special.transform.position.x - X, special.transform.position.y - Y, special.transform.position.z);
       

    }


    // Update is called once per frame----------------------------------------------------------------------------------------------------------------------------------------
    void Update ()
    {
      //  marcadorTiempo.text= ((int) Time.time).ToString();
	}

    void FixedUpdate()
    {
    }
}
