using UnityEngine;
using System.Collections;

public class ActualizarValoresGameOver : Player_Controller {
	public TextMesh total;
	public TextMesh record;
   // public static int puntuacion;
	//public Puntuacion puntuacion;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnEnable(){
		//Debug.Log ("Activado");
		total.text =Puntuacion.puntuacion.ToString ();
		if (EstadoJuego.estadoJuego != null) {
						record.text = EstadoJuego.estadoJuego.puntuacionMaxima.ToString ();
				}
	}

}
