/****************************************************
    文件：GameRoot.cs
	作者：Echo
    邮箱: 350383921@qq.com
    日期：2019/10/12 14:59:31
	功能：游戏启动入口
*****************************************************/

using PEProtocol;
using UnityEngine;

public class GameRoot : MonoBehaviour 
{
    public static GameRoot instance = null;
    public LoadingWnd loadingWnd;
    public DynamicWnd dynamicWnd;

    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
        PECommon.Log("游戏开始。。。");
        ClearUIRoot();
        Init();    
    }
    private void Init()
    {

        //服务模块初始化
        NetService netService = GetComponent<NetService>();
        netService.InitSvc();
        ResSvc resSvc = GetComponent<ResSvc>();
        resSvc.InitSvc();
        AudioSvc audioSvc = GetComponent<AudioSvc>();
        audioSvc.InitSvc();

        //业务系统初始化
        LoginSys loginSys = GetComponent<LoginSys>();
        loginSys.InitSys();

        //进入登陆场景并加载UI
        loginSys.EnterLogin();

        
    }
    //初始化的时候确保所有的UI除了提示面板都是隐藏的
    private void ClearUIRoot()
    {
        Transform canvasTrans = transform.Find("Canvas");
        for (int i = 0; i < canvasTrans.childCount; i++)
        { 
            canvasTrans.GetChild(i).gameObject.SetActive(false);
        }
        dynamicWnd.SetWndState();
    }
    public void AddTips(string tips)
    {
        dynamicWnd.AddTips(tips);
    }
    private PlayerData playerData;
    public PlayerData Playerdata
    {
        get { return playerData; }
    }
    public void SetPlayerData(ResponLogin data)
    {
        playerData = data.playerData;
    }

}