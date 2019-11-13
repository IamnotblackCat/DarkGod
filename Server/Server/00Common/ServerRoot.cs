/****************************************************
	文件：ServerRoot.cs
	作者：Echo
	邮箱: 350383921@qq.com
	日期：2019/10/17 15:24   	
	功能：服务器初始化
*****************************************************/

public class ServerRoot
{
    private static ServerRoot instance=null;
    public static ServerRoot Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ServerRoot();
            }
            return instance;
        }
    }
    public void Init()
    {
        //数据库层
        DBmanager.Instance.Init();
        //服务层
        CacheServer.Instance.Init();
        NetService.Instance.Init();
        CfgSvc.Instance.Init();
        //业务系统层
        LoginSys.Instance.Init();
        GuideSys.Instance.Init();
        StrongSys.Instance.Init();
    }
    public void Update()
    {
        NetService.Instance.Update();
    }
    private int session = 0;
    public int GetSession()
    {
        if (session == int.MaxValue)
        {
            session = 0;
        }
        return session += 1;
    }
}
