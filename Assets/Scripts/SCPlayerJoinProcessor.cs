using msg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SCPlayerJoinProcessor : JsonProcessor<SCPlayerJoinMsg>
{

    private GameObject enemyPrefab;
    public SCPlayerJoinProcessor() {

        enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy");
    }

    


    public override void process(SCPlayerJoinMsg playerJoinMsg)
    {
        if (!SceneUtil.InBattleScene()) {
            return;
        }

        PlayerInfoMsg playerInfoMsg = playerJoinMsg.playerInfoMsg;
        PlayerInfo playerInfo = InstanceManager.instance.playerManager.GetPlayerInfo(playerInfoMsg.playerId);
        if (playerInfo == null)
        {
            GameObject gameObject = GameObject.Instantiate(InstanceManager.instance.prefabManager.enemyPrefab);




            playerInfo = new PlayerInfo();
            playerInfo.id = playerInfoMsg.playerId;
            playerInfo.gameObject = gameObject;
            playerInfo.name = playerInfoMsg.name;
            playerInfo.hp = playerInfoMsg.hp;

            gameObject.SetActive(true);
            gameObject.name = "Enemy_" + playerInfo.name;

            InstanceManager.instance.playerManager.PutPlayerInfo(playerInfo);

            InstanceManager.instance.playerManager.UpdatePlayerSize(playerInfo);

            Debug.Log(" create enemy " + gameObject.name);
        }

        //Debug.Log(" create enemy gameObject :" + playerInfo.gameObject + " posi :  " + (playerInfoMsg.posi == null));

        playerInfo.gameObject.GetComponent<Transform>().position = new Vector3(playerInfoMsg.posi.x, playerInfoMsg.posi.y, 0);
    }
}
