/****************************************************
    文件：ResSvc.cs
	作者：Echo
    邮箱: 350383921@qq.com
    日期：2019/10/12 14:58:48
	功能：资源加载服务
*****************************************************/

using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResSvc : MonoBehaviour 
{
    public static ResSvc instance = null;


    public void InitSvc()
    {
        instance = this;
        InitRDNameCfg(PathDefine.RDName);
        InitMapCfg(PathDefine.MapCfg);
        PECommon.Log("启动资源加载...");
    }
    private Action prgCB = null;//这个委托为了能在update里面实时更新进度值
    public void AsyncLoadScene(string sceneName,Action loaded)//参数委托为了复用这个函数
    {//loading界面是复用的代码

        //GameRoot.instance.loadingWnd.setwind;
        GameRoot.instance.loadingWnd.SetWndState(true);

        AsyncOperation sceneAsync = SceneManager.LoadSceneAsync(sceneName);
        prgCB = () =>
        {
            float val = sceneAsync.progress;
            GameRoot.instance.loadingWnd.SetProgress(val);
            if (val==1)
            {
                if (loaded!=null)
                {
                    loaded();
                }
                prgCB = null;
                sceneAsync = null;
                GameRoot.instance.loadingWnd.SetWndState(false);
            }
        };
        
    }
    private void Update()
    {
        if (prgCB!=null)
        {
            prgCB();
        }
    }
    private Dictionary<string, AudioClip> audioDic = new Dictionary<string, AudioClip>();
    public AudioClip LoadAudio(string path,bool cache=false)
    {
        AudioClip au = null;
        if (!audioDic.TryGetValue(path,out au))
        {
            au = Resources.Load<AudioClip>(path);
            if (cache)//如果需要缓存
            {
                audioDic.Add(path, au);
            }
            //PECommon.Log(path+"au: "+au.name);
        }
        return au;
    }

    private Dictionary<string, GameObject> goDic = new Dictionary<string, GameObject>();
    public GameObject LoadPrefab(string path, bool cache = false)
    {
        GameObject prefab = null;
        //如果字典里面没有
        if (!goDic.TryGetValue(path,out prefab))
        {
            prefab = Resources.Load<GameObject>(path);
            if (cache)
            {
                goDic.Add(path,prefab);
            }
        }
        GameObject go = null;
        if (prefab!=null)
        {
            go = Instantiate(prefab);
        }
        //Debug.Log(go.name+"---"+prefab.name+"--"+path.ToString());
        return go;
    }
    #region InitCfgs
    #region 初始化名字配置
    //三个list来存储姓、男名、女名
    private List<string> surNameList = new List<string>();
    private List<string> manNameList = new List<string>();
    private List<string> womanNameList = new List<string>();
    private void InitRDNameCfg(string path)
    {
        TextAsset xml = Resources.Load<TextAsset>(path);
        if (!xml)
        {
            PECommon.Log("指定文件不存在，路径：" + path, LogType.Error);
        }
        else
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml.text);
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
                foreach (XmlElement element in nodList[i].ChildNodes)
                {
                    switch (element.Name)
                    {
                        case "surname":
                            surNameList.Add(element.InnerText);
                            break;
                        case "man":
                            manNameList.Add(element.InnerText);
                            break;
                        case "woman":
                            womanNameList.Add(element.InnerText);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

    }
    public string GetRDNameData(bool man = true)//默认男性角色
    {
        //System.Random rd = new System.Random();

        string rdName = surNameList[PETools.RDInt(0, surNameList.Count - 1)];
        if (man)
        {
            rdName += manNameList[PETools.RDInt(0, manNameList.Count - 1)];
        }
        else
        {
            rdName += womanNameList[PETools.RDInt(0, womanNameList.Count - 1)];
        }
        return rdName;
    }
    #endregion

    #region 地图配置
    private Dictionary<int, MapConfig> mapCfgDataDic = new Dictionary<int, MapConfig>();
    private void InitMapCfg(string path)
    {
        TextAsset xml = Resources.Load<TextAsset>(path);
        if (!xml)
        {
            PECommon.Log("指定文件不存在，路径：" + path, LogType.Error);
        }
        else
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml.text);
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
                MapConfig mapCfg = new MapConfig();
                mapCfg.ID = ID;

                foreach (XmlElement element in nodList[i].ChildNodes)
                {
                    switch (element.Name)
                    {
                        case "mapName":
                            mapCfg.mapName = element.InnerText;
                            break;
                        case "sceneName":
                            mapCfg.sceneName = element.InnerText;
                            break;
                        case "mainCamPos":
                            {//加括号是形成块，让这个变量仅在块里面使用
                                string[] valArray = element.InnerText.Split(',');
                                //Debug.Log(valArray[0]+","+valArray[1]+","+valArray[2]);
                                mapCfg.mainCamPos = new Vector3(float.Parse(valArray[0]), float.Parse(valArray[1]), float.Parse(valArray[2]));
                            }
                            break;
                        case "mainCamRote":
                            {
                                string[] valArray = element.InnerText.Split(',');
                                mapCfg.mainCamRote = new Vector3(float.Parse(valArray[0]), float.Parse(valArray[1]), float.Parse(valArray[2]));
                            }
                            break;
                        case "playerBornPos":
                            {
                                string[] valArray = element.InnerText.Split(',');
                                //Debug.Log(valArray[0]+","+valArray[1]+","+valArray[2]);
                                mapCfg.playerBornPos = new Vector3(float.Parse(valArray[0]), float.Parse(valArray[1]), float.Parse(valArray[2]));
                            }
                            break;
                        case "playerBornRote":
                            {
                                string[] valArray = element.InnerText.Split(',');
                                mapCfg.playerBornRote = new Vector3(float.Parse(valArray[0]), float.Parse(valArray[1]), float.Parse(valArray[2]));
                            }
                            break;
                        default:
                            break;
                    }
                }
                mapCfgDataDic.Add(ID,mapCfg);
                //Debug.Log("ID:"+ID+"  mapCfg:"+mapCfg.ToString());
            }
        }
    }
    public MapConfig GetMapCfgData(int id)
    {
        MapConfig data;
        //Debug.Log(id);
        if (mapCfgDataDic.TryGetValue(id, out data))
        {
            //Debug.Log(data);
            return data;
        }
        return null;
    }
    #endregion 地图
    #endregion
}