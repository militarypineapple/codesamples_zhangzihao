using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

//the level manager is a singleton in scene
//monitors all in-level events
public class levelmanager : MonoBehaviour {

    //display
    public cameraBehaviors camera_Main;
    public cameraBehaviors camera_gameplay;

    public GameEvent level_finishedloading_event;

    public List<effectdata> effects;  //effects

    public List<GameObject> enemies;   //enemies

    //obj pooler
    public List<GameObject> pooledobjects;  //object pool container
    public List<ObjectPoolItem> itemsToPool;  //object pool setup


    //***********************************LEVEL MANAGEMENT******************************//
    public int current_world; //get from gamemanager or something [uncertain]
    //public Levelpack_world currentworld_data;
    public int current_level; //caculated and stored in this script
    public leveldata currentlevel_data;
    private List<leveldata> _unusednormallevels;
    private List<leveldata> _unusedbosslevels;
    private List<leveldata> _unusedsupplylevels;
    public worlds worlds;//stores all world/level data
                         // public Levelpack_world[] worlds;

    //level spawner
    [SerializeField] private GameObject obstacle_prefab;
    [SerializeField] private GameObject enemy_prefab;
    [SerializeField] private GameObject background_inscene;
    public GameObject background_upperobjects;
    [SerializeField] private GameObject background_bottomobjects;
    [SerializeField] private List<GameObject> obstacles;

    //***********************************ENEMY MANAGEMENT******************************//
    public bool checkforenemy;

    //***********************************GAMEPLAY MANAGEMENT******************************//
    //sub-managers
    public exptracker exptracker;
    //controller
    public GameObject controller;

    public static levelmanager LM;  //make it a singleton
    void Awake()
    {
        if (LM != null) Destroy(LM);
        else LM = this;
        DontDestroyOnLoad(this);
    }
    
    // Use this for initialization
    void Start () 
    {
        //should define at start to avoid Exceptions
        _unusednormallevels = new List<leveldata>();
        _unusedbosslevels = new List<leveldata>();
        _unusedsupplylevels = new List<leveldata>();

        //on start, create new pool and fill with placeholder objects
        pooledobjects = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amounttopool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objecttopool);
                obj.SetActive(false);
                pooledobjects.Add(obj);
            }
        }
    }
    //object pooler
    public GameObject GetPooledObject(string tag)
    {
        //check all pooled objects
        for (int i = 0; i < pooledobjects.Count; i++)
        {
            //get the one that's inactive
            if (!pooledobjects[i].activeInHierarchy && pooledobjects[i].CompareTag(tag))
            {
                return pooledobjects[i];
            }
        }
        //if no inactive object then we instantiate one and expand the pool 
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objecttopool.CompareTag(tag))
            {

                    GameObject obj = (GameObject)Instantiate(item.objecttopool);
                    obj.SetActive(false);
                    pooledobjects.Add(obj);
                    return obj;

            }
        }
        //if there's no bugs this below 2 lines should never run
        Debug.LogError("bug in levelmanager.GetPooledObject: got no object");
        return null;
    }
    // Update is called once per frame
    void Update () {
		
	}

    private void FixedUpdate()
    {
        if(checkforenemy)
        {
            if(enemies.Count>0)
            {

            }
            else
            {
                checkforenemy = false;
                //enemy = 0 logic here
                  //this is portal blocker
                background_upperobjects.transform.GetChild(1).gameObject.SetActive(false);
                //
            }
        }
    }
    //main screen play button
    public void Startfromlv1()
    {
        //main camera white in
        gamemanager.GM.queuemanager.queue.Add(camera_Main.WhiteIn);
        //camera_Main.WhiteIn();
        //determin which level
        current_level = 1;
        currentlevel_data = worlds.allworlds[current_world].level1;

        //refresh normal level group
        _unusednormallevels.Clear();
        foreach (leveldata _leveldata in worlds.allworlds[current_world].Leveldatapacks[current_level / worlds.allworlds[current_world].supply_in_every_th_except_boss].levels)
        {
            _unusednormallevels.Add(_leveldata);
        }
        //get a list of bosses
        _unusedbosslevels.Clear();
        foreach (leveldata _leveldata in worlds.allworlds[current_world].bosses)
        {
            _unusedbosslevels.Add(_leveldata);
        }
        //get a list of supplies levels
        _unusedsupplylevels.Clear();
        foreach (leveldata _leveldata in worlds.allworlds[current_world].supplies)
        {
            _unusedsupplylevels.Add(_leveldata);
        }
        //populate
        PopulateLevel(currentlevel_data);

    }

    //called when player enters next level portal
    public void Enternextlevelportal()
    {
        //iterate current level
        current_level = current_level + 1;

        //determin which level
        if(current_level == worlds.allworlds[current_world].level_total+1)//means player cleared this world
        {
            //iterate world number event      //if already unlocked do nothing if not pop prompt
            //
            //
            //exit to main menu
            //
            //
        }
        else
        {
            if (current_level % worlds.allworlds[current_world].boss_in_every_th == 0) //means the next level: boss level
            {
                //get leveldata
                currentlevel_data = _unusedbosslevels[Random.Range(0, _unusedbosslevels.Count)];
                _unusedbosslevels.Remove(currentlevel_data);
                //refresh normal level group
                _unusednormallevels.Clear();
                foreach (leveldata _leveldata in worlds.allworlds[current_world].Leveldatapacks[current_level / worlds.allworlds[current_world].supply_in_every_th_except_boss].levels)
                {
                    _unusednormallevels.Add(_leveldata);
                }
            }
            else //the next level: not boss level
            {
                if (current_level % worlds.allworlds[current_world].supply_in_every_th_except_boss == 0)//means the next level: supply level
                {
                    //get leveldata
                    currentlevel_data = _unusedsupplylevels[Random.Range(0, _unusedsupplylevels.Count)];
                    _unusedsupplylevels.Remove(currentlevel_data);
                    //refresh normal level group
                    _unusednormallevels.Clear();
                    foreach (leveldata _leveldata in worlds.allworlds[current_world].Leveldatapacks[current_level / worlds.allworlds[current_world].supply_in_every_th_except_boss].levels)
                    {
                        _unusednormallevels.Add(_leveldata);
                    }
                }
                else //the next level: normal level
                {
                    //get leveldata
                    currentlevel_data = _unusednormallevels[Random.Range(0, _unusednormallevels.Count)];
                    _unusednormallevels.Remove(currentlevel_data);
                }
            }
            //populate level
            PopulateLevel(currentlevel_data);
        }
       
    }

    //level loader
    public void PopulateLevel(leveldata data)
    {
        //spawn obstacles
        //clean former obstacles
        foreach (GameObject obstacle_former in obstacles)
        {
            obstacle_former.SetActive(false);
        }
        obstacles.Clear();
        //read: obstacles
        foreach (obstacles_leveldata obstacle_data in data.obstacles)
        {

            GameObject obstacle_new = GetPooledObject("obstacle");
            obstacle_new.transform.position = obstacle_data.position;
            obstacle_new.transform.rotation = obstacle_data.rotation;
            obstacle_new.transform.SetParent(background_inscene.transform.parent);
            foreach (Transform child in obstacle_new.transform)
            {
                child.gameObject.SetActive(false);
            }
            obstacle_new.transform.GetChild(obstacle_data.child_code).gameObject.SetActive(true);
            obstacles.Add(obstacle_new);
            //GameObject obstacle_new = Instantiate(obstacle_prefab, obstacle_data.position, obstacle_data.rotation);
            //obstacle_new.transform.SetParent(background_prefab_inscene.transform.parent);
            //obstacle_new.transform.GetChild(obstacle_data.child_code).gameObject.SetActive(true);
            //obstacles.Add(obstacle_new);
        }

        //spawn enemies
        //clean former enemies
        foreach (GameObject enemy_former in enemies)
        {
            enemy_former.SetActive(false);
        }
        enemies.Clear();
        //read: enemies
        foreach (enemies_leveldata enemy_data in data.enemies)
        {

            GameObject enemy_new = GetPooledObject("enemy");
            enemy_new.transform.position = enemy_data.position;
            enemy_new.transform.rotation = enemy_data.rotation;
            enemy_new.transform.SetParent(background_inscene.transform.parent);
            foreach (Transform child in enemy_new.transform)
            {
                child.gameObject.SetActive(false);
            }
            enemy_new.transform.GetChild(enemy_data.child_code).gameObject.SetActive(true);
            enemies.Add(enemy_new);
            //GameObject obstacle_new = Instantiate(obstacle_prefab, obstacle_data.position, obstacle_data.rotation);
            //obstacle_new.transform.SetParent(background_prefab_inscene.transform.parent);
            //obstacle_new.transform.GetChild(obstacle_data.child_code).gameObject.SetActive(true);
            //obstacles.Add(obstacle_new);
        }


        if (background_inscene != null) //actually background_inscene is never null
        {
            foreach (Transform child in background_inscene.transform)
            {
                child.gameObject.SetActive(false);
            }
            background_inscene.transform.GetChild(data.background.child_code).gameObject.SetActive(true);
            background_inscene.transform.position = data.background.parent_position;
            Debug.Log("background scene pos = " + background_inscene.transform.position);
            Debug.Log("background data pos = " + data.background.parent_position);
        }
        else{}
        if (background_upperobjects != null)
        {
            background_upperobjects.transform.position = data.background.upperobj_pos;
            Debug.Log("background_upper scene pos = " + background_upperobjects.transform.position);
            Debug.Log("background_upper data pos = " + data.background.upperobj_pos);
        }
        else{}
        if (background_bottomobjects != null)
        {
            background_bottomobjects.transform.position = data.background.bottomobj_pos;
            Debug.Log("background_bottom scene pos = " + background_bottomobjects.transform.position);
            Debug.Log("background_bottom data pos = " + data.background.bottomobj_pos);
        }
        else{}

        //if this level's NEXT is boss level, mark on door and other things(rewards etc).
        if ((current_level % worlds.allworlds[current_world].boss_in_every_th) == (worlds.allworlds[current_world].boss_in_every_th - 1))
        {
            //mark on door or something, modify background_upper or something because the next level to this level is BOSS
            //
            //
            if (current_level % worlds.allworlds[current_world].supply_in_every_th_except_boss == 0)//the level is supply level, don't give health-generating monster
            {

            }
            else //the level is not supply level, give a special health-generating monster because the next level to this level is BOSS
            {

            }
        }

        //reset player//reset camera//UI white in //invoked as a GAME EVENT
        //level_finishedloading_event.Raise();
        gamemanager.GM.queuemanager.queue.Add(camera_gameplay.WhiteOut);
        //camera_gameplay.WhiteOut(camera_Main);
        exptracker.gameObject.SetActive(true);
        //

        //****************ENEMY******************//
        //check for enemy if there is any enemy
        checkforenemy = true;
        background_upperobjects.transform.GetChild(1).gameObject.SetActive(true);
        //
    }
    
}


[System.Serializable]
public class ObjectPoolItem
{
    public GameObject objecttopool;
    public int amounttopool;
}

