using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msg
{
    public class SCEnterRoomMsg
    {

        public List<PlayerFrameMsg> playerFrameMsgs;

        public List<WallMsg> wallMsgs;

        public int score = 0;

        public List<PlayerInfoMsg> playerInfoMsgs;

        public List<MotionMsg> motionMsgs;


    }


    public class SCRoomEndMsg { 
    
    }


    public class SCPlayerJoinMsg
    {
        public PlayerInfoMsg playerInfoMsg;
    }


}
