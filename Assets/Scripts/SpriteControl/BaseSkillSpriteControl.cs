using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这是一个基础技能动画的控制器
//后面需要慢慢细化分类
public class BaseSkillSpriteControl : MonoBehaviour
{
    //每个sprite跟其skill部分关联这样可以跟skill的数据设计部分结合设计触发
    BaseSkill baseSkill;
    bool facingRight;
    Vector3 position;
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
                float speed = 0;
                if(facingRight)
                {
                    speed = baseSkill.speed;
                }
                else
                {
                    speed = -baseSkill.speed;
                }
                rb2D.velocity = new Vector2(speed, 0);
            }
            //超过距离范围摧毁这个
            if (Vector3.Distance(transform.position, position) > baseSkill.distance)
            {
                Destroy(gameObject);
            }
        }

        
        
    }

    //设定这个实例的各种参数
    public void SetSkill(BaseSkill s)
    {
        this.baseSkill = s;
        
    }

    public void SetPlayerReference(bool facingRight,Vector3 position)
    {
        this.facingRight = facingRight;
        this.position = position;
    }
}
