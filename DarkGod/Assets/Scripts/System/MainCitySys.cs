/****************************************************
    文件：MainCitySys.cs
	作者：Echo
    邮箱: 350383921@qq.com
    日期：2019/10/25 9:28:35
	功能：主城业务系统
*****************************************************/
using System;
using UnityEngine;

public class MainCitySys : SystemRoot 
{
    //单例
    public static MainCitySys Instance = null;

    public MainCityWnd cityWnd;

    public override void InitSys()
    {
        base.InitSys();
        Instance = this;
        PECommon.Log("Main City Init...");
    }

    public void EnterMainCity()
    {
        resSvc.AsyncLoadScene(Constants.SceneMainCity,()=>
        {
            PECommon.Log("Enter MainCity already");
            //打开UI
            cityWnd.SetWndState();
            //背景音乐
            audioSvc.PlayBGMusic(Constants.BGMainCity);
            //加载主角
        });
    }
}