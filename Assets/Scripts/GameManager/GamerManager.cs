using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GamerManager : NetworkManager
{
    public int leftBound = -10;
    public int rightBound = 10;

    private void Start()
    {
        
    }
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        //坐在物件 gameManager的位置
        Vector3 site = transform.position;
        site.x += Random.Range(leftBound, rightBound);
        var player = (GameObject)GameObject.Instantiate(playerPrefab, site,transform.rotation);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}
