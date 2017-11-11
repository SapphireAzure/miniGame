using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SkillName
{
    BaseSkill = 1,
    FogSkill = 2,
    ThunderSkill = 3,
}


public class SkillItem : MonoBehaviour
{
    public SkillName skillName = 0;

    //update前第一次调用 设置一个skillname值
    private void Start()
    {
        SkillName[] names = System.Enum.GetValues(typeof(SkillName)) as SkillName[];
        //系统自动选取当前时间为种子
        System.Random random = new System.Random();
        SkillName skillName = names[random.Next(1,1)];
    }

    //进入Trigger得到trigger类型
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerQuality pq= other.GetComponent<Player>().quality;
            pq.HP -= 50;
            return;
        }
    }
}

