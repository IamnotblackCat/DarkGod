
using UnityEngine;

public class BaseData<T>
{
    public int ID;
}
public class MapConfig : BaseData<MapConfig>
{
    public string mapName;
    public string sceneName;
    public Vector3 mainCamPos;
    public Vector3 mainCamRote;
    public Vector3 playerBornPos;
    public Vector3 playerBornRote;
}
