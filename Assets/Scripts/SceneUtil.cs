using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUtil
{

    public static void LoadLobbyScene() {

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Lobby");
        Debug.Log("login success, Load lobby ......  ");

        //InstanceManager.instance.updateManager.AddUpdate(new OnLoadLoginScene(asyncOperation, loginReply.wallMsgs, this));
    }


    public static bool InBattleScene() {

        Scene curScene = SceneManager.GetActiveScene();
        if (curScene.name.Equals("BattleScene")) {
            return true;
        }

        return false;
     }


}

