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
        Debug.Log("login success, LoadScene ......  " + JsonUtility.ToJson(loginReply));

        List<WallMsg> wallMsg = parseFromStr(str);

        if (wallMsg != null && wallMsg.Count > 0) {
            Debug.Log("createWalls ::: " + wallMsg.Count);
            createWalls(wallMsg);
        }



    }
    public List<WallMsg> parseFromStr(string str) {
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
    }



    public void createWalls(List<WallMsg> wallMsgs) {

        foreach ( WallMsg wallMsg in wallMsgs) {
            Debug.Log(" before createWalls ......  ");
            GameObject gameObject = GameObject.Instantiate(wallPrefab);
            gameObject.GetComponent<Transform>().position = new Vector3(wallMsg.x, wallMsg.y, 0);
            gameObject.GetComponent<Transform>().localScale = new Vector3(wallMsg.width, wallMsg.height, 0);
            gameObject.SetActive(true);
            GameObject.DontDestroyOnLoad(gameObject);
            Debug.Log(" createWalls ......  ");
        }

        
    }


    public class WallsStr {

       public string str;
    }

  
}
