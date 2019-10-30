/****************************************************
    文件：InfoWnd.cs
	作者：Echo
    邮箱: 350383921@qq.com
    日期：2019/10/30 11:45:54
	功能：Nothing
*****************************************************/

using PEProtocol;
using UnityEngine;
using UnityEngine.UI;

public class InfoWnd : WindowRoot 
{

    public Text txtCharactor;
    public Text txtExp;
    public Image imgExp;
    public Text txtPower;
    public Image imgPower;
    public Text txtJob;
    public Text txtFight;
    public Text txtHP;
    public Text txtDamage;
    public Text txtDefend;

    protected override void InitWnd()
    {
        base.InitWnd();
        RefreshUI();
    }
    private void RefreshUI()
    {
        PlayerData pd = GameRoot.instance.Playerdata;
        SetText(txtCharactor,pd.name+" LV"+pd.lv);
        SetText(txtExp,pd.exp+"/"+PECommon.GetExpUpValByLv(pd.lv));
        imgExp.fillAmount = (pd.exp*1.0f/PECommon.GetExpUpValByLv(pd.lv));
        SetText(txtPower,pd.power);
        imgPower.fillAmount = (pd.power*1.0f/PECommon.GetPowerLimit(pd.lv));
        SetText(txtJob, " 职业  暗夜刺客");
        SetText(txtFight, " 战力  "+PECommon.GetFightByPlayerData(pd));
        SetText(txtHP, " 生命  " + pd.hp);
        SetText(txtDamage, " 伤害  "+(pd.ad+pd.ap));
        SetText(txtDefend, " 防御  " + (pd.addef+pd.apdef));
    }
}