using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class currencymanager : MonoBehaviour {

    public GameEvent currency_updatestatus_event;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void deductcurrency_energy(int amount)
    {
        gamemanager.GM.currency_energy -= amount;
        currency_updatestatus_event.Raise();

    }
}
