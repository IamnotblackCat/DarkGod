/****************************************************
    文件：LoginSys.cs
	作者：Echo
    邮箱: 350383921@qq.com
    日期：2019/10/12 14:58:57
	功能：登陆注册业务系统
*****************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginSys : MonoBehaviour 
{
    public static LoginSys instance = null;

    public LoginWnd loginWnd;
    public void InitSys()
    {
        instance = this;
        Debug.Log("初始化登陆系统完成");
    }
    //进入登陆场景
    public void EnterLogin()
    {
        //异步加载登陆场景——委托如果读条完成，显示登陆界面
        ResSvc.instance.AsyncLoadScene(Constants.SceneLogin,()=> 
        {
            loginWnd.gameObject.SetActive(true);
            loginWnd.InitLogin();
        });
        //显示加载进度
        //加载完成以后打开登陆界面
    }
}