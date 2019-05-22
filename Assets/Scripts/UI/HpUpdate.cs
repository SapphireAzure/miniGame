using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HpUpdate : MonoBehaviour {
	public PlayerQuality playerQuality;

	public GameObject player;

	public Scrollbar hpBar;

	float currentHp;

	float maxHp;

	// Use this for initialization
	void Start ()
    {

        playerQuality = GetComponentInParent<Player>().quality;
        set();
	}
	
	// Update is called once per frame
	void Update (){
        set();
    }

    //HP的更新数值设定
    private void set()
    {
        maxHp = playerQuality.maxHp;

        currentHp = playerQuality.HP;

        hpBar.size = currentHp / maxHp;
    }
}
