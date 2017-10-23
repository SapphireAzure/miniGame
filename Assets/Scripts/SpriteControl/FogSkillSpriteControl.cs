using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogSkillSpriteControl : MonoBehaviour
{
    //关联的技能部分
    FogSkill fogSkill;
    //持续时间部分
    float stillDuration = 0f;


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(stillDuration<fogSkill.duration)
        {
            stillDuration += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
	}

    public void SetSkill(FogSkill skill)
    {
        this.fogSkill = skill;
    }
}
