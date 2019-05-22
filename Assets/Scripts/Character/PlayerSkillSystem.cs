using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//玩家的技能系统 技能系统负责监控技能群 
//主要是让主动被动技能都可以检索判定
//设计的基础技能目前无法放弃
[System.Serializable]
public class PlayerSkillSystem : MonoBehaviour
{
    public Skill[] skills = new Skill[3];

    public void Start()
    {
        skills[0] = gameObject.AddComponent<BaseSkill>();
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
                SplashSkill pss = s as SplashSkill;
                pss.SkillEffect(effectSite);
            }
        }
    }

    //技能系统跟据Item生成对应的技能 放入对应块位置
    public void SetSkill(SkillName skillName, int index)
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
        //在确定范围内则替换技能
        if (targetSkill)
        {
            if (0 < index && index < 3)
            {
                skills[index] = targetSkill;
                //修改canvasGUI名称
                Debug.Log("SkillButton" + index);
                targetSkill.ID = "FOG";
                Debug.Log(targetSkill.ID);
                transform.Find("ControlCanvas").GetComponent<ChangeGUIName>().SetSkillButtonName("SkillButton"+index, targetSkill.ID);
            }
        }
    }
}
