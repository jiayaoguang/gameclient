using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PrefabManager
{

    public readonly GameObject playerPrefab;
    public readonly GameObject bulletPrefab;

    public readonly GameObject enemyBulletPrefab;

    public readonly GameObject borderPrefab;

    public readonly GameObject myMotionPrefab;
    public readonly GameObject enemyMotionPrefab;
    public readonly GameObject sysMotionPrefab;


    public readonly GameObject enemySpanMotionPrefab;
    public readonly GameObject spanMotionPrefab;


    public readonly GameObject blueGo;
    public readonly GameObject redGo;

    public PrefabManager()
    {

        playerPrefab = Resources.Load<GameObject>("Prefabs/Player");
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        enemyBulletPrefab = Resources.Load<GameObject>("Prefabs/EnemyBullet");
        borderPrefab = Resources.Load<GameObject>("Prefabs/BorderCube");

        myMotionPrefab = Resources.Load<GameObject>("Prefabs/MyMotion");
        enemyMotionPrefab = Resources.Load<GameObject>("Prefabs/EnemyMotion");
        sysMotionPrefab = Resources.Load<GameObject>("Prefabs/SysMotion");


        enemySpanMotionPrefab = Resources.Load<GameObject>("Prefabs/EnemySpanMotion");
        spanMotionPrefab = Resources.Load<GameObject>("Prefabs/SpanMotion");
        // BorderCube



        blueGo = Resources.Load<GameObject>("Prefabs/BlueSphere");
        redGo = Resources.Load<GameObject>("Prefabs/RedSphere");

    }


    public GameObject CreateEnemyGo() {

        GameObject gameObject = UnityEngine.Object.Instantiate(InstanceManager.instance.prefabManager.playerPrefab);


        gameObject.GetComponent<Renderer>().sharedMaterial = redGo.GetComponent<Renderer>().sharedMaterial;

        return gameObject;

    }

}

