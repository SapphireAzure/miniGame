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
        skills[1] = gameObject.AddComponent<FogSkill>();
    }

    //技能系统调用 输入一个index表示位置
    public void PlaySkill(int index,Player player)
    {
        skills[index].PlaySkill(player);
    }

    //这个一直监控更新技能管理器下面的技能
    public void Check()
    {
        foreach (Skill s in skills)
        {

        }
    }
}
