using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitBattle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject loginBtnGameObject = GameObject.Find("ExitBattleButton");

        loginBtnGameObject.GetComponent<Button>().onClick.AddListener(exitBattle);
    }

    public void exitBattle() {
        SceneUtil.LoadLobbyScene();
        InstanceManager.instance.playerManager.uid2motionMap.Clear();
        InstanceManager.instance.playerManager.RemoveAllPlayerInfo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
