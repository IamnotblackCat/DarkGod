/****************************************************
    文件：GameRoot.cs
	作者：Echo
    邮箱: 350383921@qq.com
    日期：2019/10/12 14:59:31
	功能：游戏启动入口
*****************************************************/

using UnityEngine;

public class GameRoot : MonoBehaviour 
{
    public static GameRoot instance = null;
    public LoadingWnd loadingWnd;
    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
        Debug.Log("游戏开始。。。");
        Init();    
    }
    private void Init()
    {
        //启动资源加载服务
        ResSvc resSvc = GetComponent<ResSvc>();
        resSvc.InitSvc();
        //启动登陆界面初始化
        LoginSys loginSys = GetComponent<LoginSys>();
        loginSys.InitSys();
        loginSys.EnterLogin();
    }
}