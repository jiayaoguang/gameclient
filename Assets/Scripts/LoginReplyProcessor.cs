using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using msg;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginReplyProcessor : Processor
{

    
    public LoginReplyProcessor() {

    }

    

    public void process(byte[] msg)
    {
        string str = System.Text.Encoding.Default.GetString(msg);

        LoginReplyMsg loginReply = JsonUtility.FromJson<LoginReplyMsg>(str);

       

        switch (loginReply.errorCode) {

            case 0:
                break;
            case 1:
                GameObject.Find("TipText").GetComponent<Text>().text = "密码错误";
                return;
            case 2:
                GameObject.Find("TipText").GetComponent<Text>().text = "服务器查询玩家数据超时";
                return;
            default:
                GameObject.Find("TipText").GetComponent<Text>().text = "oh wrong : " + loginReply.errorCode;
                return;
        }


        PlayerInfo myPlayerInfo = new PlayerInfo();
        myPlayerInfo.name = loginReply.name;
        myPlayerInfo.id = loginReply.id;
        InstanceManager.instance.playerManager.myPlayerInfo = myPlayerInfo;

        //登录成功切场景
        string battleSceneName = "BattleScene";

        SceneManager.LoadScene(battleSceneName);

        Debug.Log("login success, LoadScene ......  ");


    }
  
}
