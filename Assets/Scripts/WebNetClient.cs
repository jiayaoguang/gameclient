
using System;

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using UnityEngine;





public class WebNetClient : NetClient
{

    private ClientWebSocket clientWebSocket = new ClientWebSocket();
    private string webSocketUrl = "ws://{ip}:{port}";



    public override void Send(int id, string content)
    {
        byte[] byteArray = System.Text.Encoding.Default.GetBytes(content);

        byte[] sendBytes = new byte[4 + byteArray.Length];



        sendBytes[0] = (byte)(id >> 24);
        sendBytes[1] = (byte)(id >> 16);
        sendBytes[2] = (byte)(id >> 8);
        sendBytes[3] = (byte)(id);



        for (int i = 0; i < byteArray.Length; i++)
        {

            sendBytes[i + 4] = byteArray[i];

        }

        SendBytes(sendBytes);
    }


    private void SendBytes(byte[] sendBytes)
    {
        if (clientWebSocket.State != WebSocketState.Open)
        {
            Uri serverUri = new Uri(webSocketUrl);
            clientWebSocket.ConnectAsync(serverUri, CancellationToken.None).Wait();
            
        }
        if (clientWebSocket.State == WebSocketState.Open)
        {
            ArraySegment<byte> bytesToSend = new ArraySegment<byte>(sendBytes);
            clientWebSocket.SendAsync(bytesToSend, WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }

    public override void Connect(String addr, int port)
    {
        try
        {
            ClientWebSocket clientWebSocket = new ClientWebSocket();

            
            clientWebSocket = new ClientWebSocket();
            Uri serverUri = new Uri(webSocketUrl);
            clientWebSocket.ConnectAsync(serverUri, CancellationToken.None).Wait();


        }
        catch (Exception e)
        {
            string errorMsg = "´错误信息:\t\t" + e.Message + "\t\t" + e.GetType() + "\t\t" + e.StackTrace;
            Debug.LogError(errorMsg);
        }


    }




    public override int Receive(byte[] buffer)
    {

        ArraySegment<byte> bytesReceived = new ArraySegment<byte>(buffer);
        WebSocketReceiveResult result = clientWebSocket.ReceiveAsync(bytesReceived, CancellationToken.None).Result;
        return result.Count;

    }
}










