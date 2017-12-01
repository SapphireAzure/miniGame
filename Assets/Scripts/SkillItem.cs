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
    public SkillName skillName;

    //用item来测试技能效果用HP
    public int HP = 100;

    //绘制box的大小
    private int height = 20;
    private int length = 100;

    //触发物体
    private Transform trigObject;

    //update前第一次调用 设置一个skillname值
    private void Start()
    {
        SkillName[] names = System.Enum.GetValues(typeof(SkillName)) as SkillName[];
        //系统自动选取当前时间为种子
        System.Random random = new System.Random();
        //现在默认为Fog技能
        SkillName skillName = names[1];
    }

    //用onGui绘制一个message说明信息 这个OnGUI跟玩家Camera相关联
    //目前采用GUI方法解决 后面考虑使用产生子物体的方法
    private void OnGUI()
    {
        if(trigObject)
        {
            Vector3 GUISite = trigObject.Find("Camera").GetComponent<Camera>().WorldToScreenPoint(transform.position);
            GUI.Box(new Rect(GUISite.x-length/2, Screen.height- GUISite.y-45, length, height), SkillNameToString());
        }
    }

    //进入Trigger得到触发的类型 显示一个气泡对话框
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            trigObject = other.transform;
        }
    }

    private string SkillNameToString()
    {
        switch(skillName)
        {
            case SkillName.FogSkill: return "雾霾技能";
            case SkillName.ThunderSkill:return "雷电技能";
        }
        return "随机技能";
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            trigObject = null;
        }
    }
}

