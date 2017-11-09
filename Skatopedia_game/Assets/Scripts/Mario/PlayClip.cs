using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*!
 *  @class LoopOne
 *  Loop through all animation sequences of a single game object.
 */
[AddComponentMenu("Harmony/Mario/PlayClip")]

public class PlayClip: MonoBehaviour {
    public float frameRate = 24.0f;

    IEnumerator Start()
    {
        //  Preemptively load clips.
        HarmonyRenderer renderer = GetComponent<HarmonyRenderer>();
        if ((renderer == null) || (renderer.clipNames == null))
            yield break;

        //yield return StartCoroutine(renderer.WaitForDownloads());

        foreach (string clipName in renderer.clipNames)
        {
        //    Debug.Log("Lista de clips de audio" + clipName);
            renderer.LoadClipName(clipName);
        }

        //  Wait for audio if necessary.
        HarmonyAudio audio = GetComponent<HarmonyAudio>();
        if (audio != null)
        {
            yield return StartCoroutine(audio.WaitForDownloads());
        }
    }

    // public void Anime(string ClipName)
   public void Anime(string ClipName, bool Loop, bool instant)
    {
        //Debug.Log("CLIPNAME: " +ClipName);
//if (!Player_Controller.GameOver) { 
        HarmonyAudio audio = GetComponent<HarmonyAudio>();
        if ((audio != null) && !audio.isReady)
           return;

        HarmonyAnimation animation = GetComponent<HarmonyAnimation>();
        if (animation == null)
            return;
       // animation.isPlaying = false;

        HarmonyRenderer renderer = GetComponent<HarmonyRenderer>();
        if ((renderer == null) || (renderer.clipNames == null))
        return;

        //if ( !renderer.isReady )
        //  return;

        //  Almost at end of our scheduled list, reschedule a new sequence.
        
        if (renderer.clipNames.Length > animation.scheduledCount)
        {
            //  Play all clips
            //  foreach (string clipName in renderer.clipNames)
            //{
            //   animation.PlayAnimation(frameRate, clipName);
            //yield return null;
         
        


            if (Loop) { 
               // animation.LoopAnimation(24.0f, 3 /* first clip */ );
            animation.LoopAnimation(24.0f, ClipName, 1.0f, !Loop, null);
           // Debug.Log("SE LANZA LA ULTIMA ANIMACION DE VERDAD EN BUCLE: " + Player_Controller.GameOver + " . EL NOMBRE DE LA ANIMACION ES: " + ClipName);
            }else
            //Debug.Log("LANZA LA ANIMACION" + ClipName);
            animation.PlayAnimation(frameRate, ClipName);
           // Debug.Log("SE LANZA LA ULTIMA ANIMACION DE VERDAD: " + Player_Controller.GameOver +" . EL NOMBRE DE LA ANIMACION ES: "+ ClipName);
           

            // animation.isStopped = false;
        }
}
    }
//}


