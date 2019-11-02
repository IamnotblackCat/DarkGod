/****************************************************
	文件：GuideSys.cs
	作者：Echo
	邮箱: 350383921@qq.com
	日期：2019/11/02 11:38   	
	功能：任务引导服务器端
*****************************************************/
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
    private CfgSvc cfgSvc = null;
    public void Init()
    {
        cacheSrv = CacheServer.Instance;
        cfgSvc = CfgSvc.Instance;
        PECommon.Log("GuideSystem Init Done");
    }
    public void ReqGuide(MsgPack pack)
    {
        ReqGuide data = pack.msg.reqGuide;
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.RspGuide
        };
        PlayerData pd = cacheSrv.GetPlayerDataBySession(pack.serverSession);
        GuideCfg guideConfig = cfgSvc.GetGuideCfgData(pd.guideid);
        //更新引导ID
        if (pd.guideid==data.guidid)
        {
            pd.guideid += 1;
            //更新玩家数据
            pd.coin += guideConfig.coin;
            CalculateExp(pd, guideConfig.exp);
            if (!cacheSrv.UpdatePlayerData(pd.id,pd))
            {
                msg.err = (int)ErroroCode.UpdateDBError;
            }
            else
            {
                msg.rspGuide = new RspGuide
                {
                    coin = pd.coin,
                    exp=pd.exp,
                    lv=pd.lv,
                    guideid=pd.guideid,
                };
            }
        }
        else//开挂了
        {
            msg.err = (int)ErroroCode.ServerDataError;
        }
        //发送数据
        pack.serverSession.SendMsg(msg);
    }
    private void CalculateExp(PlayerData pd,int addExp)
    {
        //升级之后当前等级经验是清零的
        //int currentExp = pd.exp; ;
        //int lv=pd.lv;
        int upNeedExp;

        upNeedExp = PECommon.GetExpUpValByLv(pd.lv)-pd.exp;
        while (true)
        {
            if (addExp>=upNeedExp)
            {
                addExp -= upNeedExp;
                pd.lv++;
                pd.exp = 0;
            }
            else
            {
                pd.exp += addExp;
                break;
            }
        }
    }
}
