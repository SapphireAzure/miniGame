using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//玩家（orAI)控制器 设计为读取GUI的组件来实现移动
//需要绑定到要操控的角色对象上 要求该对象有一个Player类 将来可以考虑继承化
public class PlayerController : NetworkBehaviour
{
    //控制指令集合 由GUI或操控输入的指令
    private InputCommand command;
    //相关联的对象 控制的目标实体
    public Player player;
    //目标实体的Check对象 主要返回一些周边因素
    private PlayerCheck playerCheck;
    //绑定的动画组件
    private Animator anim;

    //角色对象的刚体组件
    Rigidbody2D rb ;

    //一个墙壁旁边连续跳跃时间限制
    private float wallJumpTime = 2f;

    int count = 0;

    private void Awake()
    {
        //找到GUI面板
        this.command = transform.Find("ControlCanvas").GetComponent<InputCommand>();
        //绑定组件上的Player脚本
        this.player = transform.GetComponent<Player>();
        //绑定组件上的Check脚本
        this.playerCheck = transform.GetComponent<PlayerCheck>();
        //将刚体组件取出来方便操作
        rb = player.GetComponent<Rigidbody2D>();
        //动画器绑定
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(wallJumpTime>=0)
        {
            wallJumpTime -= Time.deltaTime;
        }
        
        SpellSkill();
        SpellSet();
        //判断是否进入下蹲状态
        if (IsSquat())
        {
            player.isSquatting = true;
            anim.SetBool("IsSquatting", true);
        }
        else
        {
            player.isSquatting = false;
            anim.SetBool("IsSquatting", false);
        }
    }

    private void FixedUpdate()
    {
        //移动 因为跟物理相关放在FixedUpdate中
        //Cmd服务器上调用
        CmdMove();
    }

    //释放技能
    public void SpellSkill()
    {
        if(command.attack)
        {
            player.skillSystem.PlaySkill(0, player);
        }
        if(command.skill1)
        {
            player.skillSystem.PlaySkill(1, player);
        }
        if(command.skill2)
        {
            player.skillSystem.PlaySkill(2, player);
        }
        
    }

    //设计为按住技能按键长时间 为拾取地上技能做辅助
    //这里只处理按键时长 时长长于4s若用nowitem则替换对应键
    public void SpellSet()
    {
        if (command.skill1set || command.skill2set)
        {
            if(player.quality.skillGetTime>0)
            {
                player.quality.skillGetTime -= Time.deltaTime;
            }
            else
            {
                if (command.skill1set)
                {
                    player.SkillSystemSetSkill(1);
                }
                else
                {
                    player.SkillSystemSetSkill(2);
                }
            }
        }
        else
        {
            player.quality.skillGetTime = 4;
        }
    }

    //基础控制方面 在服务器端完成移动的数据处理
    public void CmdMove()
    {
        //改为更新player脚本下记录direction方向 模1化
        player.direction = new Vector2(command.horizontal, command.vertical).normalized;

        if (rb != null)
        {
            float h = 0f;
            if(!playerCheck.front && command.horizontal>0)
            {
                h = 0.2f;
            }
            if(!playerCheck.front && command.horizontal<0)
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
            //颠倒物件正面朝向
            if (command.horizontal > 0 && !player.isFacingRight) Flip();
            else if (command.horizontal < 0 && player.isFacingRight) Flip();
            //如果脚下有东西
            if (playerCheck.ground)
            {
                //在下蹲情况下 再按跳跃为向下方翻移 此外加一个向下的力来模拟一个摩擦阻力
                if (player.isSquatting)
                {
                    if (command.jump)
                    {
                        StartCoroutine(Pnetrate(0.4f));
                    }
                    rb.AddForce(new Vector2(0, -10f));
                }
                //若不是下蹲情况 则发生跳跃
                else
                {
                    if (!playerCheck.top && command.jump)
                    {
                        rb.AddForce(new Vector2(0f, player.quality.jumpForce));
                        wallJumpTime = 1f;
                    }
                }
                
            }
            //脚下没有物体 1:在空中 2:其他可能奇怪的情况
            else
            {
                if(playerCheck.top)
                {
                    StartCoroutine(Pnetrate(0.4f));
                }
                //如果右边有东西 注意这个下滑力太小有可能没有效果 被水平力抵消
                if (playerCheck.front)
                {
                    //面向右方且攻击键抓墙 
                    if (Mathf.Abs(command.horizontal) > 0.5)
                    {
                        if (rb.velocity.y < -1.5f)
                        {
                            rb.velocity = new Vector2(rb.velocity.x, -1.5f);
                        }
                        if (command.jump && wallJumpTime <= 0)
                        {
                            rb.AddForce(new Vector2(0f, player.quality.jumpForce));
                            wallJumpTime = 2f;
                        }
                    }
                }
            }
            
            
        }
    }

    //协程事关联player的rigidbody和collider暂时失效
    IEnumerator Pnetrate(float time)
    {
        Collider2D targetCO = transform.GetComponent<Collider2D>();
        targetCO.enabled = false;
        yield return new WaitForSeconds(time);
        targetCO.enabled = true;
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

        Vector3 innerScale = transform.localScale;
        Vector3 maskScale = transform.Find("Mask").localScale;

        innerScale.x *= -1;
        //有些局域坐标不应该变化
        maskScale.x *= -1;


        transform.localScale = innerScale;
        transform.Find("Mask").localScale = maskScale;

        //对GUI组件中recttransform进行修改
        Vector2 guiScale= transform.Find("ControlCanvas").GetComponent<RectTransform>().localScale;
        guiScale.x *= -1;
        transform.Find("ControlCanvas").GetComponent<RectTransform>().localScale = guiScale;
    }
}
