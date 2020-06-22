using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvaspopupwindows : MonoBehaviour {
    [SerializeField]private GameObject _current_alive_tab;

    //group1 equipment detail tab
    public GameObject blackback;
    public GameObject equipdetail_tab;


    public void Tab_equipment_detail_open(string equip_string)
    {
        string s = equip_string;

        //populate tab
        //d: equip stat0_basicstat   m1: equip stat1_magicstat    m2: equip stat2_magicstat
        //fetch part1 data object [_ _ _ d _ _ _ _]
        equipdata d = gamemanager.GM.equipmentmanager.allequipmentdata.types[ASCIIToInt(s[0])].subtype[ASCIIToInt(s[1])].rarity[ASCIIToInt(s[2])].weapondatas[ASCIIToInt(s[3])];
        //fetch part2 data object [_ _ _ _ d _ _ _]
        equipdata m1 = gamemanager.GM.equipmentmanager.allequipdata_magicstat1.types[ASCIIToInt(s[0])].subtype[ASCIIToInt(s[1])].rarity[ASCIIToInt(s[2])].weapondatas[ASCIIToInt(s[4])];
        //fetch part1 data object [_ _ _ _ _ d _ _]
        equipdata m2 = gamemanager.GM.equipmentmanager.allequipdata_magicstat2.types[ASCIIToInt(s[0])].subtype[ASCIIToInt(s[1])].rarity[ASCIIToInt(s[2])].weapondatas[ASCIIToInt(s[5])];

        //display anything needed

        //open tab
        OpenTabWithAnimation(equipdetail_tab);

    }
    public void OpenTabWithAnimation(GameObject tab_to_open)
    {
        //enable
        blackback.SetActive(true);
        _current_alive_tab = tab_to_open;
        _current_alive_tab.SetActive(true);

        //play some animation
        //
        //
        
    }
    public void CloseLiveTabWithAnimation()
    {
        //play some animation
        //
        //

        //disable
        blackback.SetActive(false);
        _current_alive_tab.SetActive(false);
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //equipment data DECODER
    public int ASCIIToInt(char c)
    {
        int value = 0;
        if (c <= 57)
        {
            value = c - 48;
        }
        if (c >= 65 && c <= 90)
        {
            value = c - 65 + 10;
        }
        if (c >= 97)
        {
            value = c - 97 + 10 + 26;
        }
        return value;
    }
    //equipment data ENCODER
    public char IntToASCII(int i)
    {
        int _int = 0;
        if (i <= 9)
        { _int = i + 48; }
        if (i >= 10 && i < 36)
        { _int = i + 48 + 7; }
        if (i >= 36)
        { _int = i + 48 + 7 + 6; }
        char value = new char();
        value = (char)_int;
        return value;
    }
}
