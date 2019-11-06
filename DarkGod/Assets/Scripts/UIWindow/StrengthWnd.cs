/****************************************************
    文件：StrengthWnd.cs
	作者：Echo
    邮箱: 350383921@qq.com
    日期：2019/11/3 11:29:44
	功能：强化装备界面
*****************************************************/

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StrengthWnd : WindowRoot 
{
    public Transform[] posBtnTrans;

    protected override void InitWnd()
    {
        base.InitWnd();

        RegClickEvts();
        ClickPosItem(0);
    }
    private void RegClickEvts()
    {
        for (int i = 0; i < posBtnTrans.Length; i++)
        {
            Image img = posBtnTrans[i].GetComponent<Image>();
            //Transform trans = posBtnTrans[i].transform;

            OnClick(img.gameObject, (object obj) =>
             {
                 audioSvc.PlayUIAudio(Constants.uiClick);
                 ClickPosItem((int)obj);
             },i);
        }
    }
    private void ClickPosItem(int index)
    {
        PECommon.Log("Click Item: "+index);
        int currentIndex = index;
        for (int i = 0; i < posBtnTrans.Length; i++)
        {
            if (i==currentIndex)
            {
                if (!posBtnTrans[i].parent.GetChild(0).gameObject.activeSelf)
                {
                    //Debug.Log(posBtnTrans[i].parent.GetChild(0).gameObject.name);
                    posBtnTrans[i].parent.GetChild(0).gameObject.SetActive(true);
                }
            }
            else
            {
                if (posBtnTrans[i].parent.GetChild(0).gameObject.activeSelf)
                {
                    posBtnTrans[i].parent.GetChild(0).gameObject.SetActive(false);

                }
            }
        }
    }
}