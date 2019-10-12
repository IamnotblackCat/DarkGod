/****************************************************
    文件：ResSvc.cs
	作者：Echo
    邮箱: 350383921@qq.com
    日期：2019/10/12 14:58:48
	功能：资源加载服务
*****************************************************/

using System;
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
        GameRoot.instance.loadingWnd.gameObject.SetActive(true);
        GameRoot.instance.loadingWnd.InitWnd();

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
                GameRoot.instance.loadingWnd.gameObject.SetActive(false);
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
}