using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using msg;
using UnityEngine;
using UnityEngine.UI;

class SCMotionDeadProcessor : JsonProcessor<SCMotionDeadMsg>
{
    public override void process(SCMotionDeadMsg msgObj)
    {
        GameObject motion;
        InstanceManager.instance.playerManager.uid2motionMap.TryGetValue(msgObj.motionUid , out motion);

        if (motion == null) {
            return;
        }
        //TODO
        GameObject.Destroy(motion);
        InstanceManager.instance.playerManager.uid2motionMap.Remove(msgObj.motionUid);

    }
}

