using PEProtocol;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


public class GuideSys
{
    private static GuideSys instance = null;
    private CacheServer cacheSrv = null;
    public static GuideSys Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GuideSys();
            }
            return instance;
        }
    }
    public void Init()
    {
        cacheSrv = CacheServer.Instance;
        PECommon.Log("GuideSystem Init Done");
    }
    public void ReqGuide(MsgPack pack)
    {
        ReqGuide data = pack.msg.reqGuide;
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.RspGuide
        };
        PlayerData pd = cacheSrv.UpdatePlayerData(pack.serverSession);
        //更新引导ID
        if (pd.guideid==data.guidid)
        {
            pd.guideid += 1;
            //更新玩家数据
        }
        else//开挂了
        {
            msg.err = (int)ErroroCode.ServerDataError;
        }
        //发送数据
    }
}
