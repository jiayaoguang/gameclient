using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace msg{
    public class Vector2Msg
    {
        public float x;

        public float y;
    }


    public class WallMsg
    {
        //TODO unity 自带json 不好使 
        public Vector2Msg posi;

        public int width;
        public int height;


        public float x;
        public float y;
    }




}
