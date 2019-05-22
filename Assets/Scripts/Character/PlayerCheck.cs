using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//该类实例于player上 获取check组件 考其几个自组建 用于进行各项判定
//该对象目前涉及有四个方向检测周围物体情况
public class PlayerCheck : MonoBehaviour
{
    //方向检测物体
    private Transform check;
    //Check子物体组件
    private Transform groundCheck;
    private Transform backCheck;
    private Transform frontCheck;
    private Transform topCheck;

    //检测结果 目前当成成员变量
    public RaycastHit2D ground;
    public RaycastHit2D back;
    public RaycastHit2D front;
    public RaycastHit2D top;

    //绑定在同一物体上的player脚本
    public Player player;
    //由检定返回的一些影响
    private void Awake()
    {
        //检测部分绑定
        check = transform.Find("Check");
        groundCheck = check.Find("GroundCheck");
        backCheck = check.Find("BackCheck");
        frontCheck = check.Find("FrontCheck");
        topCheck = check.Find("TopCheck");
        //同一物体上的player脚本绑定
        player = transform.GetComponent<Player>();
    }

    // Use this for initialization
    void Start ()
    {	
	}
	
	// Update is called once per frame
	void Update ()
    {
        int layer = 0;
        layer |= LayerMask.GetMask("Ground");
        //时刻检测并更新四周的接触情况
        ground = Physics2D.Linecast(transform.position, groundCheck.position, layer);
        back = Physics2D.Linecast(transform.position, backCheck.position, layer);
        front = Physics2D.Linecast(transform.position, frontCheck.position, layer);
        top= Physics2D.Linecast(transform.position, topCheck.position, layer);
        GroundTrig();
    }


    //当在地面上时触发检测 修改角色属性值
    public void GroundTrig()
    {
        if(ground)
        {
            PlayerQuality quality = player.quality;
            Tile targetObject = ground.collider.gameObject.GetComponent<Tile>();
            if (targetObject != null)
            {
                //获取射线撞击目标的类型
                Tile.TileType tileType = targetObject.GetTileType();
                if (tileType == Tile.TileType.PLAIN)
                {
                    quality.PropertyReset();
                }
                else if (tileType == Tile.TileType.GRASS)
                {
                    quality.PropertyReset();
                    quality.DownSpeed();
                    quality.dodge = 15;
                }
                else if (tileType == Tile.TileType.ICE)
                {
                    quality.PropertyReset();
                    quality.UpSpeed();
                    quality.attack += 1;
                }
                else if (tileType == Tile.TileType.SWAMP)
                {
                    quality.PropertyReset();
                    quality.DownSpeed();
                    quality.defend -= 1;
                }
                else if (tileType == Tile.TileType.RUINS)
                {

                }
            }
            else
            {
                quality.PropertyReset();
            }
        }
    }
}
