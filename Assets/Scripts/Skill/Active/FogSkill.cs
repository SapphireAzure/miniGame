using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogSkill : Skill
{

    //伤害
    public int damage = 2;
    //持续时间
    public float duration = 15f;
    //冷却时间
    public float ColdTime = 60f;
    //冷却计时
    public float NextColdTime = 0f;

    // Use this for initialization
    void Start()
    {
        ID = "FogSkill";
        isActive = true;
        skillSprite = Resources.Load("Prefabs/Skill/FogSkillSprite") as GameObject;
        owner = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (NextColdTime > 0)
        {
            NextColdTime -= Time.deltaTime;
        }
        else
        {
            NextColdTime = 0;
        }
    }

    public override void PlaySkill()
    {
        if(NextColdTime<=0)
        {
            NextColdTime = ColdTime;
            GameObject spriteInstance = Instantiate(skillSprite, owner.transform.position, owner.transform.rotation);
            spriteInstance.GetComponent<FogSkillSpriteControl>().SetSkill(this);

        }
    }

    public override void SkillEffect(Transform effectSite)
    {
    }
}
