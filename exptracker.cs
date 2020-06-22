using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class exptracker : MonoBehaviour {


    public int currentexp;
    public int currentlevel;

    //animation
    int _oldexp;
    int _filltoexp;
    int _oldlevel;
    float _baranimationcount;
    [SerializeField] private float speed;
    //in scene
    public TextMeshProUGUI bartext;
    public RectTransform barfill_scale;//1 is full 0 is empty

    //int _filltolevel;
    bool _animatebar;
    bool _fillbartotheend;
    //reference
    int[] _expforeachlevel;

    //select skill screen
    public GameObject selectskillscreen;
    bool poplvl1;

    delegate void queuedchanges(int value);
    bool running;
    List<queuedchanges> queue;
    List<int> parameters;
    // Use this for initialization
    void Start()
    {
        queue = new List<queuedchanges>();
        parameters = new List<int>();
    }

    private void OnEnable()
    {
        poplvl1 = true;
        _expforeachlevel = levelmanager.LM.worlds.allworlds[levelmanager.LM.current_world].playerexpneeded_eachlevel;
        currentexp = 0;
        currentlevel = 1;
        bartext.text = currentlevel.ToString();
        //select starting 
        barfill_scale.localScale = Vector3.one;
        Change(0);
    }
    public void AddChange(int value)
    {
        queue.Add(Change);
        parameters.Add(value);
    }
    public void Change(int value)
    {
        _baranimationcount = 0f;
        _fillbartotheend = false;
        //currentexp = 0;
        _oldexp = currentexp;
        _oldlevel = currentlevel;
        currentexp = currentexp + value;
        _filltoexp = currentexp;



        while(_filltoexp > _expforeachlevel[currentlevel-1])
        {
            _fillbartotheend = true;
            _filltoexp -= _expforeachlevel[currentlevel - 1];
            currentlevel++;
        }
        _animatebar = true;
    }

	// Update is called once per frame
	void FixedUpdate () {
        if (!running)
        {
            if (queue.Count != 0)
            {
                queue[0].Invoke(parameters[0]);
                queue.Remove(queue[0]);
                parameters.Remove(parameters[0]);
                running = true;
            }

        }
        if (_animatebar)
        {
            _baranimationcount += speed * Time.deltaTime;
            if(_fillbartotheend)
            {
                if (_baranimationcount < 1f)
                {
                    float f1 = 1f;
                    Vector3 _targetscale = new Vector3(f1, 1, 1);
                    barfill_scale.localScale = Vector3.Lerp(barfill_scale.localScale, _targetscale, _baranimationcount);
                }
                else
                {
                    currentexp = 0;
                    _oldexp = 0;
                    _baranimationcount = 0f;
                    _oldlevel++;
                    bartext.text = _oldlevel.ToString();
                    barfill_scale.localScale = new Vector3(0, 1, 1);
                    if (_oldlevel==currentlevel)
                    {
                        _fillbartotheend = false;
                    }
                    gamemanager.GM.queuemanager.queue.Add(PopSelectSkill) ;
                }
            }
            else
            {
                if(_baranimationcount<1f)
                {
                    float f0 = ((float)_oldexp / _expforeachlevel[currentlevel - 1]);
                    Vector3 _fromscale = new Vector3(f0, 1, 1);
                    float f1 = ((float)_filltoexp / _expforeachlevel[currentlevel - 1]);
                    Vector3 _targetscale = new Vector3(f1, 1, 1);

                    barfill_scale.localScale = Vector3.Lerp(_fromscale, _targetscale, _baranimationcount);
                }
                else
                {
                    currentexp = _filltoexp;
                    _animatebar = false;
                    running = false;
                    if (poplvl1)
                    {
                        poplvl1 = false;

                        gamemanager.GM.queuemanager.queue.Add(PopSelectSkill);
                        
                    }
                }

            }
        }
	}
    void PopSelectSkill()
    {
        Debug.Log("select skill screen has been popped");
        selectskillscreen.SetActive(true);
    }
}
