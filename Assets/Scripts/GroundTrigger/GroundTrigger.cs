using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//地面出发器 用于player跟地面元素进行交互
//这个类作为Player的组件
public class GroundTrigger
{
    Player player;

    public GroundTrigger(Player player)
    {
        this.player = player;
    }

    //当在地面上时触发检测 修改角色属性值
    public void GroundTrig(RaycastHit2D target)
    {
        PlayerQuality quality = player.quality;
        Tile targetObject = target.collider.gameObject.GetComponent<Tile>();
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
