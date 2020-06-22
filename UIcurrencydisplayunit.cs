using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIcurrencydisplayunit : MonoBehaviour {
    public bool enough;
    public bool notenough;
    public Color normal_color;
    public Color notenough_color;
    public int displayamount;
    public TextMeshPro text;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void RefreshStatus()
    {
        if(displayamount<=gamemanager.GM.currency_energy)
        {
            enough = true;
            notenough = false;
            text.color = normal_color;
        }
        else
        {
            enough = false;
            notenough = true;
            text.color = notenough_color;
        }
    }
}
