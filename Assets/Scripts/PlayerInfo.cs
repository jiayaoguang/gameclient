using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class PlayerInfo
{

    public bool isEnterBattleScene;

    public long id;
    public string name;

    public GameObject gameObject;

    public int score = 0;


    public GameObject bullet;

    public long bulletEndTime = 0L;

    public Vector3 bulletDir = Vector3.up;


    public int hp = 0;

    public int state = 0;


}

