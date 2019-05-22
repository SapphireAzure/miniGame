using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//变更脚本放置到整体player上
public class FogMaskClear : MonoBehaviour
{
    // 光照半径
    public float range = 8;

    // 光照质量，数值越低，向四周辐射的射线越多，效果越好，但性能越低
    public int levelOfDetails = 1;

    // 接受光照产生阴影的对象
    public LayerMask[] layerMask;
    //内外部分离不用 反复改名字
    private int triIndex = 0;
    private int lod = 1;
    private float width;
    private Mesh mesh;
    private Vector3 worldPos;

    private Vector3[] verts;
    private int[] tris;
    private Vector2[] uvs;

    //参考层
    private int mask;
    //相关角色
    private Player player;
    //相关角色数值
    private PlayerQuality playerQuality;
    //角色朝向
    private Vector2 direction;

    // Code that runs on entering the state.
    public void Start()
    {
        //获得网格图形组件
        mesh = new Mesh();
        //获取自组建Mask
        GetComponent<MeshFilter>().mesh = mesh;
        
        lod = levelOfDetails;
        width = range;

        player = transform.parent.GetComponent<Player>();
        playerQuality = player.quality;
        //若loa为1，则共发射360条射线作为光线，则有360个顶点加个圆心
        verts = new Vector3[(playerQuality.sightAngular / lod) + 1];
        //每两个顶点跟圆心组成一个三角形，所以三角形的个数为定点数乘3
        tris = new int[(playerQuality.sightAngular / lod) * 3];

        uvs = new Vector2[verts.Length];

        for (int i = 0; i < layerMask.Length; ++i)
        {
            mask |= layerMask[i];
        }
    }

    // Code that runs every frame.
    public void Update()
    {
        int index = 0;
        triIndex = 0;
        //组件在世界坐标中的位置
        worldPos = transform.position;

        verts[index] = new Vector3(0,0,0);

        index++;


        if (player.direction.x ==0 && player.direction.y ==0)
        {
            if (player.isFacingRight)
            {
                direction = new Vector2(1, 0);
            }
            else
            {
                direction = new Vector2(-1, 0);
            }
        }
        else
        {
            direction = player.direction;
        }
        float xa;
        float ya;
        for (var a = playerQuality.sightAngular/2; a > -playerQuality.sightAngular/2; a -= lod)
        {
            xa = Mathf.Cos(Mathf.Deg2Rad * a);
            ya = Mathf.Sin(Mathf.Deg2Rad * a);
            //从12点钟顺时针画圆弧射线探索
            Vector2 oneRay = new Vector2(xa * direction.x - ya * direction.y, ya * direction.x + xa * direction.y);

            RaycastHit2D hit2D =  Physics2D.Raycast(worldPos, oneRay, width, mask);
            if (hit2D)
            {
                // 如果被射线探中，则将探测到的点作为网格的顶点
                verts[index] = new Vector3(hit2D.point.x-transform.position.x, hit2D.point.y - transform.position.y, worldPos.z);

            }
            else
            {
                // 否则将射线的末端作为网格的顶点
                verts[index] = new Vector3(oneRay.x*width, oneRay.y*width,  worldPos.z);
            }
            index++;
        }

        // 根据网格顶点组合三角形
        for (var i = 1; i < (playerQuality.sightAngular / lod); i++)
        {
            tris[triIndex] = 0;
            tris[triIndex + 1] = i;
            tris[triIndex + 2] = i + 1;
            triIndex += 3;

        }

        tris[((playerQuality.sightAngular / lod) * 3) - 3] = 0;
        tris[((playerQuality.sightAngular / lod) * 3) - 2] = playerQuality.sightAngular / lod;
        tris[((playerQuality.sightAngular / lod) * 3) - 1] = 1;

        // 网格贴图
        int j = 0;
        while (j < uvs.Length)
        {
            uvs[j] = new Vector2(verts[j].x, verts[j].y);
            j++;
        }

        // 重新组合网格
        mesh.Clear();
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }
}
