/****************************************************
	文件：DBmanager.cs
	作者：Echo
	邮箱: 350383921@qq.com
	日期：2019/10/19 10:33   	
	功能：数据库管理类
*****************************************************/


using MySql.Data.MySqlClient;
using PEProtocol;
using System;

public class DBmanager
{
    private static DBmanager instance = null;
    public static DBmanager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DBmanager();
            }
            return instance;
        }
    }
    private MySqlConnection conn = null;
    public void Init()
    {
        conn = new MySqlConnection("server=localhost;userid=root;password=;database=darkgod;charset=utf8");
        conn.Open();
        PECommon.Log("DBMr Init Done");

       //QueryPlayerdata("xxxxx","111111");
    }
    public PlayerData QueryPlayerdata(string acct,string pass)
    {
        PlayerData pd =null;
        MySqlDataReader reader =null;
        bool isNew = true;

        try
        {
            MySqlCommand cmd = new MySqlCommand("select * from account where acct=@acct", conn);
            cmd.Parameters.AddWithValue("acct", acct);
            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                isNew = false;
                string _pass = reader.GetString("pass");
                if (_pass.Equals(pass))//密码正确
                {
                    pd = new PlayerData
                    {
                        id = reader.GetInt32("id"),
                        lv = reader.GetInt32("lv"),
                        coin = reader.GetInt32("coin"),
                        diamond = reader.GetInt32("diamond"),
                        power = reader.GetInt32("power"),
                        exp = reader.GetInt32("exp"),
                        name = reader.GetString("name"),

                        hp = reader.GetInt32("hp"),
                        ad = reader.GetInt32("ad"),
                        ap = reader.GetInt32("ap"),
                        addef = reader.GetInt32("addef"),
                        apdef = reader.GetInt32("apdef"),
                        dodge = reader.GetInt32("dodge"),
                        pierce = reader.GetInt32("pierce"),
                        critical = reader.GetInt32("critical")

                    };
                }
            }

        }
        catch (Exception e)
        {
            PECommon.Log("Query playerdata By Acct&Pass Error:" + e, LogType.Error);
        }
        finally
        {
            if (reader!=null)
            {
                reader.Close();
            }
            if (isNew)
            {
                pd = new PlayerData
                {
                    id = -1,
                    name = "",
                    lv=1,
                    exp=0,
                    coin=5000,
                    diamond=500,
                    power=150,

                    hp = 2000,
                    ad = 275,
                    ap = 265,
                    addef = 67,
                    apdef = 43,
                    dodge = 7,
                    pierce = 5,
                    critical = 2,

                };
                //这一行代码很重要，id要重新设置为自动增加的id，否则所有新建账号的id都是-1了
                pd.id= InsertNewAcctData(acct,pass,pd);
            }
        }
        return pd;
    }
    private int InsertNewAcctData(string acct,string pass,PlayerData pd)
    {
        int id = -1;
        try
        {
            MySqlCommand cmd = new MySqlCommand
                ("insert into account set acct=@acct,pass=@pass,name=@name,lv=@lv,power=@power,coin=@coin,diamond=@diamond,exp=@exp,"+
                "hp=@hp,ad=@ad,ap=@ap,addef=@addef,apdef=@apdef,dodge=@dodge,pierce=@pierce,critical=@critical", conn);
            cmd.Parameters.AddWithValue("id", id);
            cmd.Parameters.AddWithValue("acct", acct);
            cmd.Parameters.AddWithValue("pass", pass);
            cmd.Parameters.AddWithValue("name", pd.name);
            cmd.Parameters.AddWithValue("lv", pd.lv);
            cmd.Parameters.AddWithValue("exp", pd.exp);
            cmd.Parameters.AddWithValue("power", pd.power);
            cmd.Parameters.AddWithValue("coin", pd.coin);
            cmd.Parameters.AddWithValue("diamond", pd.diamond);

            cmd.Parameters.AddWithValue("hp",pd.hp);
            cmd.Parameters.AddWithValue("ad",pd.ad);
            cmd.Parameters.AddWithValue("ap",pd.ap);
            cmd.Parameters.AddWithValue("addef",pd.addef);
            cmd.Parameters.AddWithValue("apdef",pd.apdef);
            cmd.Parameters.AddWithValue("dodge",pd.dodge);
            cmd.Parameters.AddWithValue("pierce",pd.pierce);
            cmd.Parameters.AddWithValue("critical",pd.critical);

            cmd.ExecuteNonQuery();
            id = (int)cmd.LastInsertedId;
        }
        catch (Exception e)
        {
            PECommon.Log("Insert playerdata Error:" + e, LogType.Error);
        }
        
        return id;
    }
    public bool QueryNameIsExist(string name)
    {
        bool isExist = false;
        MySqlDataReader reader = null;
        try
        {
            MySqlCommand cmd = new MySqlCommand("Select * from account where name=@name", conn);
            cmd.Parameters.AddWithValue("name", name);
            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                isExist = true;
            }

        }
        catch (Exception e)
        {
            PECommon.Log("数据库查询出错" + e, LogType.Error);
        }
        finally
        {
            if (reader!=null)
            {//手动关闭
                reader.Close();

            }
        }
        return isExist;
    }
    public bool UpdatePlayerData(int id,PlayerData playerData)
    {
        try
        {//判断信息的条件是id
            MySqlCommand cmd = new MySqlCommand
                ("Update account set name=@name,lv=@lv,exp=@exp,power=@power,diamond=@diamond,coin=@coin,"+
                "hp=@hp,ad=@ad,ap=@ap,addef=@addef,apdef=@apdef,dodge=@dodge,pierce=@pierce,critical=@critical where id=@id", conn);
            cmd.Parameters.AddWithValue("id",id);
            cmd.Parameters.AddWithValue("name",playerData.name);
            cmd.Parameters.AddWithValue("lv",playerData.lv);
            cmd.Parameters.AddWithValue("exp",playerData.exp);
            cmd.Parameters.AddWithValue("power",playerData.power);
            cmd.Parameters.AddWithValue("diamond",playerData.diamond);
            cmd.Parameters.AddWithValue("coin",playerData.coin);
            cmd.Parameters.AddWithValue("hp",playerData.hp);
            cmd.Parameters.AddWithValue("ad",playerData.ad);
            cmd.Parameters.AddWithValue("ap",playerData.ap);

            cmd.Parameters.AddWithValue("addef",playerData.addef);
            cmd.Parameters.AddWithValue("apdef", playerData.apdef);
            cmd.Parameters.AddWithValue("dodge", playerData.dodge);
            cmd.Parameters.AddWithValue("pierce", playerData.pierce);
            cmd.Parameters.AddWithValue("critical", playerData.critical);

            //执行查询
            cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            PECommon.Log("数据库更新出错" + e, LogType.Error);
            return false;
        }
        return true;
    }
}
