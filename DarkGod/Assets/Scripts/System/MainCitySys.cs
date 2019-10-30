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
    public InfoWnd infoWnd;

    private PlayerController playerCtrl;
    public override void InitSys()
    {
        base.InitSys();
        Instance = this;
        PECommon.Log("Main City Init...");
    }

    public void EnterMainCity()
    {
        MapConfig mapData = resSvc.GetMapCfgData(Constants.MainCityMapID);
        //Debug.Log(mapData.sceneName);
        resSvc.AsyncLoadScene(mapData.sceneName,()=>
        {
            PECommon.Log("Enter MainCity already");
            //加载主角
            LoadPlayer(mapData);
            //打开UI
            cityWnd.SetWndState();
            //背景音乐
            audioSvc.PlayBGMusic(Constants.BGMainCity);
        });
    }
    private void LoadPlayer(MapConfig mapData)
    {
        GameObject player = resSvc.LoadPrefab(PathDefine.AssassinCityPlayerPrefab, true);
        //Debug.Log(player.transform.position);
        player.transform.position = mapData.playerBornPos;
        player.transform.localEulerAngles = mapData.playerBornRote;
        player.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
        playerCtrl= player.GetComponent<PlayerController>();
        playerCtrl.Init();

        //相机初始化
        Camera.main.transform.position = mapData.mainCamPos;
        //Debug.Log("CameraPos"+Camera.main.transform.position+"--mapDataCamPos"+mapData.mainCamPos);
        Camera.main.transform.eulerAngles = mapData.mainCamRote;
    }
    public void SetMoveDir(Vector2 dir)
    {
        if (dir==Vector2.zero)
        {
            playerCtrl.SetBlend(Constants.blendIdle);
        }
        else
        {
            playerCtrl.SetBlend(Constants.blendWalk);
        }
        playerCtrl.Dir = dir;
    }
    public void OpenInfoWnd()
    {
        infoWnd.SetWndState();
    }
}