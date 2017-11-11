using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //玩家player
    private Transform player;
    //player到camera之间的向量 偏移量
    private Vector3 offset;
    //
    public float smothing = 0f;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        offset = transform.position - player.position;
    }

    // Use this for initialization
    void Start ()
    {
        transform.position = player.position;

    }
	
	// Update is called once per frame
	void Update ()
    {
        //确认目标角色存在
        if(player!=null)
        {
            //canjump说明在地面上那么镜头应该要往上一点
            if (player.GetComponent<Player>().CanJump)
            {
                Vector3 targetCampos = player.position + offset;
                transform.position = Vector3.Lerp(transform.position, targetCampos, smothing * Time.deltaTime);
            }
            else
            {
                Vector3 targetCampos = player.position + offset;
                transform.position = Vector3.Lerp(transform.position, targetCampos, smothing * Time.deltaTime);
            }
            
        }
        
    }
}
