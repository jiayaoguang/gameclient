using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msg
{
    public class CSHitMsg
    {

        public long hitTargetId = 0L;



    }


    public class SCHitMsg
    {

        public int score = 0;

        public int addScore = 0;

        public long hitTargetId = 0L;

        public int targetHp = 0;

        public long attackPlayerId = 0L;

        public int attackPlayerHp = 0;

        public int targetState = 0;

    }


}
