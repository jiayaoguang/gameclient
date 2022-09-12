using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PrefabManager
{

    public readonly GameObject enemyPrefab;
    public readonly GameObject bulletPrefab;

    public readonly GameObject enemyBulletPrefab;

    public readonly GameObject borderPrefab;

    public readonly GameObject myMotionPrefab;
    public readonly GameObject enemyMotionPrefab;
    public PrefabManager()
    {

        enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy");
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        enemyBulletPrefab = Resources.Load<GameObject>("Prefabs/EnemyBullet");
        borderPrefab = Resources.Load<GameObject>("Prefabs/BorderCube");

        myMotionPrefab = Resources.Load<GameObject>("Prefabs/MyMotion");
        enemyMotionPrefab = Resources.Load<GameObject>("Prefabs/EnemyMotion");

        // BorderCube

    }
}

