using msg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Play : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(EnterBattleScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SendPlayRequest() {

        InstanceManager.instance.netClient.Send(new CSEnterRoomMsg());

    }


    public void EnterBattleScene() {


        string battleSceneName = "BattleScene";

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(battleSceneName);
        Debug.Log("login success, LoadSceneAsyn ......  ");

        

        InstanceManager.instance.updateManager.AddUpdate(new OnLoadBattleScene(asyncOperation));
    }

}


class OnLoadBattleScene : UpdateAble
{
    AsyncOperation asyncOperation;

    public OnLoadBattleScene(AsyncOperation asyncOperation) { 
        this.asyncOperation = asyncOperation;
    }

    public override void Update()
    {
        if (asyncOperation.isDone)
        {
            Stop();
            InstanceManager.instance.netClient.Send(new CSEnterRoomMsg());
        }
    }
}
