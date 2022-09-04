
using System;

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using UnityEngine;



public class EventData
{



    public int msgId
    {
        get; set;
    }

    public byte[] msg
    {
        get; set;
    }


}

public abstract class NetClient
{

   




    public ConcurrentDictionary<int, Processor> processorDict = new ConcurrentDictionary<int, Processor>();


    public ConcurrentQueue<EventData> globalQueue = new ConcurrentQueue<EventData>();


    public ConcurrentDictionary<int, Type> protoClassDict = new ConcurrentDictionary<int, Type>();
    public ConcurrentDictionary<Type, int> protoClass2IdDict = new ConcurrentDictionary<Type, int>();


    private byte[] uncompleteMsg = new byte[0];


    public void start() {
        try {
            Connect();
            StartReceive();
        }
        catch (Exception e)
        {
            string errorMsg = "´错误信息:\t\t" + e.Message + "\t\t" + e.GetType() + "\t\t" + e.StackTrace;
            Debug.LogError(errorMsg);
        }

    }

    public void Connect()
    {
        Connect("127.0.0.1", 8088);
        //Connect("47.93.13.212", 8088);
    }

    public abstract void Connect(String addr, int port);

    


    public void StartReceive() {
        Thread th = new Thread(ReceiveMsg);

        th.IsBackground = true;

        th.Start();


        Debug.Log("============ StartReceive");

    }


    public void Send(object data)
    {
        int msgId = GetMsgId(data.GetType());

        if (msgId <= 0)
        {
            Debug.LogError("msgId not found " + data.GetType());
            return;
        }
        Send(msgId, data);

    }

    public void Send(int id, object data)
    {
        Send(id, InstanceManager.instance.jsonManager.Serialize(data));
    }

    public abstract void Send(int id, string content);



    public bool putProcessor(int id, Processor processor)
    {

        return processorDict.TryAdd(id, processor);

    }


    public abstract void ReceiveMsg();



    public void handleReciveBytes(byte[] buffer , int readLen) {

        Debug.Log("Recive buff len " + readLen);

        byte[] newBytes  = new byte[uncompleteMsg.Length + readLen];

        for (int i = 0;i < newBytes.Length; i++) {

            if (i <= uncompleteMsg.Length - 1) {
                newBytes[i] = uncompleteMsg[i];
            } else {
                newBytes[i] = buffer[ i - (uncompleteMsg.Length ) ];
            }

        }
        
        uncompleteMsg = newBytes;


        int uncompleteMsgIndex = 0;





        for (;uncompleteMsgIndex < uncompleteMsg.Length;) {

            



            int msgLength = (uncompleteMsg[uncompleteMsgIndex ] & 0xff) >> 24;
            msgLength += (uncompleteMsg[uncompleteMsgIndex + 1] & 0xff) >> 16;
            msgLength += (uncompleteMsg[uncompleteMsgIndex + 2] & 0xff) << 8;
            msgLength += uncompleteMsg[uncompleteMsgIndex + 3];


            


            int msgId = (uncompleteMsg[uncompleteMsgIndex + 4] & 0xff) >> 24;
            msgId += (uncompleteMsg[uncompleteMsgIndex + 5] & 0xff) >> 16;
            msgId += (uncompleteMsg[uncompleteMsgIndex + 6] & 0xff) << 8;
            msgId += uncompleteMsg[uncompleteMsgIndex + 7];

            if ( uncompleteMsg.Length < (uncompleteMsgIndex + 4 + msgLength) ) {

                byte[] newUncompleteMsg = new byte[uncompleteMsg.Length - uncompleteMsgIndex];

                for (int i = 0; i < newUncompleteMsg.Length; i++)
                {

                    newUncompleteMsg[i] = uncompleteMsg[i - uncompleteMsgIndex];

                }
                uncompleteMsg = newUncompleteMsg;
                Debug.Log("uncompleteMsg.Length < (uncompleteMsgIndex + 8 + msgLenth) msgLength : " + msgLength + ", msgId :" + msgId);
                break;
            }



            byte[] completeMsg = new byte[msgLength - 4];
            for (int i = 0; i < completeMsg.Length; i++)
            {
                completeMsg[i] = uncompleteMsg[uncompleteMsgIndex + 8 + i];

            }

  

            uncompleteMsgIndex = uncompleteMsgIndex + msgLength + 4;

            
            publicEvent( msgId , completeMsg);

            if (uncompleteMsgIndex >= uncompleteMsg.Length - 1)
            {
                uncompleteMsg = new byte[0];
                //Debug.Log("break uncompleteMsgIndex >= uncompleteMsg.Length - 1 : " + msgId + " , len " + uncompleteMsg.Length);
                break;
            }

        }

        


    }


    public void publicEvent(int msgId, byte[] msg)
    {

        Processor processor;
        processorDict.TryGetValue(msgId, out processor);

        if (processor != null)
        {

            EventData eventData = new EventData();
            eventData.msgId = msgId;
            eventData.msg = msg;

            globalQueue.Enqueue(eventData);

        }
        else
        {

            Debug.Log("processor unkonwn , msgId :" + msgId);
        }

    }



    public void PutProto(int msgId, Type protoClazz)
    {
        protoClassDict.TryAdd(msgId, protoClazz);
        protoClass2IdDict.TryAdd(protoClazz, msgId);
    }


    public int GetMsgId(Type type)
    {

        int msgId = 0;
        protoClass2IdDict.TryGetValue(type, out msgId);
        return msgId;

    }

    public Type GetProto(int msgId)
    {
        Type protoClazz;

        protoClassDict.TryGetValue(msgId, out protoClazz);
        return protoClazz;
    }








}





