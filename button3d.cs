using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class button3d : MonoBehaviour, IPointerDownHandler,
    IPointerUpHandler
{
    public UnityEvent pressed;
    public Vector3 orgpos;
    public Vector3 orgscale;
    public float pointerdownoffsety;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("pointer down on cilinder");
        //
        orgpos = gameObject.transform.position;
        orgscale = gameObject.transform.localScale;
        //
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - pointerdownoffsety, gameObject.transform.position.z);
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * 0.8f, gameObject.transform.localScale.y * 0.8f, gameObject.transform.localScale.z * 0.8f);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("pointer up on cilinder");
        gameObject.transform.position = orgpos;
        gameObject.transform.localScale = orgscale;
        pressed.Invoke();
    }
    // Use this for initialization
    void Start () {
        orgpos = gameObject.transform.position;
        orgscale = gameObject.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
