using System;


namespace msg
{
    public class PlayerInfoMsg
    {

        public long playerId;
        public string name;
        public int hp;

        public Vector2Msg posi;

        public float dir;

        /**
     * 0:正常 1:死亡
     */
        public int state;

    }
}
