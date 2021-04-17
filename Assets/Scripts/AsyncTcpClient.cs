
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class TcpClient
{


    public static Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


   


    public static void Connect()
    {
        IPAddress ip = IPAddress.Parse("127.0.0.1");

        IPEndPoint point = new IPEndPoint(ip, 8088);
        clientSocket.Connect(point);

        //clientSocket.ReceiveAsync

        Thread th = new Thread(ReceiveMsg);
        
        th.IsBackground = true;
        
        th.Start();

    }


    public static void Send(int id , string content) {
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



        for (int i = 0; i< byteArray.Length  ;i++) {

            sendBytes[i + 8] = byteArray[i];

        }

        clientSocket.Send(sendBytes, sendBytes.Length, SocketFlags.None);
    }





    static void ReceiveMsg()
    {
  
        while (true)
        {
            try
            {
  
                byte[] buffer = new byte[1024 * 1024];
                int n = clientSocket.Receive(buffer);
                string s = Encoding.UTF8.GetString(buffer, 0, n);

                Debug.Log("===============jjj=====>>=" + s + " >>");

            }
            catch (Exception ex)
  
            {
  
            Console.Error.WriteLine(ex.Message);
            break;
  
            }
         } 
    }

}