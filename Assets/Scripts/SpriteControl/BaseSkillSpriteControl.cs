using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这是一个基础技能动画的控制器
//负责动画播放与判定问题
//后面需要慢慢细化分类
public class BaseSkillSpriteControl : MonoBehaviour
{
    //每个sprite跟其skill部分关联这样可以跟skill的数据设计部分结合设计触发
    BaseSkill baseSkill;
    bool facingRight;
    Vector3 position;
    Vector2 direction;

    // Use this for initialization
    void Start ()
    {
	}

    // Update is called once per frame
    void Update()
    {
        if (baseSkill != null)
        {
            Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
            if (rb2D != null)
            {
                //方向为0则以朝向为准
                if (direction.x==0&& direction.y==0)
                {
                    if(facingRight)
                    {
                        rb2D.velocity = new Vector2(baseSkill.speed, 0);
                    }
                    else
                    {
                        rb2D.velocity = new Vector2(-baseSkill.speed, 0);
                    }
                    
                }
                else
                {
                    rb2D.velocity = direction.normalized * baseSkill.speed;
                }
                
            }
            //超过距离范围摧毁这个
            if (Vector3.Distance(transform.position, position) > baseSkill.distance)
            {
                Destroy(gameObject);
            }
        }
    }


    //这个动画效果本身是一个trigger 碰到其他带有敌人标签 则触发
    private void OnTriggerEnter2D(Collider2D other)
    {
        //为敌人触发
        if(other.tag=="Player" && !other.GetComponent<Player>().isLocalPlayer)
        {
            //用个小物件测试效果
            PlayerQuality temp=other.GetComponent<Player>().quality;
            //调用技能拥有者 技能系统下的检查来调用被动技能buff
            baseSkill.owner.skillSystem.Check(other.transform);
            temp.HP -= baseSkill.damage;
            Destroy(this.gameObject);
        }
    }


    //设定这个实例相关技能各种参数
    public void SetSkill(BaseSkill s)
    {
        this.baseSkill = s;
        
    }

    //跟这个动画产生位置相关属性
    public void SetPlayerReference(bool facingRight, Vector3 position, Vector2 direction)
    {
        this.facingRight = facingRight;
        this.position = position;
        this.direction = new Vector2(direction.x, direction.y);
    }
}
