using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//该类放在Player下为player相关的各种属性值
[System.Serializable]
public class PlayerQuality
{
    //角色生命最大值
    public int maxHp;
    //角色生命值
    public int HP;
    //角色移动速度
    public int speedLevel;
    //回避率
    public int dodge;
    //角色移动等级
    public int[] speeds = { 3, 5, 8, 10, 11 };
    //角色攻击力
    public int attack;
    //角色防御力
    public int defend;


    public PlayerQuality()
    {
        reset();
    }
    //将quality数值回复
    public void reset()
    {
        maxHp = 100;
        HP = 100;
        speedLevel = 1;
        dodge = 0;
        attack = 0;
        defend = 0;
    }
    //获取速度
    public int GetSpeed()
    {
        return speeds[speedLevel];
    }

    //将速度提升一个等级
    public void UpSpeed()
    {
        if(speedLevel < 4)
        {
            speedLevel += 1; 
        }
    }
    //下降一个速度
    public void DownSpeed()
    {
        if(speedLevel>0)
        {
            speedLevel -= 1;
        }
    }

}
