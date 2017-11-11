using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//玩家（orAI)控制器 设计为读取GUI的组件来实现移动
//暂时不继承monobehavior来实现
public class PlayerController
{
    //控制指令集合 由GUI或操控输入的指令
    private InputCommand command;
    //相关联的对象 控制的目标实体
    private Player player;

    //构造函数给定commond
    public PlayerController(InputCommand command, Player player)
    {
        this.command = command;
        this.player = player;
    }


    //释放技能
    public void SpellSkill()
    {
        if(command.attack)
        {
            player.skillSystem.PlaySkill(0, player);
        }
        if(command.skill)
        {
            player.skillSystem.PlaySkill(1, player);
        }
        
    }

    //基础控制方面
    public void Move()
    {
        //更新Launcher的转动角度
        player.Launcher.rotation = new Quaternion(command.horizontal, command.vertical, 0,0);

        Rigidbody2D rb=player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float h = 0f;
            if(command.horizontal>0)
            {
                h = 0.2f;
            }
            if(command.horizontal<0)
            {
                h = -0.2f;
            }
            //处理速度问题 注意有正负号关系
            if (h * rb.velocity.x < player.quality.GetSpeed())
            {
                rb.AddForce(Vector2.right * h * player.quality.moveForce);
            }
            if (Mathf.Abs(rb.velocity.x) > player.quality.GetSpeed())
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * player.quality.GetSpeed(), rb.velocity.y);
            }
            if (h > 0 && !player.isFacingRight) Flip();
            else if (h < 0 && player.isFacingRight) Flip();
            if (player.CanJump)
            {
                if (command.jump)
                {
                    rb.AddForce(new Vector2(0f, player.quality.jumpForce));
                    player.CanJump = false;
                }
            }
        }
    }

    //判断下蹲问题
    public bool IsSquat()
    {
        if(command.vertical<-0.3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    /*
     * 这个函数负责翻转图像
     */
    private void Flip()
    {
        player.isFacingRight = !player.isFacingRight;
        Vector3 innerScale = player.transform.localScale;
        innerScale.x *= -1;
        player.transform.localScale = innerScale;
    }
}
