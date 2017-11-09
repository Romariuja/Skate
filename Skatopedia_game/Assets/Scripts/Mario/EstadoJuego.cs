using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class EstadoJuego : MonoBehaviour {
	//Awake igual que estar pero se llama siempre antes que star
	//Antes de ejecutar el star se ha llamado a todos los awake de todos los componmemtes de este objeto
	public static EstadoJuego estadoJuego;
	public int puntuacionMaxima=0;
	private String rutaArchivo;

	void Awake(){
		//Para saber donde guardar
		rutaArchivo=Application.persistentDataPath + "/datos.dat";
		//es nulo si es la primera vez que este script se esta ejecutando
				if (estadoJuego == null) {
						estadoJuego = this;
						DontDestroyOnLoad (gameObject);
				} else if (estadoJuego != this) {
			Destroy (gameObject);		
		}
		}

	// Use this for initialization
	void Start () {
		Cargar ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Guardar(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (rutaArchivo);
		DatosAGuardar datos = new DatosAGuardar ();
		datos.puntuacionMaxima = puntuacionMaxima;
		bf.Serialize (file, datos);

		file.Close();
		}
	void Cargar(){
		if (File.Exists(rutaArchivo)){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (rutaArchivo, FileMode.Open);
		DatosAGuardar datos = (DatosAGuardar)bf.Deserialize (file);
		puntuacionMaxima = datos.puntuacionMaxima;
		file.Close ();
	}else
		{
			puntuacionMaxima = 0;
		}
}
}
[Serializable]
class DatosAGuardar{
	public int puntuacionMaxima;
}
