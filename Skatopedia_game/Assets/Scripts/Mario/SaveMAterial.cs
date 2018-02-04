using UnityEngine;
using UnityEditor;
public class SaveMAterial : MonoBehaviour {
	public Shader shader;
	private Material mat;
	// Use this for initialization
	void Start () {
		//Renderer rend = GetComponent<Renderer>();
		//Debug.Log ("rend" + rend);
//		rend.material = new Material (shader);
		//Material material= rend.GetComponent<Material>();
		//Debug.Log ("material" + material);

		Material mat = GetComponent<Renderer> ().material; 
		Debug.Log ("material es " + mat);
		AssetDatabase.CreateAsset (mat, "Assets/Scripts/Mario/MaterialBirdAnimated5.mat");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
