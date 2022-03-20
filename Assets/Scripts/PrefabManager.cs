using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PrefabManager
{

    public GameObject enemyPrefab;
    public GameObject bulletPrefab;

    public GameObject enemyBulletPrefab;
    public PrefabManager()
    {

        enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy");
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        enemyBulletPrefab = Resources.Load<GameObject>("Prefabs/EnemyBullet");

    }
}

