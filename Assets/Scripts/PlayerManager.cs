using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class PlayerManager
{

    public PlayerInfo myPlayerInfo;

    public Dictionary<long, PlayerInfo> playerInfoMap = new Dictionary<long, PlayerInfo>();
    public Dictionary<string, PlayerInfo> name2playerInfoMap = new Dictionary<string, PlayerInfo>();


    public void PutPlayerInfo(PlayerInfo playerInfo)
    {
        if (!playerInfoMap.ContainsKey(playerInfo.id)) {
            
            playerInfoMap.Add(playerInfo.id , playerInfo);
        }
        if (playerInfo.name == null) {
            Debug.Log("playerInfo.name == null ===================");
        }else
        if (!name2playerInfoMap.ContainsKey(playerInfo.name)) { 
            name2playerInfoMap.Add(playerInfo.name , playerInfo);
        }
    }

    public PlayerInfo GetPlayerInfo(string name)
    {
        PlayerInfo playerInfo;
        name2playerInfoMap.TryGetValue(name,out playerInfo);

        return playerInfo;
    }

    public PlayerInfo GetPlayerInfo(long playerId)
    {
        PlayerInfo playerInfo;
        playerInfoMap.TryGetValue(playerId, out playerInfo);

        return playerInfo;
    }


    public void RemovePlayerInfo(String name)
    {
        //TODO
        //playerInfoMap.TryGetValue(playerInfo.id, playerInfo);
        //name2playerInfoMap.TryGetValue(name, playerInfo);
    }
    public void RemoveAllPlayerInfo()
    {
        //TODO
        //playerInfoMap.TryGetValue(playerInfo.id, playerInfo);
        //name2playerInfoMap.TryGetValue(name, playerInfo);
        playerInfoMap.Clear();
        name2playerInfoMap.Clear();

        myPlayerInfo.gameObject = null;
        myPlayerInfo.bullet = null;
    }


    public void Update() {

        if (myPlayerInfo == null)
        {
            //Debug.LogError("myPlayerInfo == null");
            return;
        }

        if (myPlayerInfo.gameObject == null ) {
            return;
        }

        msg.ClientFrameMsg clientFrameMsg = new msg.ClientFrameMsg();
        clientFrameMsg.posi = new msg.Vector2Msg();
        clientFrameMsg.posi.x = myPlayerInfo.gameObject.transform.position.x;
        clientFrameMsg.posi.y = myPlayerInfo.gameObject.transform.position.y;
        if (myPlayerInfo.bullet != null) {
            if (myPlayerInfo.bullet.activeInHierarchy) { 
                clientFrameMsg.bulletPosi = new msg.Vector2Msg(myPlayerInfo.bullet.transform.position.x, myPlayerInfo.bullet.transform.position.y);
            }
            clientFrameMsg.bulletActive = myPlayerInfo.bullet.activeInHierarchy;
        }

        InstanceManager.instance.netClient.Send(clientFrameMsg);
    }


    public void UpdatePlayerSize(PlayerInfo playerInfo) {

        int scaleSize = Math.Min(Math.Max((playerInfo.hp -100 ) / 5 , 0) + 10 , 40 ) ;


        playerInfo.gameObject.transform.localScale
            = new Vector3(scaleSize, scaleSize, playerInfo.gameObject.transform.localScale.z);

        string parentName = playerInfo.gameObject.name;

        GameObject go = GameObject.Find(parentName + "/HpText");
        if (go == null) {
            Debug.Log(" UpdatePlayerSize fail  " + parentName + "/HpText" + " not found ===============");
            return;
        }

        if (go.GetComponent<TextMesh>() == null)
        {
            Debug.Log(" UpdatePlayerSize fail  " + parentName + "/HpText TextMesh" + " not found ===============");
            return;
        }

        go.GetComponent<TextMesh>().text = playerInfo.hp + "";

        //Debug.Log(" player  " + playerInfo.name + " be hit HP : " + playerInfo.hp + " =============== obj name : " + playerInfo.gameObject.name);



    }



}

