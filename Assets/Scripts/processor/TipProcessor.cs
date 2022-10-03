using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using msg;
using UnityEngine;
using UnityEngine.UI;

class TipProcessor : JsonProcessor<SCTipMsg>
{
    public override void process(SCTipMsg msgObj)
    {
        InstanceManager.instance.playerManager.tipQueue.Enqueue(msgObj.content);


        //Debug.Log(" get tip : " + msgObj.content + " tip count " + InstanceManager.instance.playerManager.tipQueue.Count);
    }
}

