using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //玩家player
    private Transform player;
    //player到camera之间的向量
    private Vector3 playerToCameraVec;


    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerToCameraVec = transform.position - player.position;
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    private void FixedUpdate()
    {
        transform.position = player.position+ playerToCameraVec;
    }
}
