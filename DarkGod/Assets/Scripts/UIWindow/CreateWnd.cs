/****************************************************
    文件：CreateWnd.cs
	作者：Echo
    邮箱: 350383921@qq.com
    日期：2019/10/15 15:55:10
	功能：创建角色面板
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class CreateWnd : WindowRoot 
{
    public InputField iptRDName;
    protected override void InitWnd()
    {
        base.InitWnd();

        iptRDName.text = resSvc.GetRDNameData(false);
    }
    public void ClickRandomBtn()
    {
        audioSvc.PlayUIAudio(Constants.uiClick);
        string rdName = resSvc.GetRDNameData(false);
        iptRDName.text = rdName;
    }
    public void ClickEnterBtn()
    {
        audioSvc.PlayUIAudio(Constants.uiClick);
        if (iptRDName.text!="")
        {
            //发送名字到服务器，登陆主城

        }
        else
        {
            GameRoot.instance.AddTips("当前名称不符合规范");
        }
    }
}
