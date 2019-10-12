/****************************************************
    文件：LoadingWnd.cs
	作者：Echo
    邮箱: 350383921@qq.com
    日期：2019/10/12 15:39:25
	功能：Loading界面相关功能
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class LoadingWnd : MonoBehaviour 
{
    public Text textTips;
    public Image imgFg;
    public Image imgPoint;
    public Text textProgress;

    private float fgWidth;

    public void InitWnd()
    {//为了获得填充条的宽度
        fgWidth = imgFg.GetComponent<RectTransform>().sizeDelta.x;

        textTips.text = "这是一条游戏tips";
        imgFg.fillAmount = 0;
        imgPoint.transform.position = new Vector3(-fgWidth/2, 0,0);
        textProgress.text = "0%";
    }
    public void SetProgress(float prg)
    {
        textProgress.text = (int)(prg * 100) + "%";
        imgFg.fillAmount = prg;

        float posX = prg * fgWidth - 545;
        //Debug.Log(posX+"prg: "+prg+"fgWidth: "+fgWidth);
        //相对于锚点的参考位置
        imgPoint.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX,0);
    }
    
}