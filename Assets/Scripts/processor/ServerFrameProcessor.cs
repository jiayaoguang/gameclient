using msg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ServerFrameProcessor : Processor
{
    


    public ServerFrameProcessor() {

       


    }

    

    public void process(byte[] msg)
    {
        if (InstanceManager.instance.playerManager.myPlayerInfo == null)
        {
            return;
        }

        if (!InstanceManager.instance.playerManager.myPlayerInfo.isEnterBattleScene)
        {
            return;
        }


        if (!SceneUtil.InBattleScene()) {
            return;
        }


        string str = System.Text.Encoding.Default.GetString(msg);

        ServerFrameMsg serverFrame = InstanceManager.instance.jsonManager.Deserialize<ServerFrameMsg>(str);


       // Debug.Log(" receieve serverFrame ...... " + serverFrame.playerFrameMsgs.Count);


        

        foreach (PlayerFrameMsg playerFrameMsg in serverFrame.playerFrameMsgs) {

            if (playerFrameMsg.playerId == InstanceManager.instance.playerManager.myPlayerInfo.id) {
                continue;
            }

            PlayerInfo playerInfo = InstanceManager.instance.playerManager.GetPlayerInfo(playerFrameMsg.playerId);
            if (playerInfo == null)
            {
                GameObject gameObject = InstanceManager.instance.prefabManager.CreateEnemyGo();


                gameObject.SetActive(true);

                playerInfo = new PlayerInfo();
                playerInfo.id = playerFrameMsg.playerId;
                playerInfo.gameObject = gameObject;

                gameObject.name = "Enemy_"+playerFrameMsg.name;


                InstanceManager.instance.playerManager.PutPlayerInfo(playerInfo);
                

                Debug.Log(" create enemy ");
            }



            if (playerInfo.bullet == null) {
                playerInfo.bullet = GameObject.Instantiate(InstanceManager.instance.prefabManager.enemyBulletPrefab);
            }

            
            if (playerFrameMsg.bulletPosi != null) { 
                playerInfo.bullet.transform.position = new Vector3(playerFrameMsg.bulletPosi.x ,playerFrameMsg.bulletPosi.y,0);
            }

            if (playerFrameMsg.bulletActive)
            {
                playerInfo.bullet.SetActive(true);
            }
            else {
                playerInfo.bullet.SetActive(false);
            }


            playerInfo.gameObject.transform.localEulerAngles = new Vector3(playerInfo.gameObject.transform.localEulerAngles.x , playerInfo.gameObject.transform.localEulerAngles.y , playerFrameMsg.dir);
            

            InstanceManager.instance.playerManager.UpdatePlayerState(playerInfo ,playerFrameMsg.state);
            InstanceManager.instance.playerManager.UpdatePlayerPosi(playerInfo , playerFrameMsg.posi.x, playerFrameMsg.posi.y);


            
        } 

        


    }

  
}
