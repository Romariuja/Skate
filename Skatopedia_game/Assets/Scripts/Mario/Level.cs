using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {


    public static List<Target> TargetList;

        //Debug.Log("LA tabla es" + Table.name + ",y esta en el obbjeto padre " + Table.transform.parent.parent + "En la posicion " + Table.transform.localPosition );

void Start()
{
    TargetList = new List<Target>();
    TargetList.Add(new Target("Loop", 0));
    TargetList.Add(new Target("Mortal Bastard", 0));
    TargetList.Add(new Target("Unicorn Horn Grind", 0));
    TargetList.Add(new Target("Supertrick", 0));

    }

//LOCAL FUNCTIONS____________________________________________________________________________________________________________________________________________________________________


// void LoadAnimataionClip()
//{
//  animationLoadManager.LoadAnimation("Ollie", null);
//}

//UPDATE TRANSITIONS-----------------------------------------------------------------------------------------------------------------------------------------------------------
public class Target
{
    public string name;
    public int init;
    public Target(string newName, int newInit)
    {
        name = newName;
        init = newInit;
    }
}

public void UpdateTarget(List<Target> TargetList, string Name)
{

    for (int i = 0; i < TargetList.Count; i++)
    {
        if (TargetList[i].name == Name)
        {
            TargetList[i].init ++;
        }
       
    }
}


protected int TestTarget(List<Target> TargetList, string Name)
{

    for (int i = 1; i < TargetList.Count; i++)
    {
        if (TargetList[i].name == Name)
        {
            return TargetList[i].init;
        }
    }
    return 0;
}

// Use this for initialization

	
	// Update is called once per frame
	void Update () {
		
	}

}