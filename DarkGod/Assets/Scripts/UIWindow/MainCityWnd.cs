/****************************************************
    文件：MainCityWnd.cs
	作者：Echo
    邮箱: 350383921@qq.com
    日期：2019/10/25 9:17:49
	功能：主城UI界面
*****************************************************/
using UnityEngine.UI;
using UnityEngine;
using PEProtocol;
using UnityEngine.EventSystems;

public class MainCityWnd : WindowRoot 
{
    #region Public UI Transform
    public Image imgTouch;
    public Image imgDirBg;
    public Image imgDirPoint;

    public Text txtFight;
    public Text txtLV;
    public Text txtPower;
    public Image imgPowerPrg;
    public Text txtName;
    public Text txtExpPrg; 

    public Transform expProgramTrans;

    #endregion
    public Animation menuAnim;
    private bool menuState = true;//true是打开，false收起

    protected override void InitWnd()
    {
        base.InitWnd();
        SetActive(imgDirPoint,false);
        RegistrTouchEvts();
        RefreshUI();
    }
    private void RefreshUI()
    {
        PlayerData pd = GameRoot.instance.Playerdata;

        SetText(txtFight, PECommon.GetFightByPlayerData(pd));
        SetText(txtLV,pd.lv);
        SetText(txtPower,"体力："+pd.power+"/"+PECommon.GetPowerLimit(pd.lv));
        //需要限制不要超出范围吗？
        imgPowerPrg.fillAmount = pd.power * 1.0f / PECommon.GetPowerLimit(pd.lv);
        SetText(txtName,pd.name);

        int expValPercent = (int)(pd.exp * 1.0f / PECommon.GetExpUpValByLv(pd.lv)*100);
        SetText(txtExpPrg,expValPercent+"%");
        int index = expValPercent / 10;
        GridLayoutGroup grid = expProgramTrans.GetComponent<GridLayoutGroup>();
        //得到 标准高度和当前高度的比例，然后乘以当前宽度得到真实宽度，然后减掉间隙计算经验条宽度
        float screenRate = 1.0f * Constants.screenStandardHeight / Screen.height;
        float screenWidth = Screen.width * screenRate;
        float width = (screenWidth - 180) / 10;
        
        grid.cellSize = new Vector2(width,7);

        for (int i = 0; i < expProgramTrans.childCount; i++)
        {
            Image expImageValue = expProgramTrans.GetChild(i).GetComponent<Image>();
            if (i<index)
            {
                expImageValue.fillAmount = 1;
            }
            else if (i==index)
            {
                expImageValue.fillAmount = expValPercent*1.0f % 10 / 10;
            }
            else
            {
                expImageValue.fillAmount = 0;
            }
        }
    }

    public void ClickMenuBtn()
    {
        audioSvc.PlayUIAudio(Constants.uiExtenBtn);
        menuState = !menuState;

        AnimationClip clip = null;
        //取反之后的状态
        if (menuState)
        {
            clip = menuAnim.GetClip("OpenMainCityBtn");
        }
        else
        {
            clip = menuAnim.GetClip("CloseMainCityBtn");
        }
        menuAnim.Play(clip.name);
    }

    public void RegistrTouchEvts()
    {
        //添加监听器
        PEListener listener = imgTouch.gameObject.AddComponent<PEListener>();
        listener.onClickDown = (PointerEventData evt) =>
          {
              imgDirBg.transform.position = evt.position;
          };
    }
}