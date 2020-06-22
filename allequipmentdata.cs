using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AllEquipmentData", menuName = "AllEquipment data", order = 4)]
public class allequipmentdata : ScriptableObject
{

    public List<equip_type> types;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
//serialize obstacles
[System.Serializable]
public class equip_type
{
    public string Name;
    public List<equip_subtype> subtype;
}
[System.Serializable]
public class equip_subtype
{
    public string Name;
    public List<equip_rarity> rarity;
}
[System.Serializable]
public class equip_rarity
{
    public string Name;
    public List<equipdata> weapondatas;

}
