using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//被动节能
//你的技能对任意角色造成伤害时，对角色周围距离为100以内的其他角色造成50%的溅射伤害，每个角色每次只会受到一次溅射伤害。
public class SplashSkill : Skill
{
    private void Start()
    {
        ID = "Splash";
        isActive = false;
        skillSprite = null;
        owner = GetComponent<Player>();
    }



    //执行技能能力调用函数
    public override void PlaySkill()
    {

    }

    public override void SkillEffect(Transform effectSite)
    {
        Debug.Log("change OK");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(effectSite.position, 3);
        foreach(Collider2D t in colliders)
        {
            if(t.tag==Tags.Enemy)
            {
                t.GetComponent<SkillItem>().HP -= 10;
            }
        }
    }
}
