using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class selectskillscreen : MonoBehaviour {

    public popupgreyback greyback;
    //shuffle skills
    public GameObject skilltab_prefab;
    public GameObject mountpoint1;
    public GameObject mountpoint2;
    public GameObject mountpoint3;

    public List<GameObject> tab1_list;
    //new
    public List<GameObject> tab2_list;
    public List<GameObject> tab3_list;
    //animate
    [SerializeField] private bool tab1_animating;
    [SerializeField] private bool tab1_animate_shuffling;
    [SerializeField] private bool tab1_animate_stopping;
    [SerializeField] private bool tab1_readjusting;
    [SerializeField] float speed_shuffle;
    [SerializeField] int freeshuffling_frames;
    //new 
    [SerializeField] private bool tab2_animating;
    [SerializeField] private bool tab2_animate_shuffling;
    [SerializeField] private bool tab2_animate_stopping;
    [SerializeField] private bool tab2_readjusting;
    int tab2_startorder;
    float _speed_stopping_tab2; 
    float _speed_readjusting_tab2;
    [SerializeField] private bool tab3_animating;
    [SerializeField] private bool tab3_animate_shuffling;
    [SerializeField] private bool tab3_animate_stopping;
    [SerializeField] private bool tab3_readjusting;
    int tab3_startorder;
    float _speed_stopping_tab3;
    float _speed_readjusting_tab3;
    /// <summary>
    /// //////
    /// </summary>
    int freeshuffling_counter;
    [SerializeField] bool freeshuffling;
    [SerializeField] float speed_stopping;
    float _speed_stopping;
    [SerializeField] float speed_readjusting;
    float _speed_readjusting;
   
    //[SerializeField] int stopin_frames;

    int tab1_startorder;

    //[Data]skill effect assigning
    [SerializeField] effectdata tab1_targeteffect;
    [SerializeField] effectdata tab2_targeteffect;
    [SerializeField] effectdata tab3_targeteffect;

    //announce skill effect after choice
    public GameObject announcer;
    


    private void OnEnable()
    {
        Time.timeScale = 0;
        greyback.In();
        levelmanager.LM.controller.SetActive(false);
    }
     void OnDisable()
    {
        Time.timeScale = 1;
        greyback.Out();
        levelmanager.LM.controller.SetActive(true);
        foreach(GameObject g in tab1_list)
        {
            Destroy(g);
        }
        foreach (GameObject x in tab2_list)
        {
            Destroy(x);
        }
        foreach (GameObject p in tab3_list)
        {
            Destroy(p);
        }
        
    }
    public void selectskill(effectdata skill)
    {
        Debug.Log(skill.Effectname);
        levelmanager.LM.effects.Add(skill);
        gameObject.SetActive(false);
        announcer.SetActive(true);
        announcer.GetComponent<TextMeshProUGUI>().text = skill.Description;
        announcer.GetComponent<RectTransform>().GetChild(1).GetComponent<TextMeshProUGUI>().text = skill.Effectname;
    }



    //present skills to shuffle
    public void InitializeSkills()
    {
        //determine three skills
        effectdata[] effect_pool = levelmanager.LM.worlds.allworlds[levelmanager.LM.current_world].availableEffects;
        List<effectdata> main_effects = new List<effectdata>();
        foreach(effectdata e in effect_pool)
        {
            main_effects.Add(e);
        }
        tab1_targeteffect = main_effects[Random.Range(0, main_effects.Count)];
        main_effects.Remove(tab1_targeteffect);
        tab2_targeteffect = main_effects[Random.Range(0, main_effects.Count)];
        main_effects.Remove(tab2_targeteffect);
        tab3_targeteffect = main_effects[Random.Range(0, main_effects.Count)];
        main_effects.Remove(tab3_targeteffect);
        //
        RectTransform mountpoint1_transform = mountpoint1.GetComponent<RectTransform>();
        GameObject tab1 = Instantiate(skilltab_prefab, mountpoint1_transform.anchoredPosition3D, mountpoint1_transform.rotation);
        RectTransform tab1_transform = tab1.GetComponent<RectTransform>();
        tab1_transform.SetParent(mountpoint1.GetComponent<RectTransform>());
        tab1_transform.anchoredPosition3D = Vector3.zero;
        tab1_transform.localScale = Vector3.one;
         tab1_startorder = Random.Range(0, 3);

        tab1.GetComponent<SpriteRenderer>().sprite = tab1_targeteffect.Displayimage;
        //tab1.GetComponent<Button>().enabled = true;

        Vector3 tabvectoroffset = new Vector3(0, 50, 0);

        tab1_transform.anchoredPosition3D = tab1_transform.anchoredPosition3D + (tabvectoroffset * tab1_startorder);
      
        RectTransform tab1_other1_transform = new RectTransform();
        RectTransform tab1_other2_transform = new RectTransform();

        tab1_list.Clear();
        tab1_list.Add(tab1);
        for (int i = 0; i < 3;i++)
        {
            if(i!=tab1_startorder)
            {
                GameObject tab1_othertab1 = Instantiate(skilltab_prefab, mountpoint1_transform.anchoredPosition3D, mountpoint1_transform.rotation);
                tab1_other1_transform = tab1_othertab1.GetComponent<RectTransform>();
                tab1_other1_transform.SetParent(mountpoint1.GetComponent<RectTransform>());
                tab1_other1_transform.anchoredPosition3D = Vector3.zero;
                tab1_other1_transform.localScale = Vector3.one;
                tab1_other1_transform.anchoredPosition3D = tab1_other1_transform.anchoredPosition3D + (tabvectoroffset*i);
                tab1_othertab1.GetComponent<SpriteRenderer>().sprite = main_effects[Random.Range(0, main_effects.Count)].Displayimage;
                tab1_list.Add(tab1_othertab1);
            }
        }
        tab1_animating = true;
        tab1_animate_shuffling = true;
        tab1_animate_stopping = false;
        tab1_readjusting = false;
        _speed_stopping = speed_stopping;
        _speed_readjusting = speed_readjusting;


        freeshuffling = true;
        freeshuffling_counter = freeshuffling_frames;

        /////////
        //new
        RectTransform mountpoint2_transform = mountpoint2.GetComponent<RectTransform>();
        GameObject tab2 = Instantiate(skilltab_prefab, mountpoint2_transform.anchoredPosition3D, mountpoint2_transform.rotation);
        RectTransform tab2_transform = tab2.GetComponent<RectTransform>();
        tab2_transform.SetParent(mountpoint2.GetComponent<RectTransform>());
        tab2_transform.anchoredPosition3D = Vector3.zero;
        tab2_transform.localScale = Vector3.one;
        tab2_startorder = tab1_startorder;

        tab2.GetComponent<SpriteRenderer>().sprite = tab2_targeteffect.Displayimage;

        Vector3 tab2vectoroffset = new Vector3(0, 50, 0);

        tab2_transform.anchoredPosition3D = tab2_transform.anchoredPosition3D + (tab2vectoroffset * tab2_startorder);

        RectTransform tab2_other1_transform = new RectTransform();
        RectTransform tab2_other2_transform = new RectTransform();

        tab2_list.Clear();
        tab2_list.Add(tab2);
        for (int i = 0; i < 3; i++)
        {
            if (i != tab2_startorder)
            {
                GameObject tab2_othertab1 = Instantiate(skilltab_prefab, mountpoint2_transform.anchoredPosition3D, mountpoint2_transform.rotation);
                tab2_other1_transform = tab2_othertab1.GetComponent<RectTransform>();
                tab2_other1_transform.SetParent(mountpoint2.GetComponent<RectTransform>());
                tab2_other1_transform.anchoredPosition3D = Vector3.zero;
                tab2_other1_transform.localScale = Vector3.one;
                tab2_other1_transform.anchoredPosition3D = tab2_other1_transform.anchoredPosition3D + (tab2vectoroffset * i);
                tab2_othertab1.GetComponent<SpriteRenderer>().sprite = main_effects[Random.Range(0, main_effects.Count)].Displayimage;
                tab2_list.Add(tab2_othertab1);
            }
        }
        tab2_animating = true;
        tab2_animate_shuffling = true;
        tab2_animate_stopping = false;
        tab2_readjusting = false;
        _speed_stopping_tab2 = speed_stopping;
        _speed_readjusting_tab2 = speed_readjusting;

        //////
        RectTransform mountpoint3_transform = mountpoint3.GetComponent<RectTransform>();
        GameObject tab3 = Instantiate(skilltab_prefab, mountpoint3_transform.anchoredPosition3D, mountpoint3_transform.rotation);
        RectTransform tab3_transform = tab3.GetComponent<RectTransform>();
        tab3_transform.SetParent(mountpoint3.GetComponent<RectTransform>());
        tab3_transform.anchoredPosition3D = Vector3.zero;
        tab3_transform.localScale = Vector3.one;
        tab3_startorder = tab1_startorder;

        tab3.GetComponent<SpriteRenderer>().sprite = tab3_targeteffect.Displayimage;

        Vector3 tab3vectoroffset = new Vector3(0, 50, 0);

        tab3_transform.anchoredPosition3D = tab3_transform.anchoredPosition3D + (tab3vectoroffset * tab3_startorder);

        RectTransform tab3_other1_transform = new RectTransform();
        RectTransform tab3_other2_transform = new RectTransform();

        tab3_list.Clear();
        tab3_list.Add(tab3);
        for (int i = 0; i < 3; i++)
        {
            if (i != tab3_startorder)
            {
                GameObject tab3_othertab1 = Instantiate(skilltab_prefab, mountpoint3_transform.anchoredPosition3D, mountpoint3_transform.rotation);
                tab3_other1_transform = tab3_othertab1.GetComponent<RectTransform>();
                tab3_other1_transform.SetParent(mountpoint3.GetComponent<RectTransform>());
                tab3_other1_transform.anchoredPosition3D = Vector3.zero;
                tab3_other1_transform.localScale = Vector3.one;
                tab3_other1_transform.anchoredPosition3D = tab3_other1_transform.anchoredPosition3D + (tab3vectoroffset * i);
                tab3_othertab1.GetComponent<SpriteRenderer>().sprite = main_effects[Random.Range(0, main_effects.Count)].Displayimage;
                tab3_list.Add(tab3_othertab1);
            }
        }
        tab3_animating = true;
        tab3_animate_shuffling = true;
        tab3_animate_stopping = false;
        tab3_readjusting = false;
        _speed_stopping_tab3 = speed_stopping;
        _speed_readjusting_tab3 = speed_readjusting;

        ////////

    }
    // Use this for initialization
    void Start () {
		
	}

    // Time is stopped. Use Update here.
    void Update()
    {
        freeshuffling_counter--;

        if (tab1_animating)
        {
            if (tab1_animate_shuffling)
            {
                foreach (GameObject t in tab1_list)
                {
                    RectTransform t_rec = t.GetComponent<RectTransform>();
                    t_rec.anchoredPosition3D += speed_shuffle * Vector3.down;
                    if (t_rec.anchoredPosition3D.y < -50)
                    {
                        t_rec.anchoredPosition3D = new Vector3(0, 100, 0);
                    }
                }
            }
            if (tab1_animate_stopping)
            {
                if (_speed_stopping > speed_readjusting)
                {
                    _speed_stopping = _speed_stopping - 0.2f;
                }

                if ((tab1_list[0].GetComponent<RectTransform>().anchoredPosition3D.y - (-5)) >= 0.1f)//|| (tab1_list[0].GetComponent<RectTransform>().anchoredPosition3D.y - (-5)) <= -0.1f)
                {
                    foreach (GameObject t in tab1_list)
                    {
                        RectTransform t_rec = t.GetComponent<RectTransform>();
                        t_rec.anchoredPosition3D += _speed_stopping * 0.5f * Vector3.down;
                        if (t_rec.anchoredPosition3D.y < -50)
                        {
                            t_rec.anchoredPosition3D = new Vector3(0, 100, 0);
                        }
                    }
                }
                else
                {
                    tab1_animate_stopping = false;
                    tab1_readjusting = true;
                    _speed_readjusting = speed_readjusting;


                }

            }
            if (tab1_readjusting)
            {
                Debug.Log("readjusting fired");
                if (_speed_readjusting > 0.2)
                {
                    _speed_readjusting = _speed_readjusting - 0.1f;
                }

                if ((tab1_list[0].GetComponent<RectTransform>().anchoredPosition3D.y - (0)) <= -0.1f)//|| (tab1_list[0].GetComponent<RectTransform>().anchoredPosition3D.y - (0)) >= 0.1f)
                {
                    foreach (GameObject t in tab1_list)
                    {
                        RectTransform t_rec = t.GetComponent<RectTransform>();
                        t_rec.anchoredPosition3D += _speed_readjusting * 0.5f * Vector3.up;
                        //if (t_rec.anchoredPosition3D.y < -50)
                        //{
                        //    t_rec.anchoredPosition3D = new Vector3(0, 100, 0);
                        //}
                    }
                }
                else
                {
                    tab1_readjusting = false;
                    tab1_animating = false;

                    tab2_animate_shuffling = false;
                    tab2_animate_stopping = true;
                }

            }

        }
        if (freeshuffling)
        {
            if (freeshuffling_counter < 0)
            {
                if ((tab1_list[0].GetComponent<RectTransform>().anchoredPosition3D.y - (50 * tab1_startorder)) >= -2 && (tab1_list[0].GetComponent<RectTransform>().anchoredPosition3D.y - (50 * tab1_startorder)) <= 2)
                {
                    tab1_animate_shuffling = false;
                    tab1_animate_stopping = true;
                    _speed_stopping = speed_stopping;
                    freeshuffling = false;
                }
            }
        }



        ////new
        /// 
        if (tab2_animating)
        {
            if (tab2_animate_shuffling)
            {
                foreach (GameObject t in tab2_list)
                {
                    RectTransform t_rec = t.GetComponent<RectTransform>();
                    t_rec.anchoredPosition3D += speed_shuffle * Vector3.down;
                    if (t_rec.anchoredPosition3D.y < -50)
                    {
                        t_rec.anchoredPosition3D = new Vector3(0, 100, 0);
                    }
                }
            }
            if (tab2_animate_stopping)
            {
                if (_speed_stopping_tab2 > speed_readjusting)
                {
                    _speed_stopping_tab2 = _speed_stopping_tab2 - 0.2f;
                }

                if ((tab2_list[0].GetComponent<RectTransform>().anchoredPosition3D.y - (-5)) >= 0.1f)//|| (tab1_list[0].GetComponent<RectTransform>().anchoredPosition3D.y - (-5)) <= -0.1f)
                {
                    foreach (GameObject t in tab2_list)
                    {
                        RectTransform t_rec = t.GetComponent<RectTransform>();
                        t_rec.anchoredPosition3D += _speed_stopping_tab2 * 0.5f * Vector3.down;
                        if (t_rec.anchoredPosition3D.y < -50)
                        {
                            t_rec.anchoredPosition3D = new Vector3(0, 100, 0);
                        }
                    }
                }
                else
                {
                    tab2_animate_stopping = false;
                    tab2_readjusting = true;
                    _speed_readjusting_tab2 = speed_readjusting;

                   
                }

            }
            if (tab2_readjusting)
            {
                Debug.Log("readjusting fired");
                if (_speed_readjusting_tab2 > 0.2)
                {
                    _speed_readjusting_tab2 = _speed_readjusting_tab2 - 0.1f;
                }

                if ((tab2_list[0].GetComponent<RectTransform>().anchoredPosition3D.y - (0)) <= -0.1f)//|| (tab1_list[0].GetComponent<RectTransform>().anchoredPosition3D.y - (0)) >= 0.1f)
                {
                    foreach (GameObject t in tab2_list)
                    {
                        RectTransform t_rec = t.GetComponent<RectTransform>();
                        t_rec.anchoredPosition3D += _speed_readjusting_tab2 * 0.5f * Vector3.up;
                        //if (t_rec.anchoredPosition3D.y < -50)
                        //{
                        //    t_rec.anchoredPosition3D = new Vector3(0, 100, 0);
                        //}
                    }
                }
                else
                {
                    tab2_readjusting = false;
                    tab2_animating = false;

                    tab3_animate_shuffling = false;
                    tab3_animate_stopping = true;
                }

            }

           

        }
        if (tab3_animating)
        {
            if (tab3_animate_shuffling)
            {
                foreach (GameObject t in tab3_list)
                {
                    RectTransform t_rec = t.GetComponent<RectTransform>();
                    t_rec.anchoredPosition3D += speed_shuffle * Vector3.down;
                    if (t_rec.anchoredPosition3D.y < -50)
                    {
                        t_rec.anchoredPosition3D = new Vector3(0, 100, 0);
                    }
                }
            }
            if (tab3_animate_stopping)
            {
                if (_speed_stopping_tab3 > speed_readjusting)
                {
                    _speed_stopping_tab3 = _speed_stopping_tab3 - 0.2f;
                }

                if ((tab3_list[0].GetComponent<RectTransform>().anchoredPosition3D.y - (-5)) >= 0.1f)//|| (tab1_list[0].GetComponent<RectTransform>().anchoredPosition3D.y - (-5)) <= -0.1f)
                {
                    foreach (GameObject t in tab3_list)
                    {
                        RectTransform t_rec = t.GetComponent<RectTransform>();
                        t_rec.anchoredPosition3D += _speed_stopping_tab3 * 0.5f * Vector3.down;
                        if (t_rec.anchoredPosition3D.y < -50)
                        {
                            t_rec.anchoredPosition3D = new Vector3(0, 100, 0);
                        }
                    }
                }
                else
                {
                    tab3_animate_stopping = false;
                    tab3_readjusting = true;
                    _speed_readjusting_tab3 = speed_readjusting;
                }

            }
            if (tab3_readjusting)
            {
                Debug.Log("readjusting fired");
                if (_speed_readjusting_tab3 > 0.2)
                {
                    _speed_readjusting_tab3 = _speed_readjusting_tab3 - 0.1f;
                }

                if ((tab3_list[0].GetComponent<RectTransform>().anchoredPosition3D.y - (0)) <= -0.1f)//|| (tab1_list[0].GetComponent<RectTransform>().anchoredPosition3D.y - (0)) >= 0.1f)
                {
                    foreach (GameObject t in tab3_list)
                    {
                        RectTransform t_rec = t.GetComponent<RectTransform>();
                        t_rec.anchoredPosition3D += _speed_readjusting_tab3 * 0.5f * Vector3.up;
                        //if (t_rec.anchoredPosition3D.y < -50)
                        //{
                        //    t_rec.anchoredPosition3D = new Vector3(0, 100, 0);
                        //}
                    }
                }
                else
                {
                    tab3_readjusting = false;
                    tab3_animating = false;

                    //
                    tab1_list[0].GetComponent<Button>().enabled = true;
                    tab1_list[0].GetComponent<Button>().onClick.AddListener(delegate { selectskill(tab1_targeteffect); });
                    tab2_list[0].GetComponent<Button>().enabled = true;
                    tab2_list[0].GetComponent<Button>().onClick.AddListener(delegate { selectskill(tab2_targeteffect); });
                    tab3_list[0].GetComponent<Button>().enabled = true;
                    tab3_list[0].GetComponent<Button>().onClick.AddListener(delegate { selectskill(tab3_targeteffect); });
                }

            }

        }
    }
}
