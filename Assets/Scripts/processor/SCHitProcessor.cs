using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using msg;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SCHitProcessor : JsonProcessor<SCHitMsg>
{

    


    public SCHitProcessor() {
       
    }

    

    public override void process(SCHitMsg hitMsg)
    {
        if (!SceneUtil.InBattleScene()) {
            return;
        }

        if (hitMsg.attackPlayerId == InstanceManager.instance.playerManager.myPlayerInfo.id) {

            GameObject scoreTextGo = GameObject.Find("ScoreText");

            Text scoreText = scoreTextGo.GetComponent<Text>();

            scoreText.text = "score : " + hitMsg.score;


        }

        PlayerInfo attackPlayer = InstanceManager.instance.playerManager.GetPlayerInfo(hitMsg.attackPlayerId);
        if (attackPlayer != null) {
            attackPlayer.hp = hitMsg.attackPlayerHp;
            InstanceManager.instance.playerManager.UpdatePlayerSize(attackPlayer);
        }


        PlayerInfo hitPlayer = InstanceManager.instance.playerManager.GetPlayerInfo(hitMsg.hitTargetId);
        if (hitPlayer != null)
        {
            hitPlayer.hp = hitMsg.targetHp;
            InstanceManager.instance.playerManager.UpdatePlayerSize(hitPlayer);
        }



    }
    
   
}
