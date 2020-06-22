using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIequipmentTAB : MonoBehaviour {

    public string equipment_string;
    public int place_in_sorted;
    public GameObject equipmentmanagerUI;
    public GameObject icon_anchor;

    public void Clicked()
    {
        equipmentmanagerUI.GetComponent<equipmentmanagerUI>().Tab_open(equipment_string);
    }

    private void OnDisable()
    {
        foreach(Transform c in gameObject.transform.GetChild(0))//foreach child in rarity
        {
            c.gameObject.SetActive(false);
        }
        foreach(Transform c in gameObject.transform.GetChild(1))//foreach type
        {
            foreach(Transform c_sub in c)
            {
                c.gameObject.SetActive(false);
            }
        }
    }
   
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
