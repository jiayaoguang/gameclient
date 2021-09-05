using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace msg{
    public class LoginRequestMsg
    {

        public string name;
        public string password;



    }


    public class LoginReplyMsg {

        public long id;
        public string name;
        public string token;
        // 0 : 登陆成功 ， 其他表示失败
        public int errorCode;

    }



}
