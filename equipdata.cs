using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EquipData", menuName = "equip data", order = 1)]
public class equipdata : ScriptableObject
{
    public GameObject prefab;
    public Vector3 prefab_icon_pos_offset;
    public Quaternion prefab_icon_rotation_offset;
    public Vector3 prefab_icon_scale;
    public Vector3 prefab_displaytab_offset;
    public Vector3 prefab_onhand_offset_pos;
    public Quaternion prefab_onhand_offset_rot;
    [SerializeField] private string weaponname;
    [SerializeField] private int damage;
    [SerializeField] private List<effectdata> effects;
    [SerializeField] private int rarity;//0 is white //1 is blue //2 is purple //3 is gold

    //data
    public string itemname;
    public string description;


    public string Weaponname
    {
        get
        {
            return weaponname;
        }
    }
    public int Damage
    {
        get
        {
            return damage;
        }
    }
    public List<effectdata> Effects
    {
        get
        {
            return effects;
        }
    }
    public int Rarity
    {
        get
        {
            return rarity;
        }
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
