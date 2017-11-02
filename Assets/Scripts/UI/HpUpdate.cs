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
	void Start () {
		//暂时无法绑定player的quality..
		playerQuality = new PlayerQuality();
	
		maxHp = playerQuality.maxHp;

		currentHp = playerQuality.HP;

		hpBar.size = currentHp / maxHp;

	}
	
	// Update is called once per frame
	void Update () {
		maxHp = playerQuality.maxHp;

		currentHp = playerQuality.HP;
			
		hpBar.size = currentHp / maxHp;
		
	}
}
