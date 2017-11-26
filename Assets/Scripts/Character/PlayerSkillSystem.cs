using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//玩家的技能系统 技能系统负责监控技能群 
//主要是让
[System.Serializable]
public class PlayerSkillSystem : MonoBehaviour
{
    public Skill[] skills = new Skill[4];

    public void Start()
    {
        skills[0] = gameObject.AddComponent<BaseSkill>();
        skills[1] = gameObject.AddComponent<PassiveSplashSkill>();
    }

    //技能系统调用 输入一个index表示位置
    public void PlaySkill(int index,Player player)
    {
        if(skills[index])
        {
            skills[index].PlaySkill();
        }
        else
        {
            Debug.Log("技能还为空！");
        }
    }

    //这个一直监控更新技能管理器下面的技能
    public void Check(Transform effectSite)
    {
        foreach (Skill s in skills)
        {
            //技能不为空且为被动技能
            if(s!=null&&!s.isActive)
            {
                //将技能转化为子类来调用 （为了后面继续扩展准备）
                PassiveSplashSkill pss = s as PassiveSplashSkill;
                pss.SkillEffect(effectSite);
            }
        }
    }

    //技能系统跟据Item生成对应的技能
    public void SetSkill(SkillName skillName)
    {
        Skill targetSkill = null;
        switch (skillName)
        {
            case SkillName.FogSkill:
                targetSkill = gameObject.AddComponent<FogSkill>();
                break;
            case SkillName.ThunderSkill:
                targetSkill = gameObject.AddComponent<FogSkill>();
                break;

        }
        //如果确定有其技能
        if(targetSkill)
        {
            for (int i = 0;i<skills.Length;i++)
            {
                if(skills[i]==null)
                {
                    skills[i] = targetSkill;
                }
            }
        }
    }

}
