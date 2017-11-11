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
    Quaternion quaternion;

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
                float xSpeed;
                float ySpeed;
                if (quaternion.x==0&&quaternion.y==0)
                {
                    if(facingRight)
                    {
                        xSpeed = baseSkill.speed;
                        ySpeed = 0;
                    }
                    else
                    {
                        xSpeed = -baseSkill.speed;
                        ySpeed = 0;
                    }
                    
                }
                else
                {
                    xSpeed = quaternion.x / Mathf.Sqrt(quaternion.x * quaternion.x + quaternion.y * quaternion.y) * baseSkill.speed;
                    ySpeed = quaternion.y / Mathf.Sqrt(quaternion.x * quaternion.x + quaternion.y * quaternion.y) * baseSkill.speed;
                }
                


                rb2D.velocity = new Vector2(xSpeed, ySpeed);
            }
            //超过距离范围摧毁这个
            if (Vector3.Distance(transform.position, position) > baseSkill.distance)
            {
                Destroy(gameObject);
            }
        }
    }


    //这个动画效果本身是一个trigger 碰到其他如
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Enemy")
        {
            Destroy(this.gameObject);
        }
    }


    //设定这个实例相关技能各种参数
    public void SetSkill(BaseSkill s)
    {
        this.baseSkill = s;
        
    }

    //跟这个动画产生位置相关属性
    public void SetPlayerReference(bool facingRight, Vector3 position, Quaternion quaternion)
    {
        this.facingRight = facingRight;
        this.position = position;
        this.quaternion = new Quaternion(quaternion.x,quaternion.y,quaternion.z,quaternion.w);
    }
}
