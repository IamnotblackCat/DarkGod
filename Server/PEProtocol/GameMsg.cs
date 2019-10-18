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
    }
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
        public int lv;
        public string name;
        public int coin;
        public int diamond;
        public int power;
        public int exp;
    }
    public enum ErroroCode
    {
        None=0,//没有错误
        AcctIsOnline,//账号已经上线
        WrongPass,
    }
    public enum CMD
    {
        None=0,
        //登陆相关100
        RequestLogin=101,
        ResponLogin=102,
    
    }
    public class ServiceConfig
    {
        public const string srvIP = "127.0.0.1";
        public const int srvPort = 17666;
    }
}
