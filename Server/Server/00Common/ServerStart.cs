/****************************************************
	文件：ServerStart.cs
	作者：Echo
	邮箱: 350383921@qq.com
	日期：2019/10/17 15:24   	
	功能：服务器入口
*****************************************************/

namespace Server
{
    class ServerStart
    {
        static void Main(string[] args)
        {
            ServerRoot.Instance.Init();

            //死循环的目的是防止关闭。
            while (true)
            {
                ServerRoot.Instance.Update();
            }
        }
    }
}
