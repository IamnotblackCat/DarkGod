/****************************************************
    文件：ResSvc.cs
	作者：Echo
    邮箱: 350383921@qq.com
    日期：2019/10/12 14:58:48
	功能：资源加载服务
*****************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResSvc : MonoBehaviour 
{
    public static ResSvc instance = null;
    public void InitSvc()
    {
        instance = this;
        Debug.Log("启动资源加载...");
    }
    private Action prgCB = null;//这个委托为了能在update里面实时更新进度值
    public void AsyncLoadScene(string sceneName,Action loaded)//参数委托为了复用这个函数
    {//loading界面是复用的代码

        //GameRoot.instance.loadingWnd.setwind;
        GameRoot.instance.loadingWnd.SetWndState(true);

        AsyncOperation sceneAsync = SceneManager.LoadSceneAsync(sceneName);
        prgCB = () =>
        {
            float val = sceneAsync.progress;
            GameRoot.instance.loadingWnd.SetProgress(val);
            if (val==1)
            {
                if (loaded!=null)
                {
                    loaded();
                }
                prgCB = null;
                sceneAsync = null;
                GameRoot.instance.loadingWnd.SetWndState(false);
            }
        };
        
    }
    private void Update()
    {
        if (prgCB!=null)
        {
            prgCB();
        }
    }
    private Dictionary<string, AudioClip> audioDic = new Dictionary<string, AudioClip>();
    public AudioClip LoadAudio(string path,bool cache=false)
    {
        AudioClip au = null;
        if (!audioDic.TryGetValue(path,out au))
        {
            au = Resources.Load<AudioClip>(path);
            if (cache)//如果需要缓存
            {
                audioDic.Add(path, au);
            }
            Debug.Log(path+"au: "+au.name);
        }
        return au;
    }
}