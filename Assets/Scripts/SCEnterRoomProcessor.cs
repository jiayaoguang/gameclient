using msg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SCEnterRoomProcessor : JsonProcessor<SCEnterRoomMsg>
{


    private GameObject wallPrefab;

    public SCEnterRoomProcessor()
    {
        wallPrefab = Resources.Load<GameObject>("Prefabs/Wall");
    }



    public override void process(SCEnterRoomMsg enterRoomMsg)
    {
        

        Debug.Log(" walls num  " + enterRoomMsg.wallMsgs.Count + " one pos > " + enterRoomMsg.wallMsgs);



        /*switch (loginReply.errorCode)
        {

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
        }*/


       /* PlayerInfo myPlayerInfo = new PlayerInfo();
        myPlayerInfo.name = loginReply.name;
        myPlayerInfo.id = loginReply.id;


        InstanceManager.instance.playerManager.myPlayerInfo = myPlayerInfo;*/

        

        if (!SceneUtil.InBattleScene())
        {
            return;
        }

        InstanceManager.instance.playerManager.myPlayerInfo.score = enterRoomMsg.score;

        Debug.Log("asyncOperation.isDone start ,,, GetActiveScene().name : " + SceneManager.GetActiveScene().name);

        List<WallMsg> wallMsgs = enterRoomMsg.wallMsgs;

        if (wallMsgs != null && wallMsgs.Count > 0)
        {
            //Debug.Log("createWalls ::: " + wallMsg.Count);
            createWalls(wallMsgs);
        }


        InstanceManager.instance.playerManager.myPlayerInfo.gameObject = GameObject.Find("Player");

        //InstanceManager.instance.playerManager.myPlayerInfo.gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/BlueMaterial");

        InstanceManager.instance.playerManager.myPlayerInfo.isEnterBattleScene = true;


        Debug.Log("asyncOperation.isDone GetActiveScene().name : " + SceneManager.GetActiveScene().name);


        GameObject scoreTextGo = GameObject.Find("ScoreText");

        scoreTextGo.GetComponent<Text>().text = "score : " + InstanceManager.instance.playerManager.myPlayerInfo.score;


        foreach (PlayerInfoMsg playerInfoMsg in enterRoomMsg.playerInfoMsgs)
        {

            if (playerInfoMsg.playerId == InstanceManager.instance.playerManager.myPlayerInfo.id)
            {
                
                InstanceManager.instance.playerManager.myPlayerInfo.hp = playerInfoMsg.hp;
                InstanceManager.instance.playerManager.myPlayerInfo.gameObject.name = "Player";
                InstanceManager.instance.playerManager.PutPlayerInfo(InstanceManager.instance.playerManager.myPlayerInfo);
                InstanceManager.instance.playerManager.UpdatePlayerSize(InstanceManager.instance.playerManager.myPlayerInfo);
                continue;
            }

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
                gameObject.name = "Enemy_"+playerInfo.name;

                InstanceManager.instance.playerManager.PutPlayerInfo(playerInfo);

                InstanceManager.instance.playerManager.UpdatePlayerSize(playerInfo);

                Debug.Log(" create enemy "  + gameObject.name);
            }

            playerInfo.gameObject.GetComponent<Transform>().position = new Vector3(playerInfoMsg.posi.x, playerInfoMsg.posi.y, 0);
        }



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



    public void createWalls(List<WallMsg> wallMsgs)
    {

        foreach (WallMsg wallMsg in wallMsgs)
        {
            //Debug.Log(" before createWalls ......  " + wallMsg.posi.x);
            GameObject gameObject = GameObject.Instantiate(wallPrefab);
            gameObject.GetComponent<Transform>().position = new Vector3(wallMsg.posi.x, wallMsg.posi.y, 2);
            gameObject.GetComponent<Transform>().localScale = new Vector3(wallMsg.width, wallMsg.height, 0);
            gameObject.SetActive(true);
            //GameObject.DontDestroyOnLoad(gameObject);

        }


    }


    public class WallsStr
    {

        public string str;
    }


}

public class OnLoadLoginScene : UpdateAble
{
    private AsyncOperation asyncOperation;
    private List<WallMsg> wallMsgs;
    private SCEnterRoomProcessor enterRoomProcessor;

    public OnLoadLoginScene(AsyncOperation asyncOperation, List<WallMsg> wallMsgs, SCEnterRoomProcessor enterRoomProcessor)
    {
        this.asyncOperation = asyncOperation;
        this.wallMsgs = wallMsgs;
        this.enterRoomProcessor = enterRoomProcessor;
    }

    public override void Update()
    {
        //Debug.Log("asyncOperation.isDone : " + asyncOperation.isDone);
        if (asyncOperation.isDone)
        {

            Debug.Log("asyncOperation.isDone start ,,, GetActiveScene().name : " + SceneManager.GetActiveScene().name);
            if (wallMsgs != null && wallMsgs.Count > 0)
            {
                //Debug.Log("createWalls ::: " + wallMsg.Count);
                enterRoomProcessor.createWalls(wallMsgs);
            }


            InstanceManager.instance.playerManager.myPlayerInfo.gameObject = GameObject.Find("Player");

            //InstanceManager.instance.playerManager.myPlayerInfo.gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/BlueMaterial");

            InstanceManager.instance.playerManager.myPlayerInfo.isEnterBattleScene = true;


            Debug.Log("asyncOperation.isDone GetActiveScene().name : " + SceneManager.GetActiveScene().name);


            GameObject scoreTextGo = GameObject.Find("ScoreText");

            scoreTextGo.GetComponent<Text>().text = "score : " + InstanceManager.instance.playerManager.myPlayerInfo.score;

            Stop();
        }
        else
        {

        }
    }




}

