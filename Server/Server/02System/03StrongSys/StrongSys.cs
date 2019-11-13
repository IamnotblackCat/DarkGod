/****************************************************
	文件：03StrongSys.cs
	作者：Echo
	邮箱: 350383921@qq.com
	日期：2019/11/13 14:49   	
	功能：强化升级系统
*****************************************************/
using PEProtocol;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


public class StrongSys
{
    private static StrongSys instance = null;
    private CacheServer cacheSrv = null;
    public static StrongSys Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new StrongSys();
            }
            return instance;
        }
    }
    public void Init()
    {
        cacheSrv = CacheServer.Instance;
        PECommon.Log("StrongSystem Init Done");
    }
    public void ReqStrong(MsgPack msgPack)
    {
        ReqStrong data = msgPack.msg.reqStrong;

        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.ResStrong,
        };
        PlayerData pd = cacheSrv.GetPlayerDataBySession(msgPack.serverSession);
        int curtStarLv = pd.strengthArray[data.pos];
        //判断条件是否符合

        //修改数据

        //发送消息
    }
}
