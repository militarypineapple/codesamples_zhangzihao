using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class queuemanager : MonoBehaviour {

    //*************QUEUEMANAGER: handles the queue of any function that cannot finish running on the same frame of invoke *********/

    public delegate void queuedfunctions();
    public bool running;
    public List<queuedfunctions> queue;
	// Use this for initialization
	void Start () {
        queue = new List<queuedfunctions>();

    }
	
	// Update is called once per frame // can be stopped by timescale 0
	void FixedUpdate () {
        if(!running)
        {
            if(queue.Count!=0)
            {
                queue[0].Invoke();
                queue.Remove(queue[0]);
                running = true;
            }
           
        }
	}
}
