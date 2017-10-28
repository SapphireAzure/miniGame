using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//创建一个实际例子 挂载Player身上
public class BaseSkill : Skill
{
    //都有继承一个Gameobject为创建prefab
    //都有集成一个isActive为是否主动技能

    //伤害
    public int damage = 2;
    //速度
    public int speed = 10;
    //距离
    public float distance = 40f;
    //冷却时间
    public float ColdTime = 2f;
    //冷却计时
    public float NextColdTime = 0;
    //技能释放位置
    public float siteX = 0;
    public float siteY = 0;

    private void Start()
    {
        this.isActive = true;
        this.skillSprite = Resources.Load("Prefabs/Skill/BaseSkillSprite") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(NextColdTime > 0)
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
            GameObject spriteInstance = Instantiate(skillSprite, player.transform.position, player.transform.rotation);
            spriteInstance.GetComponent<BaseSkillSpriteControl>().SetSkill(this);
            spriteInstance.GetComponent<BaseSkillSpriteControl>().SetPlayerReference(player.isFacingRight, player.transform.position);
        } 
    }
}
