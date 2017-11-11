using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //前面的ground 将collision复位
    private RaycastHit2D formorGround;
    //角色脚下的collider
    private RaycastHit2D ground;
    
    //是否面朝右边 图片默认朝向左边
    public bool isFacingRight = false;
    //隐藏 是否在跳跃
    [HideInInspector]
    public bool CanJump = false;
    //隐藏 是否主动下落
    public bool WillDrop = false;
    //标记目标是否在下蹲状态
    public bool IsSquatting = false;
    //发射器 跟子弹技能方向相关
    public Transform Launcher = null;

    //跟游戏内容相关部分
    //角色数值
    public PlayerQuality quality;
    //角色控制部分
    private PlayerController controller;
    //地面检测
    public GroundTrigger gt = null;
    //技能列表
    public PlayerSkillSystem skillSystem = null;
    //检测地面用射击直线
    public Transform groundCheck = null;
    //目前碰到的Item属性
    private Collider2D NowItem;
    //绑定的动画组件
    private Animator anim;


    // Use this for initialization
    void Start ()
    {
        //跟一些游戏中的组件绑定
        groundCheck = transform.Find("GroundCheck");
        Launcher = transform.Find("Launcher");
        skillSystem = gameObject.AddComponent<PlayerSkillSystem>();
        //动画器绑定
        anim = GetComponent<Animator>();
        //封装一些角色数值的东西
        quality = new PlayerQuality();
        //为角色对象则初始化控制组件
        if(this.tag=="Player")
        {
            //从canvas获取得到InputCommond脚本的实例组件
            controller = new PlayerController(GameObject.Find("ControlCanvas").GetComponent<InputCommand>(), this);
        }
        else
        {
            controller = null;
        }
        //地面监测模块
        gt = new GroundTrigger(this);
    }
	
	// Update is called once per frame
	void Update ()
    {
        ground = Physics2D.Linecast(transform.position, groundCheck.position, LayerMask.GetMask("Ground"));
        skillSystem.Check();
        //技能控制器控制释放技能
        controller.SpellSkill();
        //先进行地面检测
        if (ground == true)
        {
            gt.GroundTrig(ground);
            CanJump = true;
        }
        //捡取道具
        if(NowItem)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                skillSystem.SetSkill(NowItem.GetComponent<SkillItem>().skillName);
                Destroy(NowItem.gameObject);
                NowItem = null;
            }
        }
        //生命值为0销毁
        if (quality.HP <= 0)
        {
            Destroy(gameObject);
        }
        //判断是否进入下蹲状态
        if(controller.IsSquat())
        {
            anim.SetBool("IsSquatting", true);
        }
        else
        {
            anim.SetBool("IsSquatting", false);
        }
    }


    private void FixedUpdate()
    {
        controller.Move();
        if(transform.localScale.x<0)
        {
            Debug.Log(transform.localScale);
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
