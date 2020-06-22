using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popupgreyback : MonoBehaviour {
    public Image greyback;
    [SerializeField] Color fromcolor;
    [SerializeField] Color tocolor;
    [SerializeField] float speed;
    [SerializeField] float greyback_count;
    [SerializeField] bool greyback_in;
    [SerializeField] bool greyback_out;
    // Use this for initialization
    void Start () {
		
	}

    public void In()
    {
        greyback.color = fromcolor;
        gameObject.SetActive(true);
        greyback_count = 0f;
        greyback_in = true;
    }
    public void Out()
    {
        greyback.color = tocolor;
        greyback_count = 0f;
        greyback_out = true;

    }
    // Update is called once per frame
    void Update()
    {
        greyback_count += speed * Time.unscaledDeltaTime;
        if (greyback_in)
        {
            if (greyback_count < 1f)
            {
                greyback.color = Color.Lerp(fromcolor, tocolor, greyback_count);
            }
            else
            { 
                greyback_in = false; 
                //safe to run the next queued function
                gamemanager.GM.queuemanager.running = false; 
            }

        }
        if (greyback_out)
        {
            if (greyback_count < 1f)
            {
                greyback.color = Color.Lerp(tocolor, fromcolor, greyback_count);
            }
            else
            { 
                greyback_out = false;
                //safe to run the next queued function
                gamemanager.GM.queuemanager.running = false;
                gameObject.SetActive(false); 

            }

        }

    }
}
