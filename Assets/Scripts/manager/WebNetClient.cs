
using System;

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using UnityEngine;





public class WebNetClient : NetClient
{

    private ClientWebSocket clientWebSocket = new ClientWebSocket();
    private string webSocketUrl = "ws://127.0.0.1:8089";

    private const int ReceiveChunkSize = 1024;
    private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
    private readonly CancellationToken _cancellationToken;

    public WebNetClient() {
        _cancellationToken = _cancellationTokenSource.Token;
    }


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


    public async override void ReceiveMsg()
    {

        
        for (int loopTimes = 0; ; loopTimes++)
        {
            try
            {

                if((loopTimes & 1024) == 0){
                    Thread.Sleep(1);
                }

                var buffer = new byte[ReceiveChunkSize];

                byte[] byteResult = new byte[0];

                while (clientWebSocket.State == WebSocketState.Open)
                {
                    

                    WebSocketReceiveResult result;
                    do
                    {
                        Debug.Log(" ========== ReceiveAsync start ");
                        result = await clientWebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), _cancellationToken);

                        Debug.Log(" ========== ReceiveAsync ");

                        if (result.MessageType == WebSocketMessageType.Close)
                        {
                            Disconnect();
                        }
                        else
                        {
                            byteResult = byteResult.Concat(buffer.Take(result.Count)).ToArray();
                        }

                    } while (!result.EndOfMessage);
                }





                    byte[] msg = new byte[byteResult.Length - 4];

                int msgId = (byteResult[0] & 0xff) >> 24;
                msgId += (byteResult[1] & 0xff) >> 16;
                msgId += (byteResult[2] & 0xff) << 8;
                msgId += byteResult[3];

                for (int i = 4; i < byteResult.Length; i++)
                {
                    msg[i - 4] = byteResult[i];
                }

                publicEvent(msgId, msg);

                //string s = Encoding.UTF8.GetString(buffer, 8, readLen);



                Debug.Log("msgId : " + msgId + " ====receive msg : =====>>=" + msg + " >> msgId"  );

            }
            catch (Exception ex)

            {

                Console.Error.WriteLine(ex.Message);
                break;

            }
        }
    }

    private void Disconnect()
    {
        clientWebSocket.Dispose();
    }

  
}










