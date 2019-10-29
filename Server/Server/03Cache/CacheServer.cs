/****************************************************
	文件：CacheServer.cs
	作者：Echo
	邮箱: 350383921@qq.com
	日期：2019/10/18 14:14   	
	功能：缓存层
*****************************************************/

using PEProtocol;
using System.Collections.Generic;

public class CacheServer
{
    private static CacheServer instance = null;
    public static CacheServer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CacheServer();
            }
            return instance;
        }
    }
    private Dictionary<string, ServerSession> onLineAcct = new Dictionary<string, ServerSession>();
    private Dictionary<ServerSession, PlayerData> onLineSessionDic = new Dictionary<ServerSession, PlayerData>();
    private DBmanager db;
    public void Init()
    {
        db = DBmanager.Instance;
        PECommon.Log("CacheServer Init Done");
    }
    public bool IsAcctOnLine(string acct)
    {
        return onLineAcct.ContainsKey(acct);
    }
    /// <summary>
    /// 根据账号密码返回玩家数据，密码错误返回null，账号不存在默认创建新的账号
    /// </summary>
    public PlayerData GetPlayerData(string acct, string pass)
    {
        //TODO  从数据库中查找账号数据
        return db.QueryPlayerdata(acct, pass);
    }
    /// <summary>
    /// 账号上线缓存数据
    /// </summary>
    public void AcctOnline(string acct, ServerSession serverSession, PlayerData playerData)
    {
        onLineAcct.Add(acct, serverSession);
        onLineSessionDic.Add(serverSession, playerData);
    }
    public bool IsNameExist(string name)
    {
        return db.QueryNameIsExist(name);
    }
    public PlayerData UpdatePlayerData(ServerSession session)
    {
        if (onLineSessionDic.TryGetValue(session, out PlayerData playerData))
        {
            return playerData;
        }
        else
        {
            return null;
        }
    }
    public bool UpdatePlayerData(int id, PlayerData playerData)
    {
        return db.UpdatePlayerData(id, playerData);
    }
    public void AcctOffLine(ServerSession serverSession)
    {
        foreach (var item in onLineAcct)
        {
            if (item.Value==serverSession)
            {
                onLineAcct.Remove(item.Key);
                break;
            }
        }
        bool succ=onLineSessionDic.Remove(serverSession);
        PECommon.Log("账号下线,Session:"+serverSession.session);
    }
}
