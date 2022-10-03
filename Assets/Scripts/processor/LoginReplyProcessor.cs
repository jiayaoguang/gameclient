using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using msg;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginReplyProcessor : Processor
{
    private GameObject wallPrefab;

    public LoginReplyProcessor() {
        wallPrefab = Resources.Load<GameObject>("Prefabs/Wall");
    }

    

    public void process(byte[] msg)
    {
        string str = System.Text.Encoding.Default.GetString(msg);

        LoginReplyMsg loginReply = InstanceManager.instance.jsonManager.Deserialize<LoginReplyMsg>(str);

       // Debug.Log(" walls num  " + loginReply.wallMsgs.Count + " one pos > " + loginReply.wallMsgs);

       

        switch (loginReply.errorCode) {

            case 0:
                break;
            case 1:
                InstanceManager.instance.playerManager.tipQueue .Enqueue( "密码错误");
                return;
            case 2:
                InstanceManager.instance.playerManager.tipQueue.Enqueue("服务器查询玩家数据超时");
               
                return;
            default:
                InstanceManager.instance.playerManager.tipQueue.Enqueue("oh wrong : " + loginReply.errorCode);
                return;
        }


        PlayerInfo myPlayerInfo = new PlayerInfo();
        myPlayerInfo.name = loginReply.name;
        myPlayerInfo.id = loginReply.id;
        
        
        InstanceManager.instance.playerManager.myPlayerInfo = myPlayerInfo;

        SceneUtil.LoadLobbyScene();


    }
    /*public List<WallMsg> parseFromStr(string str) {
        List<WallMsg> wallMsgs = new List<WallMsg>();

        string wallsStr = str.Substring(str.IndexOf('[')+1 ,   str.IndexOf(']') - str.IndexOf('[') - 1);

        Debug.Log("wallsStr ::: " + wallsStr);

        string wall = "";
        int bracesWaitNum = 0;
        foreach ( char s in wallsStr  ) {
            if ( s == '{') {
                if (bracesWaitNum == 0)
                {
                    wall = "";
                }
                bracesWaitNum++;
                
                wall += s;
            }
            else if (s == '}')
            {
                wall += s;
                bracesWaitNum--;
                
                if (bracesWaitNum == 0) { 
                    Debug.Log("parse wall ::: " + wall);
                    WallMsg wallMsg = JsonUtility.FromJson<WallMsg>(wall);
                    wallMsgs.Add(wallMsg);
                    wall = "";
                }
            }
            else {
                wall += s;
            }
        }
        
        return wallMsgs;
    }*/







  




   
}
