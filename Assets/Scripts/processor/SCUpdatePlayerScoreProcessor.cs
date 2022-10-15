using msg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SCUpdatePlayerScoreProcessor : JsonProcessor<SCUpdatePlayerScoreMsg>
{
    

    public override void process(SCUpdatePlayerScoreMsg playerScoreMsg)
    {
        if (!SceneUtil.InBattleScene()) {
            return;
        }

        
        PlayerInfo playerInfo = InstanceManager.instance.playerManager.GetPlayerInfo(playerScoreMsg.playerId);
        if (playerInfo == null)
        {
            return;
        }
        

        InstanceManager.instance.playerManager.UpdateScore( playerInfo , playerScoreMsg.score);

        GameObject gameObject = playerInfo.gameObject;

        InstanceManager.instance.playerManager.UpdatePlayerSize(playerInfo, playerScoreMsg.playerSize);




    }
}
