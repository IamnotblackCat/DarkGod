/****************************************************
    文件：Constants.cs
	作者：Echo
    邮箱: 350383921@qq.com
    日期：2019/10/12 15:16:59
	功能：存储常量类名
*****************************************************/

using UnityEngine;

public class Constants 
{
    //AutoGuideNPC
    public const int NPCWiseMan = 0;
    public const int NPCGeneral = 1;
    public const int NPCArtisan = 2;
    public const int NPCTrader = 3;

    //场景类名、ID
    public const string SceneLogin = "SceneLogin";
    //public const string SceneMainCity = "SceneMainCity";
    public const int MainCityMapID = 10000;

    //音效名
    public const string BGLogin = "bgLogin";
    public const string BGMainCity = "bgMainCity";
    public const string uiClick = "uiClickBtn";
    public const string uiExtenBtn = "uiExtenBtn";
    public const string uiOpenPage = "uiOpenPage";

    //登陆音效
    public const string uiLogin = "uiLoginBtn";

    //屏幕标准宽高
    public const int screenStandardWidth = 1334;
    public const int screenStandardHeight = 750;

    //摇杆点中心小圆点最多移动的距离
    public const int screenOperationDistant = 90;

    //移动速度
    public const float playerMoveSpeed = 8;
    public const float monsterMoveSpeed = 4;

    //动画混合树平滑加快速率
    public const float accelerateSpeed = 5;

    //混合树动画设定值
    public const float blendIdle = 0;
    public const float blendWalk = 1;
}