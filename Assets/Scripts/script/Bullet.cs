using msg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    Text scoreText;

    void Start()
    {
        GameObject scoreTextGo = GameObject.Find("ScoreText");

        scoreText = scoreTextGo.GetComponent<Text>();
    }



    void OnTriggerEnter(Collider collider)
    {
        //进入触发器执行的代码

        Debug.Log("OnTriggerEnter ：" + collider.gameObject.name);
        if (collider.gameObject.name.StartsWith("EnemyBullet"))
        {
           
        }else if (collider.gameObject.name.StartsWith("Enemy_")) {
            hitEnemy(collider.gameObject);
        }

        

    }


    public void hitEnemy(GameObject enemy) {

        /* int score = InstanceManager.instance.playerManager.myPlayerInfo.score + 1;
         InstanceManager.instance.playerManager.myPlayerInfo.score = score;

         scoreText.text = "Score : " + score;*/

        string hitTraget = enemy.name;

        CSHitMsg hitMsg = new CSHitMsg();
        PlayerInfo enemyInfo = InstanceManager.instance.playerManager.GetPlayerInfo(enemy.name.Replace("Enemy_", ""));
        if (enemyInfo != null) {
            hitMsg.hitTargetId = enemyInfo.id;
            hitTraget = enemyInfo.name;
        }

        Debug.Log("hit target : " + hitTraget);

        InstanceManager.instance.netClient.Send(hitMsg);

    }

    void OnCollisionEnter(Collision collision)
    {
        //进入碰撞器执行的代码
        Debug.Log("OnCollisionEnter");
    }

}

