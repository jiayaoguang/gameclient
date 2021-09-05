using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace msg { 


    public class EnemyInfoMsg
    {
        public long uid; 
        public string name;
        public Vector2DMsg position;



    }

    public class Vector2DMsg {

        public float x;
        public float z;
    
    }

    public class CreateEnemyMsg{
    

    }

}
