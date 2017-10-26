using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //这个Tile的类型 用以实现不同的地形效果
    public enum TileType
    {
        PLAIN=1,
        GRASS=2,
        SWAMP=3,
        ICE=4,
        RUINS=5,
    }
    //地板格子类型
    public TileType tielType = TileType.PLAIN;


    public TileType GetTileType()
    {
        return this.tielType;
    }
}
