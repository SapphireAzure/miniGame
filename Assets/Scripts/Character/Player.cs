using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{

    //前面的ground 将collision复位
    private RaycastHit2D formorGround;
    
    //是否面朝右边 图片默认朝向左边
    [SyncVar]
    public bool isFacingRight = false;
    //标记目标是否在下蹲状态
    [SyncVar]
    public bool isSquatting = false;

    //发射器 跟子弹技能方向相关
    public Transform Launcher = null;
    //跟游戏内容相关部分
    //角色数值 与monobehavior脱离
    public PlayerQuality quality;
    //技能列表
    public PlayerSkillSystem skillSystem = null;
    //目前碰到的Item属性
    private Collider2D NowItem;

    private void Awake()
    {
        //跟一些游戏中的组件绑定
        //技能相关发射器绑定
        Launcher = transform.Find("Launcher");
        //技能系统绑定
        skillSystem = gameObject.AddComponent<PlayerSkillSystem>();
        //封装一些角色数值的东西
        quality = new PlayerQuality();
    }

    // Use this for initialization
    void Start ()
    {
        //设定子物体画布当且仅当其是LocalPlayer时才是可见的
        //注意是在产生副本时候判定
       
        if (isLocalPlayer)
        {
            transform.Find("ControlCanvas").GetComponent<Canvas>().enabled = true;
            transform.Find("Camera").GetComponent<Camera>().enabled = true;
            transform.Find("FogCamera").GetComponent<Camera>().enabled = true;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
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
