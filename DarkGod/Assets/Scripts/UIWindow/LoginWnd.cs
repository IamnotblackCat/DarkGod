/****************************************************
    文件：LoginWnd.cs
	作者：Echo
    邮箱: 350383921@qq.com
    日期：2019/10/12 19:2:2
	功能：登陆界面功能
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class LoginWnd : WindowRoot 
{
    public Button btnNotice;
    public Button btnEnterGame;
    public InputField iptAccount;
    public InputField iptPassword;

    protected override void InitWnd()
    {
        base.InitWnd();
        //本地存储的账号密码
        if (PlayerPrefs.HasKey("acct") &&PlayerPrefs.HasKey("pass"))
        {
            iptAccount.text = PlayerPrefs.GetString("acct");
            iptPassword.text = PlayerPrefs.GetString("pass");
        }
        else
        {
            iptAccount.text = "";
            iptPassword.text = "";
        }
    }
    //登陆按钮
    public void ClickEnterGame()
    {
        audioSvc.PlayUIAudio(Constants.uiLogin);
        string acct= iptAccount.text;
        string pass = iptPassword.text;
        if (acct!=""&&pass!="")
        {
            //存储账号密码
            PlayerPrefs.SetString("acct",acct);
            PlayerPrefs.SetString("pass",pass);

            //发送网络消息，请求登陆

            //模拟 接收成功——这些代码后面会删除
            LoginSys.instance.RspLogin();
        }
        else
        {
            GameRoot.instance.AddTips("帐号或者密码为空");
        }
    }
    public void ClickNoticeBtn()
    {
        GameRoot.instance.AddTips("功能正在开发中。。。");
        audioSvc.PlayUIAudio(Constants.uiClick);
    }
}