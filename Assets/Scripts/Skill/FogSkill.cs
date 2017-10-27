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
        this.isActive = true;
        this.skillSprite = Resources.Load("Prefabs/Skill/FogSkillSprite") as GameObject;
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

    public override void PlaySkill(Player player)
    {
        if(NextColdTime<=0)
        {
            NextColdTime = ColdTime;
            Debug.Log(player.transform.position);
            GameObject spriteInstance = Instantiate(skillSprite, player.transform.position, player.transform.rotation);
            spriteInstance.GetComponent<FogSkillSpriteControl>().SetSkill(this);

        }
    }
}
