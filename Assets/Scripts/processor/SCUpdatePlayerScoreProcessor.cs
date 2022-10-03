using msg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SCUpdatePlayerScoreProcessor : JsonProcessor<SCUpdatePlayerScore>
{
    

    public override void process(SCUpdatePlayerScore playerScoreMsg)
    {
        if (!SceneUtil.InBattleScene()) {
            return;
        }

        
        PlayerInfo playerInfo = InstanceManager.instance.playerManager.GetPlayerInfo(playerScoreMsg.playerId);
        if (playerInfo == null)
        {
            return;
        }
        playerInfo.score = playerScoreMsg.score;
        GameObject gameObject = playerInfo.gameObject;

        float scale = playerScoreMsg.playerSize;

        gameObject.transform.localScale = new Vector3(scale, scale , scale);

        

    }
}
