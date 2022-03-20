
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
        //Connect("127.0.0.1", 8088);
        Connect("47.93.13.212", 8088);
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





