
using System;

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using UnityEngine;





public class TcpClient : NetClient
{

    public Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


    private void SendBytes(byte[] sendBytes)
    {
        clientSocket.Send(sendBytes, sendBytes.Length, SocketFlags.None);
    }


    public override void Send(int id, string content)
    {
        byte[] byteArray = System.Text.Encoding.Default.GetBytes(content);

        byte[] sendBytes = new byte[8 + byteArray.Length];

        int len = 4 + byteArray.Length;


        sendBytes[0] = (byte)(len >> 24);
        sendBytes[1] = (byte)(len >> 16);
        sendBytes[2] = (byte)(len >> 8);
        sendBytes[3] = (byte)(len);

        sendBytes[4] = (byte)(id >> 24);
        sendBytes[5] = (byte)(id >> 16);
        sendBytes[6] = (byte)(id >> 8);
        sendBytes[7] = (byte)(id);



        for (int i = 0; i < byteArray.Length; i++)
        {

            sendBytes[i + 8] = byteArray[i];

        }

        SendBytes(sendBytes);
    }



    public override void Connect(String addr, int port)
    {
        try
        {





            IPAddress ip = IPAddress.Parse(addr);

            IPEndPoint point = new IPEndPoint(ip, 8088);
            clientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.NoDelay, true);
            clientSocket.Connect(point);


        }
        catch (Exception e)
        {
            string errorMsg = "´错误信息:\t\t" + e.Message + "\t\t" + e.GetType() + "\t\t" + e.StackTrace;
            Debug.LogError(errorMsg);
        }


    }




    public int Receive(byte[] buffer)
    {
        return clientSocket.Receive(buffer);

    }


    public override void ReceiveMsg()
    {


        while (true)
        {
            try
            {

                byte[] buffer = new byte[1024 * 1024];
                int readLen = Receive(buffer);





                byte[] msg = new byte[readLen - 8];

                int msgId = (buffer[4] & 0xff) >> 24;
                msgId += (buffer[5] & 0xff) >> 16;
                msgId += (buffer[6] & 0xff) << 8;
                msgId += buffer[7];

                for (int i = 8; i < readLen; i++)
                {
                    msg[i - 8] = buffer[i];
                }
                Debug.Log("msgId : " + msgId + " ====receive msg : =====>>=");
                //publicEvent(msgId, msg);

                handleReciveBytes(buffer,readLen);

                //string s = Encoding.UTF8.GetString(buffer, 8, readLen);



                 

            }
            catch (Exception ex)

            {

                Console.Error.WriteLine(ex.Message);
                break;

            }
        }
    }

}










