using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class BotonJugar : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseDown(){
//		Camera.main.GetComponent<AudioSource>().Stop ();
		GetComponent<AudioSource>().Play ();
		//El segundo parametro de Invoke hace referencia al tiempo que debe tardar en ejecutar la funcion invocada
		Invoke ("CargarNivelJuego", GetComponent<AudioSource> ().clip.length);
		Camera.main.GetComponent<AudioSource>().Stop ();
		//audio.Play();
		//Application.LoadLevel("Scena_juego1");
	}
	void CargarNivelJuego(){
        //Application.LoadLevel("Scena_juego1");
        PhysicsObject.gameOver = false;
        SceneManager.LoadScene("simple");
	}
}