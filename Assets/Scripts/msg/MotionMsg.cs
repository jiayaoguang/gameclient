using msg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msg
{
    public class MotionMsg
    {


        public long ownPlayerId;

        public int motionType;

        public Vector2Msg posi;

        public long uid;


        public int maxHp;

        public int hp;

        public Vector2Msg scale;


        public Vector2Msg bulletPosi;



    }


    public class CSCreateMotionMsg {

        public int type;

        public Vector2Msg posi;

    }


    public class SCCreateMotionMsg
    {

        public MotionMsg motionMsg;

        public int costScore;

        public int currentScore; 
    }

}



