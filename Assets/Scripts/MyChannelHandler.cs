using System;
using System.Net;
using System.Threading.Tasks;
using DotNetty.Transport.Channels;

public class MyChannelHandler : IChannelHandler
{
    public Task BindAsync(IChannelHandlerContext context, EndPoint localAddress)
    {
        throw new NotImplementedException();
    }

    public void ChannelActive(IChannelHandlerContext context)
    {
        throw new NotImplementedException();
    }

    public void ChannelInactive(IChannelHandlerContext context)
    {
        throw new NotImplementedException();
    }

    public void ChannelRead(IChannelHandlerContext context, object message)
    {
        throw new NotImplementedException();
    }

    public void ChannelReadComplete(IChannelHandlerContext context)
    {
        throw new NotImplementedException();
    }

    public void ChannelRegistered(IChannelHandlerContext context)
    {
        throw new NotImplementedException();
    }

    public void ChannelUnregistered(IChannelHandlerContext context)
    {
        throw new NotImplementedException();
    }

    public void ChannelWritabilityChanged(IChannelHandlerContext context)
    {
        throw new NotImplementedException();
    }

    public Task CloseAsync(IChannelHandlerContext context)
    {
        throw new NotImplementedException();
    }

    public Task ConnectAsync(IChannelHandlerContext context, EndPoint remoteAddress, EndPoint localAddress)
    {
        throw new NotImplementedException();
    }

    public Task DeregisterAsync(IChannelHandlerContext context)
    {
        throw new NotImplementedException();
    }

    public Task DisconnectAsync(IChannelHandlerContext context)
    {
        throw new NotImplementedException();
    }

    public void ExceptionCaught(IChannelHandlerContext context, Exception exception)
    {
        throw new NotImplementedException();
    }

    public void Flush(IChannelHandlerContext context)
    {
        throw new NotImplementedException();
    }

    public void HandlerAdded(IChannelHandlerContext context)
    {
        throw new NotImplementedException();
    }

    public void HandlerRemoved(IChannelHandlerContext context)
    {
        throw new NotImplementedException();
    }

    public void Read(IChannelHandlerContext context)
    {
        throw new NotImplementedException();
    }

    public void UserEventTriggered(IChannelHandlerContext context, object evt)
    {
        throw new NotImplementedException();
    }

    public Task WriteAsync(IChannelHandlerContext context, object message)
    {
        throw new NotImplementedException();
    }
}