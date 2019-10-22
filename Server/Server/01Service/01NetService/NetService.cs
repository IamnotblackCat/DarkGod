/****************************************************
	文件：NetService.cs
	作者：Echo
	邮箱: 350383921@qq.com
	日期：2019/10/17 15:29   	
	功能：网络服务
*****************************************************/

using PENet;
using PEProtocol;
using System.Collections.Generic;

public class MsgPack
{
    public ServerSession serverSession;
    public GameMsg msg;
    public MsgPack(ServerSession serverSession,GameMsg msg)
    {
        this.serverSession = serverSession;
        this.msg = msg;
    }
}
public class NetService
{
    private static NetService instance=null;
    public static NetService Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new NetService();
            }
            return instance;
        }
    }
    private static readonly string obj="lock";
    private Queue<MsgPack> msgPackQue = new Queue<MsgPack>();
    public void Init()
    {
        PESocket<ServerSession, GameMsg> server = new PESocket<ServerSession, GameMsg>();
        server.StartAsServer(ServiceConfig.srvIP,ServiceConfig.srvPort);

        PECommon.Log("NetSvc Init Done.");
    }
    public void AddMsgQue(ServerSession serverSession,GameMsg msg)
    {
        lock (obj)
        {
            msgPackQue.Enqueue(new MsgPack(serverSession,msg));
        }
    }
    public void Update()
    {
        if (msgPackQue.Count>0)
        {
            PECommon.Log("PackCount"+msgPackQue.Count);
            //取数据的时候也要加锁
            lock (obj)
            {
                MsgPack msgPack= msgPackQue.Dequeue();
                HandOutMsg(msgPack);
            }
        }
    }
    private void HandOutMsg(MsgPack msgPack)
    {
        switch ((CMD)msgPack.msg.cmd)
        {
            case CMD.None:
                break;
            case CMD.RequestLogin:
                LoginSys.Instance.ReqLogin(msgPack);
                break;
            case CMD.ReqRename:
                LoginSys.Instance.ReqRename(msgPack);
                break;
            default:
                break;
        }
    }
}
