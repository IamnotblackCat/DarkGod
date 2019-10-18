/****************************************************
	文件：PECommon.cs
	作者：Echo
	邮箱: 350383921@qq.com
	日期：2019/10/17 18:12   	
	功能：客户端服务端公用工具类，这个放在协议工程里面就两个端都能用，因为解决方案放在Unity里面了
*****************************************************/
using PENet;

public enum LogType
{
Log=0,
Warn=1,
Error=2,
Info=3
}
public class PECommon
{//默认为log消息,这个方法是对PETool的LogMsg进行封装，因为PETool的命名空间是PENet，封装以后作为静态方法直接用这个类调用，减少代码量
    public static void Log(string msg="",LogType tp=LogType.Log)
    {
        LogLevel lv=(LogLevel)tp;
       PETool.LogMsg(msg,lv);
    }
}
