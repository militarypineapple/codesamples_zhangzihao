using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "worlds", menuName = "levelmanagement__worlds", order = 5)]

public class worlds : ScriptableObject
{
    public Levelpack_world[] allworlds;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
[System.Serializable]
public class Levelpack_world
{
    public string name;
    [Header("level_total should be 10s")]
    public int level_total;//can only be 10s
    public int supply_in_every_th_except_boss;//group levels[] by this
    [Header("boss_in_every_th should be supply_in_every_th_except_boss * 2")]
    public int boss_in_every_th;

    public leveldata level1;

    [Header("elements count should be level_total/supply_in_every_th_except_boss")]
    public Leveldatapack[] Leveldatapacks;//divide into  level_total/supply_in_every_th_except_boss  groups

    public leveldata[] supplies;
    public leveldata[] bosses;

    public effectdata[] availableEffects;
    public int[] playerexpneeded_eachlevel;
}
[System.Serializable]
public class Leveldatapack
{
    public leveldata[] levels;
}
