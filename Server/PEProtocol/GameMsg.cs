/****************************************************
	文件：Class1.cs
	作者：Echo
	邮箱: 350383921@qq.com
	日期：2019/10/17 15:52   	
	功能：网络通信协议（客户端服务端公用）
*****************************************************/

using PENet;
using System;

namespace PEProtocol
{
    [Serializable]
    public class GameMsg:PEMsg
    {//这里的目的是将信息进行分类区分，明确信息的意义
        public RequestLogin reqLogin;
        public ResponLogin respLogin;
        public ReqRename reqRename;
        public RespondRename respondRename;
    }
    #region 登陆相关
    [Serializable]
    public class RequestLogin
    {
        public string acct;
        public string pass;
    }
    [Serializable]
    public class ResponLogin//回应应该持有的玩家数据
    {
        public PlayerData playerData;
    }
    [Serializable]
    public class PlayerData
    {
        public int id;
        public string name;
        public int lv;
        public int exp;
        public int power;
        public int coin;
        public int diamond;

        public int hp;
        public int ad;
        public int ap;
        public int addef;
        public int apdef;
        public int dodge;//闪避概率
        public int pierce;//穿透比率
        public int critical;//暴击概率

    }
    [Serializable]
    public class ReqRename
    {
        public string name;
    }
    [Serializable]
    public class RespondRename
    {
        public string name;
    }
    #endregion
    public enum ErroroCode
    {
        None=0,//没有错误
        UpdateDBError,//数据库更新出错
        AcctIsOnline,//账号已经上线
        WrongPass,
        NameIsExist,//名字已存在
    }
    public enum CMD
    {
        None=0,
        //登陆相关100
        RequestLogin=101,
        ResponLogin=102,
        ReqRename=103,
        RespondRename=104,
    }
    public class ServiceConfig
    {
        public const string srvIP = "127.0.0.1";
        public const int srvPort = 17666;
    }
}
