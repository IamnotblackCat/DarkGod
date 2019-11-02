/****************************************************
	文件：LoginSys.cs
	作者：Echo
	邮箱: 350383921@qq.com
	日期：2019/10/17 15:31   	
	功能：登陆系统
*****************************************************/

using PENet;
using PEProtocol;

public class LoginSys
{
    private static LoginSys instance = null;
    private CacheServer cacheSrv = null;
    public static LoginSys Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LoginSys();
            }
            return instance;
        }
    }
    public void Init()
    {
        PECommon.Log("LoginSystem Init Done");
        cacheSrv = CacheServer.Instance;
    }
    //登陆的请求
    public void ReqLogin(MsgPack pack)
    {
        RequestLogin data = pack.msg.reqLogin;
        //账号是否上线
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.ResponLogin
           
        };
        if (cacheSrv.IsAcctOnLine(data.acct))
        {
            //已上线——返回错误信息
            msg.err = (int)ErroroCode.AcctIsOnline;
        }
        else
        {
            //未上线   
            //账号是否存在

            PlayerData pd= cacheSrv.GetPlayerData(data.acct,data.pass);
            //账号密码为空就查不到数据，就会为空
            if (pd==null)
            {
                //存在，密码错误
                msg.err = (int)ErroroCode.WrongPass;
            }
            else
            {
                msg.respLogin = new ResponLogin
                {
                    playerData = pd
                };
                //缓存账号数据
                cacheSrv.AcctOnline(data.acct, pack.serverSession, pd);
            }
        }

        //回应客户端

        pack.serverSession.SendMsg(msg);
    }
    //处理重命名请求
    public void ReqRename(MsgPack pack)
    {
        ReqRename data = pack.msg.reqRename;
        GameMsg msg = new GameMsg
        {
            cmd=(int)CMD.RespondRename,
            //下面的代码太早了，这个名字也许存在
            //respondRename = new RespondRename
            //{
            //    name = data.name
            //}
        };
        //名字存在，返回错误码
        if (cacheSrv.IsNameExist(data.name))
        {
            msg.err = (int)ErroroCode.NameIsExist;
        }
        //不存在，更新缓存和数据库数据
        else
        {
            PlayerData playerData = cacheSrv.GetPlayerDataBySession(pack.serverSession);
            playerData.name = data.name;
            //数据库更新出错
            if (!cacheSrv.UpdatePlayerData(playerData.id,playerData))
            {
                msg.err = (int)ErroroCode.UpdateDBError;
            }
            else
            {
                msg.respondRename = new RespondRename
                {
                    name = data.name
                };
            }
        }
        //发送回去
        pack.serverSession.SendMsg(msg);
    }
    public void ClearOfflineData(ServerSession serverSession)
    {
        cacheSrv.AcctOffLine(serverSession);
    }
}
