using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class BotonJugar : MonoBehaviour {


    private bool load;
    AsyncOperation asyncLoad;
    Scene CurrentScene;
    // Use this for initialization
    void Start () {
        //Use a coroutine to load the Scene in the background
        load = false;
        CurrentScene = SceneManager.GetActiveScene();
        StartCoroutine(LoadGameScene());
        Debug.Log("START Load Async");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseDown(){

//		Camera.main.GetComponent<AudioSource>().Stop ();
		GetComponent<AudioSource>().Play ();
		//El segundo parametro de Invoke hace referencia al tiempo que debe tardar en ejecutar la funcion invocada
		//Invoke ("CargarNivelJuego", GetComponent<AudioSource> ().clip.length);
       // StartCoroutine(LoadYourAsyncScene());
    
        Debug.Log("START Load Async");
        load = true;
        Camera.main.GetComponent<AudioSource>().Stop();
        //audio.Play();
        //Application.LoadLevel("Scena_juego1");
    }
	void CargarNivelJuego(){
        //Application.LoadLevel("Scena_juego1");
        PhysicsObject.gameOver = false;
        SceneManager.LoadScene("simple");
	}


    public IEnumerator LoadGameScene()
    {
        PhysicsObject.gameOver = false;
        Debug.Log("START Load Async");
      //  var result = SceneManager.LoadSceneAsync("simple", LoadSceneMode.Additive);
        var result = SceneManager.LoadSceneAsync("simple", LoadSceneMode.Single);
        result.allowSceneActivation = false;

        while (result.progress<0.9f || load==false)
        {
       //     Debug.Log("progress: " + result.progress + " Load:" +load);
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("ESCENAS CARGADAS" + SceneManager.sceneCount);
        //   GameObject.Destroy(GameObject.Find("MainScene"));
        Debug.Log("Escena actual " + CurrentScene);
      
 
        Debug.Log("YEAH Loaded Async");
        // still scene one should be active, tryed it as workaround, did not help
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("simple"));
        result.allowSceneActivation = true;
        SceneManager.UnloadSceneAsync("simple");

        //  SceneManager.UnloadSceneAsync(SceneManagement.Scene Main);


    }



   

    IEnumerator LoadAnimation()
        {

        //Invoke("ActivateScene",0);
        yield return null;
            //yield  WaitForSeconds(4);
        //asyncLoad.allowSceneActivation = true;

    }


    

}



   
         


    
