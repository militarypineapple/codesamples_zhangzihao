using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipmentmanager : MonoBehaviour {
    //client static equipment data
    public allequipmentdata allequipmentdata;
    public allequipmentdata allequipdata_magicstat1;
    public allequipmentdata allequipdata_magicstat2;
    //parse raw equipment string
    [SerializeField]private string _rawstring;
    string _littlestring;
    public List<string> equipments_equipped;
    public List<string> equipments_notequipped;

    //
    public GameEvent event_changeequipments;//triggers eventlisteners in :UI
    public GameEvent event_addequipment;
    public GameEvent event_deleteequipment;
    public GameEvent event_upload;//triggers eventlisteners in :data manager or data management


    //two-way string management: 1/get string from gamemanager and parse into equipments 2/get equipments and write into strings, give gamemanager
    //only raw string in-and-out
    //*!!!!!!!!!!! BASIC RAW_EQUIP_DATA STRUCTURE *this is the rule of decoding rawstring
    //SLOT 0: type                             ||
    //SLOT 1: subtype(weapon)                  ||
    //SLOT 2: rarity                           ||
    //SLOT 3: item_part_1                      ||main part of the weapon, determines rule of gaining exp       
    //SLOT 4: item_part_2                      ||
    //SLOT 5: item_part_3                      ||
    //SLOT 6 & 7: percentage of weapon exp     ||

    //public static equipmentmanager EM;  //make it a singleton
    //void Awake()
    //{
    //    if (EM != null) Destroy(EM);
    //    else EM = this;
    //    DontDestroyOnLoad(this);
    //}
    //this function is invoked by gamemanager/datamanager: gotdata-populatedata event
    public void Parsedataintoequipment()
    {
        //refresh equipments_notequipped from _rawstring
        equipments_notequipped.Clear();
        int j = 0;
        _littlestring = "";
        for (int i = 0; i < _rawstring.Length;i++)
        {
                _littlestring = _littlestring + _rawstring[i];
                j++;
                if (j==8)
                {
                    equipments_notequipped.Add(_littlestring);
                    j = 0;
                    _littlestring = "";
                }
        }
        //

    }

    //this function is invoked when client changes anything in equipment
    public void Writeequipmentintodata()
    {
         _littlestring = "";
        foreach(string s in equipments_notequipped)
        {
            _littlestring = _littlestring + s;
        }
        _rawstring = _littlestring;
    }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
