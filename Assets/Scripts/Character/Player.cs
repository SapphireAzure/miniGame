using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //水平受力大小
    public float moveForce = 365f;
    //竖直受力大小
    public float jumpForce = 1000f;
    //前面的ground 将collision复位
    private RaycastHit2D formorGround;
    //角色脚下的collider
    private RaycastHit2D ground;
    
    //隐藏是否面朝右边 图片默认朝向左边
    public bool isFacingRight = false;
    //隐藏是否在跳跃
    [HideInInspector]
    public bool CanJump = false;
    //隐藏是否主动下落
    public bool WillDrop = false;

    //跟游戏内容相关部分
    //角色数值
    public PlayerQuality quality;
    //地面检测
    public GroundTrigger gt;
    //技能列表
    public PlayerSkillSystem skillSystem;
    //检测地面用射击直线
    public Transform groundCheck;
    //目前脚底下的Item属性
    private Collider2D NowItem;

    // Use this for initialization
    void Start ()
    {
        groundCheck = transform.Find("GroundCheck");
        skillSystem = gameObject.AddComponent<PlayerSkillSystem>();
        quality = new PlayerQuality();
        gt = new GroundTrigger(this);
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        ground = Physics2D.Linecast(transform.position, groundCheck.position, LayerMask.GetMask("Ground"));
        skillSystem.Check();
        if(ground == true)
        {
            //先进行地面检测
            gt.GroundTrig(ground);
            //如果在跳跃过程中要无视
            if (Input.GetKeyDown(KeyCode.W))
            {
                CanJump = true;
            }
        }
        if(Input.GetKeyDown("j"))
        {
            skillSystem.PlaySkill(0,this);
        }
        if(Input.GetKeyDown("k"))
        {
            skillSystem.PlaySkill(1, this);
        }
        //捡取道具
        if(NowItem)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                skillSystem.SetSkill(NowItem.GetComponent<SkillItem>().skillName);
                Destroy(NowItem);
                NowItem = null;
            }
        }
    }

    /*
     * 这个函数负责翻转图像
     */
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 innerScale = transform.localScale;
        innerScale.x *= -1;
        transform.localScale = innerScale;
    }

    private void FixedUpdate()
    {
        BaseControl();
    }

    //游戏中的基础 移动控制
    private void BaseControl()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if(rb !=null)
        {
            float h = 0;
            if (Input.GetKey("a"))
            {
                h = -0.1f;
            }
            if (Input.GetKey("d"))
            {
                h = 0.1f;
            }
            //处理速度问题 注意有正负号关系
            if (h * rb.velocity.x < quality.GetSpeed())
            {
                rb.AddForce(Vector2.right * h * moveForce);
            }
            if (Mathf.Abs(rb.velocity.x) > quality.GetSpeed())
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * quality.GetSpeed(), rb.velocity.y);
            }
            if (h > 0 && !isFacingRight) Flip();
            else if (h < 0 && isFacingRight) Flip();
            if (CanJump)
            {
                rb.AddForce(new Vector2(0f, jumpForce));
                CanJump = false;
            }
            if (WillDrop)
            {
                formorGround = ground;
                Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), ground.collider, true);
                WillDrop = false;
            }
        }
    }

    //进入Trigger得到trigger类型
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "SkillItem")
        {
            NowItem = other;
        }
    }
    //当出了Item Trigger时应当取消绑定
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(NowItem)
        {
            NowItem = null;
        }
    }
}
