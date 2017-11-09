using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation_Harmony : MonoBehaviour {

    public float frameRate = 24.0f;
    public float Clip;
    private float lastclip;
    public bool Loop = false;
    private int i = 0;
    HarmonyAnimation animationClip;
    // Use this for initialization
    void Start()
    {
        animationClip = GetComponent<HarmonyAnimation>();
    }

    // Update is called once per frame
    void Update()
    {

        //HarmonyAnimation 
       //animation.LoopAnimation(frameRate, (int)Clip);
        //animation.PlayAnimation(frameRate, (int)Clip, 1.0f,  1, false);
        if (i==0 && !Loop) { 
        animationClip.PlayAnimation(frameRate, (int)Clip);
            i++;
        }
        if (i == 0 && Loop)
        {
            animationClip.LoopAnimation(frameRate, (int)Clip);
            i++;
        }

        // animation.PlayAnimationPriv(frameRate, clipIdx, startFrame, -1.0f /*full length*/, nTimes, reverse, callbacks);
        if (lastclip != Clip)
        {
            animationClip.ResetAnimation();
            i = 0;
           lastclip = Clip;
        }

    }
}
