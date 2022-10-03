using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using msg;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SCCreateMotionProcessor : JsonProcessor<SCCreateMotionMsg>
{

    


    public SCCreateMotionProcessor() {
       
    }

    

    public override void process(SCCreateMotionMsg createMotionMsg)
    {
        if (!SceneUtil.InBattleScene()) {
            return;
        }
        if (createMotionMsg.motionMsg == null) {
            return;
        }

        InstanceManager.instance.playerManager.createMotion(createMotionMsg.motionMsg);

    
    }
    
   
}
