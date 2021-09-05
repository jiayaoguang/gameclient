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
            default:
                GameObject.Find("TipText").GetComponent<Text>().text = "oh wrong";
                return;
        }




        string battleSceneName = "BattleScene";

        SceneManager.LoadScene(battleSceneName);


        Debug.Log("login success, LoadScene ......  ");


    }

  
}
