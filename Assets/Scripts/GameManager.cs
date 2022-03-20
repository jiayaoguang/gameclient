/*using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;*/
using msg;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace yg
{
    public class GameManager : MonoBehaviour
    {




        /*    private Task<IChannel> channelTask;
        */    // Start is called before the first frame update


        long lastSnedTime;

        void Start()
        {

            Application.targetFrameRate = 300;
            /*Bootstrap bootstrap = new Bootstrap();
            IEventLoopGroup eventLoopGroup = new MultithreadEventLoopGroup(1);
            bootstrap.Group(eventLoopGroup);
            bootstrap.Channel<TcpServerSocketChannel>();// 设置通道模式为TcpSocket
            bootstrap.Option(ChannelOption.SoBacklog, 1024);

            bootstrap.Handler(new LoggingHandler(LogLevel.WARN));

            bootstrap.Handler(new MyChannelHandler<System.Object>());

            bootstrap.Option(ChannelOption.SoKeepalive, true);//是否启用心跳保活机制

            channelTask = bootstrap.ConnectAsync("127.0.0.1",8080);*/


            //TcpClient.Connect();

            //Console.WriteLine("1111111111111111");
            InstanceManager.instance.netClient.PutProto(108, typeof(msg.LoginRequestMsg));
            InstanceManager.instance.netClient.PutProto(109, typeof(msg.LoginReplyMsg));
            InstanceManager.instance.netClient.PutProto(120, typeof(msg.SCPlayerJoinMsg));

            InstanceManager.instance.netClient.PutProto(121, typeof(msg.ClientFrameMsg));
            InstanceManager.instance.netClient.PutProto(122, typeof(msg.ServerFrameMsg));

            InstanceManager.instance.netClient.PutProto(123, typeof(msg.CSEnterRoomMsg));
            InstanceManager.instance.netClient.PutProto(124, typeof(msg.SCEnterRoomMsg));

            InstanceManager.instance.netClient.PutProto(125, typeof(msg.CSHitMsg));
            InstanceManager.instance.netClient.PutProto(126, typeof(SCHitMsg));

            InstanceManager.instance.netClient.PutProto(127, typeof(SCRoomEndMsg));


            InstanceManager.instance.Init();


            DontDestroyOnLoad(gameObject);

            InstanceManager.instance.netClient.putProcessor(109, new LoginReplyProcessor());
            InstanceManager.instance.netClient.putProcessor(120 , new SCPlayerJoinProcessor());
            
            InstanceManager.instance.netClient.putProcessor(122, new ServerFrameProcessor());

            InstanceManager.instance.netClient.putProcessor(124, new SCEnterRoomProcessor());

            InstanceManager.instance.netClient.putProcessor(126, new SCHitProcessor());
            InstanceManager.instance.netClient.putProcessor(127, new SCRoomEndProcessor());

            InstanceManager.instance.netClient.start();



        }

        // Update is called once per frame
        void Update()
        {
            updateMsg();

            InstanceManager.instance.Update();

            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);

            long time = Convert.ToInt64(ts.TotalSeconds);



            if (lastSnedTime + 3L > time)
            {
                return;
            }
            lastSnedTime = time;
           // TcpClient.Send(108, "{ \"type\":111}");

            // Debug.Log("send===========");


        }

        public void updateMsg() {
            EventData eventData = null;
            InstanceManager.instance.netClient.globalQueue.TryDequeue(out eventData);
            if (eventData != null)
            {

                Processor processor;
                InstanceManager.instance.netClient.processorDict.TryGetValue(eventData.msgId, out processor);

                if (processor != null)
                {

                    processor.process(eventData.msg);
                }

            }
        }


        public static void Main(string[] s)
        {



            Console.WriteLine("1111111111111111");

        }

    }
}
