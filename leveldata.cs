using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New LevelData", menuName = "level data", order = 3)]
public class leveldata : ScriptableObject
{
    //background
    public background_leveldata background;

    //player/enemy spawn
    public bool playerspawnatmiddle;
    public Vector3 playerspawnpoint_pos;
    public List<enemies_leveldata> enemies;
    //public bool bosslevel; //levelmanager can tell if it is boss level //boss levels will be put sepatately in worlds pack

    //obstacles
    public List<obstacles_leveldata> obstacles; 
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
}
//serialize obstacles
[System.Serializable]
public class obstacles_leveldata{
    public int child_code;
    public Vector3 position;
    public Quaternion rotation;

}
//serialize background
[System.Serializable]
public class background_leveldata
{
    public int child_code;
    public Vector3 parent_position;
    public Vector3 upperobj_pos;
    public Vector3 bottomobj_pos;

}
//serialize enemies
[System.Serializable]
public class enemies_leveldata
{
  
    public int child_code;
    public Vector3 position;
    public Quaternion rotation;

}
