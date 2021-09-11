using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace msg{
    public class LoginRequestMsg
    {

        public string name;
        public string password;



    }

    [Serializable]
    public class LoginReplyMsg {
        [SerializeField]
        public long id;
        [SerializeField]
        public string name;
        [SerializeField]
        public string token;
        // 0 : 登陆成功 ， 其他表示失败
        [SerializeField]
        public int errorCode;

        [SerializeField]
        public List<WallMsg> wallMsgs;

    }



}
