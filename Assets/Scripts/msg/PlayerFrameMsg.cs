using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msg{ 


    public class PlayerFrameMsg
    {
        public long playerId;
        public long frameTime;
        public Vector2Msg posi;

        public Vector2Msg dir;
        public Vector2Msg bulletPosi;
        public bool bulletActive;

        public string name;
    }
}

