/****************************************************
	文件：CfgSvc.cs
	作者：Echo
	邮箱: 350383921@qq.com
	日期：2019/11/02 11:38   	
	功能：配置数据服务
*****************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;


public class CfgSvc
{
    private static CfgSvc instance = null;
    public static CfgSvc Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CfgSvc();
            }
            return instance;
        }
    }
    public void Init()
    {
        InitGuideCfg();
        PECommon.Log("CfgSvc Init Done.");
    }
    #region 自动任务
    private Dictionary<int, GuideCfg> autoGuideDic = new Dictionary<int, GuideCfg>();
    private void InitGuideCfg()
    {
        
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\GitHub\DarkGod\DarkGod\Assets\Resources\ResCfgs\guideCfg.xml");
            //选中子节点集合
            XmlNodeList nodList = doc.SelectSingleNode("root").ChildNodes;
            for (int i = 0; i < nodList.Count; i++)
            {
                XmlElement ele = nodList[i] as XmlElement;
                if (ele.GetAttributeNode("ID") == null)
                {//不包含ID的节点，直接跳到下一个遍历，安全校验
                    continue;
                }
                int ID = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);
                GuideCfg guideCfg = new GuideCfg();
                guideCfg.ID = ID;

                foreach (XmlElement element in nodList[i].ChildNodes)
                {
                    switch (element.Name)
                    {
                        case "coin":
                            guideCfg.coin = int.Parse(element.InnerText);
                            break;
                        case "exp":
                            guideCfg.exp = int.Parse(element.InnerText);
                            break;
                        default:
                            break;
                    }
                }
                autoGuideDic.Add(ID, guideCfg);
                //Debug.Log("ID:"+ID+"  mapCfg:"+mapCfg.ToString());
            }
        
    }
    public GuideCfg GetGuideCfgData(int id)
    {
        GuideCfg agc = null;

        //Debug.Log(id);
        if (autoGuideDic.TryGetValue(id, out agc))
        {
            //Debug.Log(data);
            return agc;
        }
        return null;
    }
    #endregion
}
public class GuideCfg : BaseData<GuideCfg>
{
    public int coin;
    public int exp;
}
public class BaseData<T>
{
    public int ID;
}
