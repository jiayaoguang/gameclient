using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Bootstrap bootstrap = new Bootstrap();
        IEventLoopGroup eventLoopGroup = new MultithreadEventLoopGroup(1);
        bootstrap.Group(eventLoopGroup);
        bootstrap.Channel<TcpServerSocketChannel>();// 设置通道模式为TcpSocket
        bootstrap.Option(ChannelOption.SoBacklog, 1024);

        bootstrap.Handler(new LoggingHandler(LogLevel.WARN));

        bootstrap.Handler(new MyChannelHandler());

        bootstrap.Option(ChannelOption.SoKeepalive, true);//是否启用心跳保活机制



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
