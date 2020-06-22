using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonRaiseEvent : MonoBehaviour {
    public GameEvent eventtoraise;


    public void RaiseEvent()
    {

        eventtoraise.Raise();

    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
