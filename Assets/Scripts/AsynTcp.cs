
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;



public class EventData {

       

    public int msgId 
    {
        get;set;
    }

    public byte[] msg
    {
        get; set;
    }


}

public class TcpClient
{


    public static Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);



    public static ConcurrentDictionary<int, Processor> processorDict = new ConcurrentDictionary<int, Processor>();


    public static ConcurrentQueue<EventData> globalQueue = new ConcurrentQueue<EventData>();


    public static ConcurrentDictionary<int, Type> protoClassDict = new ConcurrentDictionary<int, Type>();
    public static ConcurrentDictionary<Type, int> protoClass2IdDict = new ConcurrentDictionary<Type, int>();

    public static void Connect()
    {
        Connect("127.0.0.1", 8088);
    }

    public static void Connect(String addr, int port)
    {
        try
        {
            IPAddress ip = IPAddress.Parse(addr);

            IPEndPoint point = new IPEndPoint(ip, 8088);
            clientSocket.Connect(point);

            //clientSocket.ReceiveAsync

            Thread th = new Thread(ReceiveMsg);

            th.IsBackground = true;

            th.Start();
        }
        catch(Exception e) {
            string errorMsg = "´错误信息:\t\t" + e.Message + "\t\t" + e.GetType() + "\t\t" + e.StackTrace;
            Debug.LogError(errorMsg);
        }
            

    }

    public static void Send(object data)
    {
        int msgId = GetMsgId(data.GetType());

        if (msgId <= 0) {
            Debug.LogError("msgId not found " + data.GetType());
            return;
        }
        Send(msgId , data);

    }

        public static void Send(int id, object data) {
        Send(id,JsonUtility.ToJson(data));
    }

    public static void Send(int id, string content)
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

        clientSocket.Send(sendBytes, sendBytes.Length, SocketFlags.None);
    }


    public static bool putProcessor(int id , Processor processor) {

        return processorDict.TryAdd(id , processor);
            
    }


    static void ReceiveMsg()
    {


        while (true)
        {
            try
            {

                byte[] buffer = new byte[1024 * 1024];
                int readLen = clientSocket.Receive(buffer);

  
                   


                byte[] msg = new byte[readLen - 8];

                int msgId = (buffer[4] & 0xff) >> 24;
                msgId += (buffer[5] & 0xff) >> 16;
                msgId += (buffer[6] & 0xff) << 8;
                msgId += buffer[7];

                for (int i=8;i<readLen;i++) {
                    msg[i-8] = buffer[i];
                }

                publicEvent(msgId , msg);

                string s = Encoding.UTF8.GetString(buffer, 8, readLen);

                    

                Debug.Log("msgId : " + msgId + " ====receive msg : =====>>=" + s + " >> msgId"  );

            }
            catch (Exception ex)

            {

                Console.Error.WriteLine(ex.Message);
                break;

            }
        }
    }


    public static void publicEvent(int msgId , byte[] msg)
    {

        Processor processor;
        processorDict.TryGetValue(msgId,out processor);

        if (processor != null)
        {

            EventData eventData = new EventData();
            eventData.msgId = msgId;
            eventData.msg = msg;

            globalQueue.Enqueue(eventData);

        }
        else {

            Debug.Log("processor unkonwn , msgId :" + msgId);
        }

    }



    public static void PutProto(int msgId , Type protoClazz)
    {
        protoClassDict.TryAdd(msgId , protoClazz);
        protoClass2IdDict.TryAdd(protoClazz , msgId);
    }


    public static int GetMsgId(Type type) {

        int msgId = 0;
        protoClass2IdDict.TryGetValue(type , out msgId);
        return msgId;

    }

    public static Type GetProto(int msgId)
    {
        Type protoClazz;

        protoClassDict.TryGetValue(msgId ,out protoClazz);
        return protoClazz;
    }

}


   

