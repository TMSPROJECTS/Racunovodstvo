using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data.Odbc;

using MySql.Data;

/// <summary>
/// Summary description for Konekcija
/// </summary>
public class Konekcija
{
    public Konekcija()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static string VratiPath()
    {
        return "Server=localhost;Port=3306;Database=master;Uid=app_user;Pwd = tsm!1234; ";
        //return "Data Source=DESKTOP-17U3H2P\\SQLEXPRESS;Initial Catalog=RIS;User Id=mm;Password = mmm;";
    }
    public static string VratiPath2(string nazivPoslovnice)
    {
        return "Server=localhost;Port=3306;Database=" + nazivPoslovnice + ";Uid=app_user;Pwd = tsm!1234; ";
        //return "Data Source=DESKTOP-17U3H2P\\SQLEXPRESS;Initial Catalog=RIS;User Id=mm;Password = mmm;";
    }

    public static string VratiPath3()
    {
        return "Server=localhost;Port=3306;Database=admin;Uid=app_user;Pwd = tsm!1234; ";
        //return "Data Source=DESKTOP-17U3H2P\\SQLEXPRESS;Initial Catalog=RIS;User Id=mm;Password = mmm;";
    }

}