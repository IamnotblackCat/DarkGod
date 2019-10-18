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
    protected override void OnConnected()
    {
        PECommon.Log("Client is connected");
        //SendMsg(new GameMsg { text = "Welcome to connect" });
    }
    protected override void OnReciveMsg(GameMsg msg)
    {
        PECommon.Log("RecivePack CMD: "+((CMD)msg.cmd).ToString());
        //SendMsg(new GameMsg { text = "SrvRsp:" + msg.text });
    }
    protected override void OnDisConnected()
    {
        PECommon.Log("Client is disConnected");
    }
}
