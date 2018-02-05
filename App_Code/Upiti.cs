using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

using MySql.Data;
using MySql.Data.MySqlClient;

/// <summary>
/// Summary description for Upiti
/// </summary>
public class Upiti
{
    public Upiti()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static DataTable Select(string kolona, string tabela, string uslov)
    {
        string where = "";

        if (uslov.Length > 2 && uslov.Substring(0, 3) != "ne,")
        {
            where = " where ";
        }
        else if (uslov.Length > 3 && uslov.Substring(0, 3) == "ne,")
        {
            where = "";
            uslov = uslov.Substring(3);
        }
        else if (uslov.Length == 2)
        {
            where = "";
            uslov = "";
        }
        MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath());
        string sql = "Select " + kolona + " from " + tabela + " " + where + uslov;
        MySqlCommand komanda = new MySqlCommand(sql, konekcija);
        MySqlDataAdapter adapter = new MySqlDataAdapter(komanda);
        DataTable dtRezultat = new DataTable();
        adapter.Fill(dtRezultat);

        return dtRezultat;

    }

    public static DataTable Select2(string kolona, string tabela, string uslov, string nazivBaze)
    {
        string where = "";

        if (uslov.Length > 2 && uslov.Substring(0, 3) != "ne,")
        {
            where = " where ";
        }
        else if (uslov.Length > 3 && uslov.Substring(0, 3) == "ne,")
        {
            where = "";
            uslov = uslov.Substring(3);
        }
        else if (uslov.Length == 2)
        {
            where = "";
            uslov = "";
        }
        MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath2(nazivBaze));
        string sql = "Select " + kolona + " from " + tabela + " " + where + uslov;
        MySqlCommand komanda = new MySqlCommand(sql, konekcija);
        MySqlDataAdapter adapter = new MySqlDataAdapter(komanda);
        DataTable dtRezultat = new DataTable();
        adapter.Fill(dtRezultat);

        return dtRezultat;

    }

    public static DataTable Select3(string kolona, string tabela, string uslov)
    {
        string where = "";

        if (uslov.Length > 2 && uslov.Substring(0, 3) != "ne,")
        {
            where = " where ";
        }
        else if (uslov.Length > 3 && uslov.Substring(0, 3) == "ne,")
        {
            where = "";
            uslov = uslov.Substring(3);
        }
        else if (uslov.Length == 2)
        {
            where = "";
            uslov = "";
        }
        MySqlConnection konekcija = new MySqlConnection(Konekcija.VratiPath3());
        string sql = "Select " + kolona + " from " + tabela + " " + where + uslov;
        MySqlCommand komanda = new MySqlCommand(sql, konekcija);
        MySqlDataAdapter adapter = new MySqlDataAdapter(komanda);
        DataTable dtRezultat = new DataTable();
        adapter.Fill(dtRezultat);

        return dtRezultat;

    }

}