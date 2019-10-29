/****************************************************
	文件：ServerSession.cs
	作者：Echo
	邮箱: 350383921@qq.com
	日期：2019/10/17 16:03   	
	功能：用来跟客户端进行联系，网络会话连接
*****************************************************/


using PENet;
using PEProtocol;

public class ServerSession:PESession<GameMsg>
{
    public int session = ServerRoot.Instance.GetSession();
    protected override void OnConnected()
    {
        PECommon.Log("Session:"+session+"--Client is connected");
        //SendMsg(new GameMsg { text = "Welcome to connect" });
    }
    protected override void OnReciveMsg(GameMsg msg)
    {
        PECommon.Log("Session:" + session + "RecivePack CMD: " +((CMD)msg.cmd).ToString());
        NetService.Instance.AddMsgQue(this,msg);
        //SendMsg(new GameMsg { text = "SrvRsp:" + msg.text });
    }
    protected override void OnDisConnected()
    {
        LoginSys.Instance.ClearOfflineData(this);
        PECommon.Log("Session:"+session+"--Client is disConnected");
    }
}
