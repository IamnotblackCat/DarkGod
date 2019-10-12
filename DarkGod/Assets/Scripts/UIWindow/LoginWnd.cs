/****************************************************
    文件：LoginWnd.cs
	作者：Echo
    邮箱: 350383921@qq.com
    日期：2019/10/12 19:2:2
	功能：登陆界面功能
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class LoginWnd : MonoBehaviour 
{
    public Button btnNotice;
    public Button btnEnterGame;
    public InputField iptAccount;
    public InputField iptPassword;

    public void InitLogin()
    {
        if (PlayerPrefs.HasKey("inputAccount")&&PlayerPrefs.HasKey("inputPassword"))
        {
            iptAccount.text = PlayerPrefs.GetString("inputAccount");
            iptPassword.text = PlayerPrefs.GetString("inputPassword");
        }
        else
        {
            iptAccount.text = "";
            iptPassword.text = "";
        }
    }
}