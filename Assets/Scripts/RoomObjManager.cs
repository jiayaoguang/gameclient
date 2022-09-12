using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class RoomObjManager
{


    public void CreateBorder() {

        

        GameObject leftGo = GameObject.Instantiate(InstanceManager.instance.prefabManager.enemyPrefab);
        GameObject rightGo = GameObject.Instantiate(InstanceManager.instance.prefabManager.enemyPrefab);

        GameObject topGo = GameObject.Instantiate(InstanceManager.instance.prefabManager.enemyPrefab);
        GameObject bottomGo = GameObject.Instantiate(InstanceManager.instance.prefabManager.enemyPrefab);

        leftGo.GetComponent<Transform>().position = new Vector3(-150, 0,1);
        leftGo.GetComponent<Transform>().localScale = new Vector3(5, 1000, 10);

        rightGo.GetComponent<Transform>().position = new Vector3(150, 0, 1);
        rightGo.GetComponent<Transform>().localScale = new Vector3(5, 1000, 10);

        topGo.GetComponent<Transform>().position = new Vector3(0, 150, 1);
        topGo.GetComponent<Transform>().localScale = new Vector3(1000, 5, 10);



        bottomGo.GetComponent<Transform>().position = new Vector3(0, -150, 1);
        bottomGo.GetComponent<Transform>().localScale = new Vector3(1000, 5, 10);
    }


    public void uipdateMotionHp(GameObject motionGo , int hp) {

        string parentName = motionGo.name;

        GameObject go = GameObject.Find(parentName + "/HpText");
        if (go == null)
        {
            Debug.Log(" UpdatePlayerSize fail  " + parentName + "/HpText" + " not found ===============");
            return;
        }

        if (go.GetComponent<TextMesh>() == null)
        {
            Debug.Log(" UpdatePlayerSize fail  " + parentName + "/HpText TextMesh" + " not found ===============");
            return;
        }

        go.GetComponent<TextMesh>().text = hp + "";
    }

}

