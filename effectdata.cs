using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

[CreateAssetMenu(fileName = "New EffectData", menuName = "effect data", order = 2)]
public class effectdata : ScriptableObject {

    [SerializeField] private string effectname;
    [SerializeField] private Sprite displayimage;
    [SerializeField] private string description;
    [SerializeField] private bool frontshoot_plus1;
    [Header("projectile effects")]
    [SerializeField] private bool pierceenemy;
    [SerializeField] private bool bouncewall;
    [SerializeField] private bool bounceenemy;
    [SerializeField] private bool splitinghit;
    public string Effectname
    {
        get
        {
            return effectname;
        }
    }
    public Sprite Displayimage
    {
        get
        {
            return displayimage;
        }
    }
    public string Description
    {
        get
        {
            return description;
        }
    }
    public void PlayerBehavior(GameObject playerobj)//the playerobj refers to controller.gameObject, not playerparent or player
    {
        if(frontshoot_plus1)
        {
            playerobj.GetComponent<playercontroller>().arrowspershot++;
        }
    }
    public void Projectile(GameObject projectileobj)
    {
        var _component = projectileobj.GetComponent<arrow>();
        if (pierceenemy)
        {
            _component.pierceenemy = true;
        }
        if(bouncewall)
        {
            _component.bouncewall = true;
        }
        if(bounceenemy)
        {
            _component.bounceenemy = true;
        }
        if(splitinghit)
        {
            _component.splitinghit = true;
        }
    }
    public void Enemy(GameObject enemyobj)
    {

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
