using msg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SCPlayerJoinProcessor : JsonProcessor<SCPlayerJoinMsg>
{

    

    


    public override void process(SCPlayerJoinMsg playerJoinMsg)
    {
        if (!SceneUtil.InBattleScene()) {
            return;
        }

        PlayerInfoMsg playerInfoMsg = playerJoinMsg.playerInfoMsg;
        PlayerInfo playerInfo = InstanceManager.instance.playerManager.GetPlayerInfo(playerInfoMsg.playerId);
        if (playerInfo == null)
        {
            GameObject gameObject = InstanceManager.instance.prefabManager.CreateEnemyGo();




            playerInfo = new PlayerInfo();
            playerInfo.id = playerInfoMsg.playerId;
            playerInfo.gameObject = gameObject;
            playerInfo.name = playerInfoMsg.name;
            playerInfo.hp = playerInfoMsg.hp;

            gameObject.SetActive(true);
            gameObject.name = "Enemy_" + playerInfo.name;
            gameObject.transform.position = new Vector3(playerInfoMsg.posi.x , playerInfoMsg.posi.y);

            InstanceManager.instance.playerManager.PutPlayerInfo(playerInfo);

            InstanceManager.instance.playerManager.UpdatePlayerSize(playerInfo);
            InstanceManager.instance.playerManager.SetPlayerName(playerInfo);

            Debug.Log(" create enemy " + gameObject.name);
        }

        //Debug.Log(" create enemy gameObject :" + playerInfo.gameObject + " posi :  " + (playerInfoMsg.posi == null));
        InstanceManager.instance.playerManager.UpdatePlayerState(playerInfo, playerInfoMsg.state);
        InstanceManager.instance.playerManager.UpdatePlayerPosi(playerInfo, playerInfoMsg.posi.x, playerInfoMsg.posi.y);

    }
}
