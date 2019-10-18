/****************************************************
    文件：NetService.cs
	作者：Echo
    邮箱: 350383921@qq.com
    日期：2019/10/17 17:6:3
	功能：网络服务
*****************************************************/

using PENet;
using PEProtocol;
using System.Collections.Generic;
using UnityEngine;

public class NetService : MonoBehaviour 
{
    public static NetService instance = null;

    private static string obj = "lock";
    PESocket<ClientSession, GameMsg> client = null;
    private Queue<GameMsg> msgQue = new Queue<GameMsg>();

    public void InitSvc()
    {
        instance = this;
        PECommon.Log("Init NetServece");
        client = new PESocket<ClientSession, GameMsg>();
        client.StartAsClient(ServiceConfig.srvIP, ServiceConfig.srvPort);
        //根据回调的不同等级
        client.SetLog(true, (string msg, int lv) =>
        {
            switch (lv)
            {
                case 0:
                    msg = "Log" + msg;
                    Debug.Log(msg);
                    break;
                case 1:
                    msg = "LogWarning" + msg;
                    Debug.LogWarning(msg);
                    break;
                case 2:
                    msg = "LogError" + msg;
                    Debug.LogError(msg);
                    break;
                case 3:
                    msg = "LogInfo" + msg;
                    Debug.Log(msg);
                    break;
                default:
                    break;
            }
        });
    }
    public void SendMsg(GameMsg msg)
    {
        if (client.session!=null)
        {
            client.session.SendMsg(msg);
        }
        else
        {
            GameRoot.instance.AddTips("服务器未连接");
            //重新初始化，尝试再连接
            InitSvc();
        }
    }
    public void AddNetPack(GameMsg msg)
    {
        lock (obj)
        {
            msgQue.Enqueue(msg);
        }
    }
    private void Update()
    {
        if (msgQue.Count>0)
        {
            lock (obj)
            {
                GameMsg msg = msgQue.Dequeue();
                ProcessMsg(msg);
            }
        }
    }
    private void ProcessMsg(GameMsg msg)
    {
        if (msg.err!=(int)ErroroCode.None)
        {
            switch ((ErroroCode)msg.err)
            {
                case ErroroCode.AcctIsOnline:
                    GameRoot.instance.AddTips("当前账号已在线");
                    break;
                case ErroroCode.WrongPass:
                    GameRoot.instance.AddTips("密码错误");
                    break;
                default:
                    break;
            }
        }
        switch ((CMD)msg.cmd)
        {
            case CMD.RequestLogin:
                break;
            case CMD.ResponLogin://去登陆系统处理服务器回应的消息
                LoginSys.instance.RspLogin(msg);
                break;
            default:
                break;
        }
    }
}