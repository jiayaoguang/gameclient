/*using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;*/
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

            Console.WriteLine("1111111111111111");

            Type type = typeof(msg.CreateEnemyMsg);
            TcpClient.putProto(120, type);

            DontDestroyOnLoad(gameObject);

            TcpClient.putProcessor(120 , new CreateEnemyProcessor());
            TcpClient.Connect();



        }

        // Update is called once per frame
        void Update()
        {
            updateMsg();

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
            TcpClient.globalQueue.TryDequeue(out eventData);
            if (eventData != null)
            {

                Processor processor;
                TcpClient.processorDict.TryGetValue(eventData.msgId, out processor);

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
