using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这个为所有技能的父类
/* 思路是这样的 这个类类似于Instance产生器
 * 因为每一个技能就是一个sprite动画 和一系列collider的触发器的组合
 * 由于目前技能还少我们就把动画和触发先黏合在一起
 * 技能只负责数值 与 产生效果调用trigger
 * 后面将技能动画 trigger试着分开 以便于技能扩展
 */
 [System.Serializable]
public abstract class Skill : MonoBehaviour
{
    //技能可能的共有部分
    public GameObject skillSprite;
    //是否为主动技能
    public bool isActive;

    public abstract void PlaySkill(Player player);
}
