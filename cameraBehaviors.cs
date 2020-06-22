using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraBehaviors : MonoBehaviour {

    //Camera thiscamera;

    public SpriteRenderer whitesprite;
    [SerializeField]private float whiteinout_speed;
    private float _whitecount;
    [SerializeField]
    private Color whiteinfrom;
    [SerializeField]
    private Color whiteinto;
    public bool _whitein;
    public bool _whiteout;
     //private bool _hasqueued_whiteout;
     //private cameraBehaviors _lastanimatingcamera;

    public void WhiteIn()
    {
        _whitecount = 0f;
        _whitein = true;
        Debug.Log("white in did play", gameObject);     
    }
    public void WhiteOut()//cameraBehaviors lastcamera)
    {

        whitesprite.gameObject.SetActive(true);
        whitesprite.color = whiteinto;
      //  gameObject.GetComponent<Camera>().enabled = false;
       // gameObject.GetComponent<AudioListener>().enabled = false;
        gameObject.SetActive(true);
       // if (lastcamera._whitein) { _hasqueued_whiteout = true; _lastanimatingcamera = lastcamera; }
       // else {
            gameObject.GetComponent<Camera>().enabled = true;
            gameObject.GetComponent<AudioListener>().enabled = true;
       // }
        _whitecount = 0f;
        _whiteout = true;
    }
	
	// Update is called once per frame
	void FixedUpdate () 
    {   
        if (_whitein)//when needed to whitein
        {
                if (_whitecount < 1)//if still in progress
                {
                    _whitecount += whiteinout_speed * Time.deltaTime;
                    whitesprite.color = Color.Lerp(whiteinfrom, whiteinto, _whitecount);
                }
                else//whitein finished, stop
                {
                    _whitein = false;
                    
                //safe to run the next queued function
                gamemanager.GM.queuemanager.running = false;
                gameObject.SetActive(false);
            }
        }
                
        if (_whiteout)//when needed to whitein
        {
                
            //if(_hasqueued_whiteout)
           // {
              //  if (!_lastanimatingcamera._whitein)
              //  {  
              //      _hasqueued_whiteout = false; 
               //     gameObject.GetComponent<Camera>().enabled = true;
               //     gameObject.GetComponent<AudioListener>().enabled = true;
            //    }
         //   }
         //   else
         //   {

                if (_whitecount < 1)//if still in progress
                {

                    _whitecount += whiteinout_speed * Time.deltaTime;
                    whitesprite.color = Color.Lerp(whiteinto, whiteinfrom, _whitecount);
                }
                else//whitein finished, stop
                {
                    _whiteout = false;
                //safe to run the next queued function
                gamemanager.GM.queuemanager.running = false;
            }
         //   }
                
           
        }
    }
}
